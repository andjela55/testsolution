import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { AppRoutes } from '../model/app-routes';
import { UserData } from '../model/user-data';

@Injectable({
  providedIn: 'root',
})
export class LocalStorageService {
  private readonly TOKEN_KEY = 'token';
  private readonly REFRESH_TOKEN_KEY = 'refresh_token';
  private readonly CURRENT_USER = 'current_user';

  private token: string = null as any;
  private refreshToken: string = null as any;
  private user: UserData = null as any;

  constructor(private router: Router) {}
  onInit(): void {
    let tokenValue = localStorage.getItem(this.TOKEN_KEY);
    if (tokenValue != null) {
      this.token = tokenValue;
    }
    let refreshtokenValue = localStorage.getItem(this.REFRESH_TOKEN_KEY);
    if (refreshtokenValue != null) {
      this.refreshToken = refreshtokenValue;
    }
  }

  public getToken(): string | null {
    return this.token;
  }

  public setToken(token: string): void {
    localStorage.setItem(this.TOKEN_KEY, token);
    this.token = token;
  }
  public getRefreshToken(): string | null {
    return this.refreshToken;
  }

  public setRefreshToken(refreshToken: string): void {
    localStorage.setItem(this.REFRESH_TOKEN_KEY, refreshToken);
    this.refreshToken = refreshToken;
  }
  public logoutUser(): void {
    localStorage.removeItem(this.TOKEN_KEY);
    localStorage.removeItem(this.REFRESH_TOKEN_KEY);
    localStorage.removeItem(this.CURRENT_USER);
    this.token = null as any;
    this.refreshToken = null as any;
    this.router.navigate([AppRoutes.Login]);
  }
  public setCurrentUser(user: UserData): void {
    localStorage.setItem(this.CURRENT_USER, JSON.stringify(user));
    this.user = user;
  }
  public getCurrentUser(): UserData {
    let user = JSON.parse(JSON.stringify(this.user)) as UserData;
    return user;
  }
  public removeCurrentUser(): void {
    localStorage.removeItem(this.CURRENT_USER);
    this.user = null as any;
  }
}
