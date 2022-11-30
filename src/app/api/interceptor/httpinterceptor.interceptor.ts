import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpResponse,
  HttpErrorResponse,
} from '@angular/common/http';
import { catchError, map, Observable, switchMap, tap, throwError } from 'rxjs';
import { LocalStorageService } from '../local-storage.service';
import { LoginService } from '../login.service';

@Injectable()
export class HttpinterceptorInterceptor implements HttpInterceptor {
  constructor(
    private localStorageService: LocalStorageService,
    private loginService: LoginService
  ) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    var token = this.localStorageService.getToken();
    if (token) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`,
        },
      });
    }

    return next.handle(request).pipe(
      catchError((err) => {
        if (err instanceof HttpErrorResponse && err.status === 401) {
          console.log('GRESKA 401');
          //this.authService.onInit();
          var refreshToken = this.localStorageService.getRefreshToken();

          return this.loginService.apiLoginRefreshPost(refreshToken!).pipe(
            switchMap((x) => {
              return next.handle(request);
            })
          );
        }
        return throwError(() => err);
      })
    );
  }
}
