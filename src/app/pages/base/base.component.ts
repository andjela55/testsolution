import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs';

@Component({
  template: '',
})
export abstract class BaseComponent implements OnInit, OnDestroy {
  ngUnsubscribe: Subject<any> = new Subject<any>();

  constructor() {}

  ngOnInit(): void {}

  ngOnDestroy(): void {
    this.ngUnsubscribe.next('');
    this.ngUnsubscribe.complete();
  }
}
