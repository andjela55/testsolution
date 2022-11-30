import { Component, OnDestroy, OnInit } from '@angular/core';
import { takeUntil } from 'rxjs';
import { LocalStorageService } from 'src/app/api/local-storage.service';
import { LoginService } from 'src/app/api/login.service';
import { UserDto } from 'src/app/model/userDto';
import { BaseComponent } from '../base/base.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent extends BaseComponent {
  user: UserDto = new UserDto();
  constructor(
    private loginService: LoginService,
    private localStorageService: LocalStorageService
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
      });
  }
}
