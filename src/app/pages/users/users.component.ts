import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { map, Observable, takeUntil } from 'rxjs';
import { UserService } from 'src/app/api/user.service';
import { AppRoutes } from 'src/app/model/app-routes';
import { UserData } from 'src/app/model/user-data';
import { BaseComponent } from '../base/base.component';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss'],
})
export class UsersComponent extends BaseComponent implements OnInit {
  users: Array<UserData> = new Array<UserData>();
  displayedColumns: Array<string> = ['email', 'name'];
  constructor(private router: Router, private userService: UserService) {
    super();
  }
  ngOnInit(): void {
    this.loadData().subscribe();
  }
  showUser(id: number) {
    this.router.navigate([`${AppRoutes.User}/${id}`]);
  }
  loadData(): Observable<void> {
    return this.userService.apiUserGetAllGet().pipe(
      takeUntil(this.ngUnsubscribe),
      map((x) => {
        this.users = x;
      })
    );
  }
}
