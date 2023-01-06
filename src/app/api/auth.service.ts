import { Injectable } from '@angular/core';
import { UserData } from '../model/user-data';
import { LocalStorageService } from './local-storage.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private localStorage: LocalStorageService) {}
  public checkIfTokenExists(): boolean {
    return this.localStorage.getToken() != null;
  }
  public logoutUser(): void {
    this.localStorage.logoutUser();
  }
  public setCurrentUser(user: UserData): void {
    this.localStorage.setCurrentUser(user);
  }
}
