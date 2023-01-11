import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/api/auth.service';
import { CurrentRouteService } from 'src/app/api/current-route.service';
import { AppRoutes } from 'src/app/model/app-routes';
import { BaseComponent } from '../base/base.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { SpinnerService } from 'src/app/api/helperServices/spinner.service';
@Component({
  selector: 'app-authorized',
  templateUrl: './authorized.component.html',
  styleUrls: ['./authorized.component.scss'],
})
export class AuthorizedComponent {
  public showSpinner: boolean = true;
  constructor(
    private authService: AuthService,
    private currentRouteService: CurrentRouteService,
    private spinnerService: SpinnerService
  ) {
    this.spinnerService.getSpinner().subscribe((x) => {
      if (x == 0) {
        this.showSpinner = false;
      }
    });
  }
  childActivated(component: BaseComponent) {
    this.currentRouteService.setCurrentRoute(component);
  }
  logout(): void {
    this.currentRouteService.checkIfComponentCanLogout().subscribe((x) => {
      if (x == false) {
        return;
      }
      this.authService.logoutUser();
    });
  }
}
