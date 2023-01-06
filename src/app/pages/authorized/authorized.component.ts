import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/api/auth.service';
import { AppRoutes } from 'src/app/model/app-routes';

@Component({
  selector: 'app-authorized',
  templateUrl: './authorized.component.html',
  styleUrls: ['./authorized.component.scss'],
})
export class AuthorizedComponent {
  userAuthenticated: boolean = false;
  constructor(private authService: AuthService, private router: Router) {
    if (this.authService.checkIfTokenExists()) {
      this.userAuthenticated = true;
    }
  }
  logout(): void {
    this.authService.logoutUser();
    this.router.navigate([AppRoutes.Login]);
    this.userAuthenticated = false;
  }
}
