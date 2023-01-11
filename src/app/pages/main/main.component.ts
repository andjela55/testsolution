import { Component } from '@angular/core';
import { Observable, of, takeUntil } from 'rxjs';
import { AuthService } from 'src/app/api/auth.service';
import { UserService } from 'src/app/api/user.service';
import { BaseComponent } from 'src/app/pages/base/base.component';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
})
export class MainComponent extends BaseComponent {
  constructor(
    private userService: UserService,
    private authService: AuthService
  ) {
    super();
  }
  canLogout(): Observable<boolean> {
    return of(true);
  }
  public getData(): void {
    this.userService
      .apiUserGetAllGet()
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe((x) => {
        console.log(x);
      });
  }
}
