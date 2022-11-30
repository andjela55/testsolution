import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class LocalStorageService {
  private readonly TOKEN_KEY = 'token';
  private readonly REFRESH_TOKEN_KEY = 'refresh_token';
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
}
