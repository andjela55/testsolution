import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CanActivatePageGuard } from './api/guards/can-activate-page.guard';
import { ResolverService } from './api/resolver.service';
import { AppRoutes } from './model/app-routes';
import { AuthorizedComponent } from './pages/authorized/authorized.component';
import { LoginComponent } from './pages/login/login.component';
import { MainComponent } from './pages/main/main.component';
import { SignalRChatComponent } from './pages/signal-r-chat/signal-r-chat.component';
import { UserComponent } from './pages/user/user.component';
import { UsersComponent } from './pages/users/users.component';

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
      {
        path: `${AppRoutes.User}/:id`,
        component: UserComponent,
        resolve: {
          data: ResolverService,
        },
      },
      {
        path: AppRoutes.Users,
        component: UsersComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
