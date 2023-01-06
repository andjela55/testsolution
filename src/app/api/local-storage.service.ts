import { Injectable } from '@angular/core';
import { UserData } from '../model/user-data';
import { UserDto } from '../model/userDto';

@Injectable({
  providedIn: 'root',
})
export class LocalStorageService {
  private readonly TOKEN_KEY = 'token';
  private readonly REFRESH_TOKEN_KEY = 'refresh_token';
  private readonly CURRENT_USER = 'current_user';
  constructor() {}

  public getToken(): string | null {
    var token = localStorage.getItem(this.TOKEN_KEY);
    return token;
  }

  public setToken(token: string): void {
    localStorage.setItem(this.TOKEN_KEY, token);
  }
  public getRefreshToken(): string | null {
    var token = localStorage.getItem(this.REFRESH_TOKEN_KEY);
    return token;
  }

  public setRefreshToken(token: string): void {
    localStorage.setItem(this.REFRESH_TOKEN_KEY, token);
  }
  public logoutUser(): void {
    localStorage.removeItem(this.TOKEN_KEY);
    localStorage.removeItem(this.REFRESH_TOKEN_KEY);
    localStorage.removeItem(this.CURRENT_USER);
  }
  public setCurrentUser(user: UserData): void {
    localStorage.setItem(this.CURRENT_USER, JSON.stringify(user));
  }
  public getCurrentUser(): UserData {
    let user = JSON.parse(localStorage.getItem(this.CURRENT_USER)!) as UserData;
    return user;
  }
  public removeCurrentUser(): void {
    localStorage.removeItem(this.CURRENT_USER);
  }
}
