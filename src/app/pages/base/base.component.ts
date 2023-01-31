import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, of, Subject } from 'rxjs';

@Component({
  template: '',
})
export class BaseComponent implements OnDestroy {
  ngUnsubscribe: Subject<any> = new Subject<any>();

  constructor() {}

  ngOnDestroy(): void {
    this.ngUnsubscribe.next('');
    this.ngUnsubscribe.complete();
  }
  canLogout(): Observable<boolean> {
    return of(true);
  }
}
