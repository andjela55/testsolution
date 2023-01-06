import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { LoginComponent } from './pages/login/login.component';
import { MainComponent } from './pages/main/main.component';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpinterceptorInterceptor } from './api/interceptor/httpinterceptor.interceptor';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { SignalRChatComponent } from './pages/signal-r-chat/signal-r-chat.component';
import { AuthorizedComponent } from './pages/authorized/authorized.component';
import { ChatDialogComponent } from './widgets/chat-dialog/chat-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    MainComponent,
    SignalRChatComponent,
    AuthorizedComponent,
    ChatDialogComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatTableModule,
    MatCardModule,
    MatDialogModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpinterceptorInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
