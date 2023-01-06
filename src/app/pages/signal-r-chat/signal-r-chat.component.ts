import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { forkJoin, map, Observable, takeUntil } from 'rxjs';
import { AuthService } from 'src/app/api/auth.service';
import { ChatService } from 'src/app/api/chat.service';
import { LocalStorageService } from 'src/app/api/local-storage.service';
import { UserService } from 'src/app/api/user.service';
import { MessageModel, UserDto } from 'src/app/model/models';
import { UserData } from 'src/app/model/user-data';
import { ChatDialogComponent } from 'src/app/widgets/chat-dialog/chat-dialog.component';
import { BaseComponent } from '../base/base.component';

@Component({
  selector: 'app-signal-r-chat',
  templateUrl: './signal-r-chat.component.html',
  styleUrls: ['./signal-r-chat.component.scss'],
})
export class SignalRChatComponent extends BaseComponent {
  messageModel: MessageModel = new MessageModel();
  msgInboxArray: MessageModel[] = [];
  currentUser: UserData = new UserData();
  activeUsers: Array<UserData> = new Array<UserData>();
  displayedColumns: Array<string> = ['user', 'email'];
  constructor(
    private chatService: ChatService,
    private localStorage: LocalStorageService,
    private userService: UserService,
    private authService: AuthService,
    public dialog: MatDialog
  ) {
    super();
    forkJoin([this.getCurrentUser(), this.getMessages(), this.getActiveUsers()])
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe();
    // this.userService
    //   .apiUserGetCurrentGet()
    //   .pipe(takeUntil(this.ngUnsubscribe))
    //   .subscribe((user) => {
    //     let userData = this.setCurrentUserValues(user);
    //     this.authService.setCurrentUser(userData);
    //     this.setCurrentUserForChat();
    //   });
    // this.chatService
    //   .retrieveMappedObject()
    //   .subscribe((receivedObj: MessageModel) => {
    //     this.addToInbox(receivedObj);
    //   });
  }
  private setCurrentUserForChat() {
    this.currentUser = this.localStorage.getCurrentUser();
    this.messageModel.user = this.currentUser.name!;
  }

  send(): void {
    if (this.messageModel) {
      if (
        this.messageModel.user.length == 0 ||
        this.messageModel.user.length == 0
      ) {
        window.alert('Both fields are required.');
        return;
      } else {
        this.chatService.broadcastMessage(this.messageModel); // Send the message via a service
        this.messageModel.message = '';
      }
    }
  }
  addToInbox(obj: MessageModel) {
    let newObj = new MessageModel();
    newObj.user = obj.user;
    newObj.message = obj.message;
    this.msgInboxArray.push(newObj);
  }
  private setCurrentUserValues(user: UserDto) {
    let userData = new UserData();
    userData.id = user.id;
    userData.email = user.email;
    userData.name = user.name;
    userData.accountConfirmed = user.accountConfirmed;
    return userData;
  }
  getCurrentUser(): Observable<void> {
    return this.userService.apiUserGetCurrentGet().pipe(
      map((user) => {
        let userData = this.setCurrentUserValues(user);
        this.authService.setCurrentUser(userData);
        this.setCurrentUserForChat();
      })
    );
  }
  getMessages(): Observable<void> {
    return this.chatService.retrieveMappedObject().pipe(
      map((x) => {
        this.addToInbox(x);
      })
    );
  }
  getActiveUsers(): Observable<void> {
    return this.chatService.getActiveUsers().pipe(
      map((x) => {
        this.activeUsers = x;
      })
    );
  }
  chat(user: UserData) {
    console.log(('Clicked: ' + user) as UserData);
    const dialogRef = this.dialog.open(ChatDialogComponent, {
      data: user as UserData,
    });

    dialogRef.afterClosed().subscribe((result) => {
      console.log('The dialog was closed');
    });
  }
}
