import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ActivatedRouteSnapshot } from '@angular/router';
import { takeUntil } from 'rxjs';
import { UserService } from 'src/app/api/user.service';
import { UserData } from 'src/app/model/user-data';
import { BaseComponent } from '../base/base.component';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss'],
})
export class UserComponent extends BaseComponent implements OnInit {
  user: UserData = null as any;

  constructor(private activatedRoute: ActivatedRoute) {
    super();
  }
  ngOnInit(): void {
    this.user = this.activatedRoute.snapshot.data['data'];
  }
}
