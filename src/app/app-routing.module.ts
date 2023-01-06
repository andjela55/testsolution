import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CanActivatePageGuard } from './api/guards/can-activate-page.guard';
import { AppRoutes } from './model/app-routes';
import { AuthorizedComponent } from './pages/authorized/authorized.component';
import { LoginComponent } from './pages/login/login.component';
import { MainComponent } from './pages/main/main.component';
import { SignalRChatComponent } from './pages/signal-r-chat/signal-r-chat.component';

const routes: Routes = [
  { path: AppRoutes.Login, component: LoginComponent },
  {
    path: '',
    component: AuthorizedComponent,
    canActivate: [CanActivatePageGuard],
    children: [
      {
        path: AppRoutes.Chat,
        component: SignalRChatComponent,
      },
      {
        path: AppRoutes.Main,
        component: MainComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
