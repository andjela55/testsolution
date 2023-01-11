import { ObserversModule } from '@angular/cdk/observers';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SpinnerService {
  public spinnerCounter = new BehaviorSubject<number>(0);

  public increaseCounter() {
    this.spinnerCounter.next(this.spinnerCounter.getValue() + 1);
  }
  public decreaseCounter() {
    this.spinnerCounter.next(this.spinnerCounter.getValue() - 1);
  }
  public getSpinner(): Observable<number> {
    return this.spinnerCounter.asObservable();
  }
}
