import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './api/auth.service';
import { AppRoutes } from './model/app-routes';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'testProjectFront';
}
