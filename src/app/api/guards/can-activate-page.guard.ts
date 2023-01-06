import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  RouterStateSnapshot,
  UrlTree,
  Router,
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { AppRoutes } from 'src/app/model/app-routes';
import { AuthService } from '../auth.service';
import { LocalStorageService } from '../local-storage.service';

@Injectable({
  providedIn: 'root',
})
export class CanActivatePageGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    if (!this.authService.checkIfTokenExists()) {
      return this.router.parseUrl(AppRoutes.Login);
    }
    return of(true);
  }
}
