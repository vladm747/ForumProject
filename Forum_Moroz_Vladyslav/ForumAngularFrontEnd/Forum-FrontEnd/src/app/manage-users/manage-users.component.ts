import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { ForumApiService } from '../forum-api.service';
import { ActivatedRoute} from '@angular/router';
@Component({
  selector: 'app-manage-users',
  templateUrl: './manage-users.component.html',
  styleUrls: ['./manage-users.component.css']
})
export class ManageUsersComponent implements OnInit {

  userList: any=[];
  userId: string | undefined;
  constructor(
    private service: ForumApiService,
    private activateRoute: ActivatedRoute
  ) { }

  ngOnInit(): void {

    this.activateRoute.paramMap.pipe(
      switchMap(params => params.getAll('userId'))
    ).subscribe(data=> this.userId = data);

    this.userList = this.service.getAllUsers();
  }


}
