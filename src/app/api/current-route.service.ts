import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseComponent } from '../pages/base/base.component';

@Injectable({
  providedIn: 'root',
})
export class CurrentRouteService {
  private currentComponent!: BaseComponent;
  constructor() {}
  setCurrentRoute(route: BaseComponent) {
    this.currentComponent = route;
  }
  checkIfComponentCanLogout(): Observable<boolean> {
    return this.currentComponent.canLogout();
  }
}
