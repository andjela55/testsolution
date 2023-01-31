import { Injectable } from '@angular/core';
import { map, Observable, of } from 'rxjs';
import { ActivatedRouteSnapshot, Router } from '@angular/router';
import { UserService } from './user.service';
import { UserData } from '../model/user-data';
import { UserDto } from '../model/userDto';
import { AppRoutes } from '../model/app-routes';
@Injectable({
  providedIn: 'root',
})
export class ResolverService {
  constructor(private userService: UserService, private router: Router) {}

  resolve(
    activatedRoute: ActivatedRouteSnapshot
  ): Observable<UserDto | undefined> {
    let id = activatedRoute.paramMap.get('id');
    return this.userService.apiUserGetIdGet(Number(id)).pipe(
      map((user) => {
        if (!user.accountConfirmed) {
          this.router.navigate([AppRoutes.Users]);
          return;
        }
        return user;
      })
    );
  }
}
