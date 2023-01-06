import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Subject } from 'rxjs/internal/Subject';
import { HttpClient } from '@angular/common/http';
import { map, Observable, tap } from 'rxjs';
import { MessageModel, UserDto } from '../model/models';
import { LocalStorageService } from './local-storage.service';
import { IHttpConnectionOptions } from '@microsoft/signalr';
import { UserData } from '../model/user-data';

@Injectable({
  providedIn: 'root',
})
export class ChatService {
  readonly CHAT_URL = 'https://localhost:7158/api/chat/';

  private receivedMessageObject: MessageModel = new MessageModel();
  private sharedObj = new Subject<MessageModel>();
  private options: IHttpConnectionOptions = {
    accessTokenFactory: () => {
      let token = this.localStorage.getToken();
      return token!;
    },
    skipNegotiation: true,
    transport: signalR.HttpTransportType.WebSockets,
  };
  private connection: any = new signalR.HubConnectionBuilder()
    .withUrl('https://localhost:7158/chatsocket', this.options)
    .configureLogging(signalR.LogLevel.Information)
    .build();
  constructor(
    private http: HttpClient,
    private localStorage: LocalStorageService
  ) {
    this.connection.onclose(async () => {
      await this.start();
    });
    this.connection.on('ReceiveMessage', (user: string, message: string) => {
      this.mapReceivedMessage(user, message);
    });
    this.start();
  }

  public async start() {
    try {
      await this.connection.start();
      console.log('connected');
    } catch (err) {
      console.log(err);
      setTimeout(() => this.start(), 5000);
    }
  }
  private mapReceivedMessage(user: string, message: string): void {
    this.receivedMessageObject.user = user;
    this.receivedMessageObject.message = message;
    this.sharedObj.next(this.receivedMessageObject);
  }

  public broadcastMessage(msgDto: any) {
    this.http
      .post(this.CHAT_URL + 'send', msgDto)
      .subscribe((data) => console.log(data));
    // this.connection.invoke("SendMessage1", msgDto.user, msgDto.msgText);    // This can invoke the server method named as "SendMethod1" directly.
  }

  public retrieveMappedObject(): Observable<MessageModel> {
    return this.sharedObj.asObservable();
  }
  public getActiveUsers(): Observable<Array<UserData>> {
    return this.http.get<Array<UserData>>(this.CHAT_URL + 'activeUsers');
  }
  public broadcastMessageToSpecificPerson(msgDto: any) {
    this.http
      .post(this.CHAT_URL + 'sendToReceiver', msgDto)
      .subscribe((data) => console.log(data));
    // this.connection.invoke("SendMessage1", msgDto.user, msgDto.msgText);    // This can invoke the server method named as "SendMethod1" directly.
  }
}
