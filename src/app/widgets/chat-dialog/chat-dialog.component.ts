import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { map, Observable } from 'rxjs';
import { AuthService } from 'src/app/api/auth.service';
import { ChatService } from 'src/app/api/chat.service';
import { LocalStorageService } from 'src/app/api/local-storage.service';
import { UserService } from 'src/app/api/user.service';
import { MessageModel } from 'src/app/model/messageDto';
import { UserData } from 'src/app/model/user-data';
import { UserDto } from 'src/app/model/userDto';
@Component({
  selector: 'app-chat-dialog',
  templateUrl: './chat-dialog.component.html',
  styleUrls: ['./chat-dialog.component.scss'],
})
export class ChatDialogComponent {
  msgInboxArray: MessageModel[] = [];
  messageModel: MessageModel = new MessageModel();
  currentUser: UserData = new UserData();
  constructor(
    public dialogRef: MatDialogRef<ChatDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: UserData,
    private chatService: ChatService,
    private localStorage: LocalStorageService,
    private userService: UserService,
    private authService: AuthService
  ) {
    this.messageModel.receiver = data.id?.toString()!;
    this.setCurrentUserForChat();
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
  addToInbox(obj: MessageModel) {
    let newObj = new MessageModel();
    newObj.message = obj.message;
    newObj.user = obj.user;
    this.msgInboxArray.push(newObj);
  }
  send(): void {
    if (!this.messageModel) {
      window.alert('Message is required.');
      return;
    }
    if (this.messageModel.message.length == 0) {
      window.alert('Message is required.');
      return;
    }

    this.chatService.broadcastMessageToSpecificPerson(this.messageModel); // Send the message via a service
    this.messageModel.message = '';
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
  private setCurrentUserValues(user: UserDto) {
    let userData = new UserData();
    userData.id = user.id;
    userData.email = user.email;
    userData.name = user.name;
    userData.accountConfirmed = user.accountConfirmed;
    return userData;
  }
  private setCurrentUserForChat() {
    this.currentUser = this.localStorage.getCurrentUser();
    this.messageModel.user = this.currentUser.name!;
  }
}
