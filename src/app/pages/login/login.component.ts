import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, of, takeUntil } from 'rxjs';
import { LocalStorageService } from 'src/app/api/local-storage.service';
import { LoginService } from 'src/app/api/login.service';
import { UserService } from 'src/app/api/user.service';
import { AppRoutes } from 'src/app/model/app-routes';
import { UserDto } from 'src/app/model/userDto';
import { BaseComponent } from '../base/base.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent extends BaseComponent {
  user: UserDto = new UserDto();
  users: Array<UserDto> = new Array<UserDto>();
  displayedColumns: Array<string> = ['email', 'name'];
  constructor(
    private loginService: LoginService,
    private localStorageService: LocalStorageService,
    private userService: UserService,
    private router: Router
  ) {
    super();
  }

  public onLogin(): void {
    this.loginService
      .apiLoginLoginPost(this.user)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe((x) => {
        console.log(x);
        this.localStorageService.setToken(x.jwtToken!);
        this.localStorageService.setRefreshToken(x.refreshToken!);
        this.user = new UserDto();
        this.router.navigate([AppRoutes.Main]);
      });
  }
  public getData(): void {
    this.userService.apiUserGetAllGet().subscribe((x) => {
      this.users = x;
      console.log(x);
    });
  }
  canLogout(): Observable<boolean> {
    return of(true);
  }
}
