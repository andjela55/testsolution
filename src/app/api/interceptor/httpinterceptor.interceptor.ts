import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpResponse,
  HttpErrorResponse,
} from '@angular/common/http';
import {
  catchError,
  map,
  mergeMap,
  Observable,
  of,
  switchMap,
  tap,
  throwError,
} from 'rxjs';
import { LocalStorageService } from '../local-storage.service';
import { LoginService } from '../login.service';
import { LoginResponse } from 'src/app/model/loginResponse';
import { Router } from '@angular/router';

@Injectable()
export class HttpinterceptorInterceptor implements HttpInterceptor {
  userIsLoggedIn: boolean = false;
  constructor(
    private localStorageService: LocalStorageService,
    private loginService: LoginService,
    private router: Router
  ) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    var token = this.localStorageService.getToken();
    if (token) {
      request = this.getRequestWithHeaders(request, token);
      this.userIsLoggedIn = true;
    }
    if (!token) {
      this.userIsLoggedIn = false;
    }

    return next.handle(request).pipe(
      catchError((err) => {
        if (!(err instanceof HttpErrorResponse)) {
          return throwError(() => err);
        }

        if (err.status != 401) {
          console.log('Not 401 error');
          return throwError(() => err);
        }
        if (err.status === 401 && request.url.includes('refresh')) {
          this.router.navigate(['/login']);
          return of();
        }

        var refreshToken = this.localStorageService.getRefreshToken();

        var newData = new LoginResponse();
        newData.refreshToken = refreshToken;
        newData.jwtToken = 'lll';

        return this.loginService.apiLoginRefreshPost(newData!).pipe(
          mergeMap((x) => {
            this.localStorageService.setToken(x.jwtToken!);
            this.localStorageService.setRefreshToken(x.refreshToken!);
            request = this.getRequestWithHeaders(request, x.jwtToken!);
            return next.handle(request);
          })
        );
      })
    );
  }
  private getRequestWithHeaders(
    request: HttpRequest<unknown>,
    token: string
  ): HttpRequest<unknown> {
    return request.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`,
      },
    });
  }
}
