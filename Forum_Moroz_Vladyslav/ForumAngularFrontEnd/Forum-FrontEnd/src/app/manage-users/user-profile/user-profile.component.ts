import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { ForumApiService } from 'src/app/forum-api.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

  userId: string;
  user: any;
  userList: any=[];
  topicList: any=[];
  authorsMap: Map<number, string> = new Map();
  topicMap: Map<number, string> = new Map();
  userMessages: any=[];

  private routeSubscription: Subscription;
  private querySubscription: Subscription;
  constructor(
    private service: ForumApiService,
    private activateRoute: ActivatedRoute){
      this.routeSubscription = activateRoute.params.subscribe(params=>this.userId=params['userId']);
        this.querySubscription = activateRoute.queryParams.subscribe(
            (queryParam: any) => {
                this.userId = queryParam['userId'];
            }
        );
     }
  ngOnInit(): void {
    this.userMessages = this.service.getMessagesByUserId(this.userId);
    this.service.getUserById(this.userId).subscribe((res: any) =>{
      this.user = res;
    },
    (err:any) =>{

      console.log(err, "user-profile");
    })

    this.showTopicMap();
    this.showAuthorMap();
  }
  showAuthorMap() {
    this.service.getAllUsers().subscribe(data => {
      this.userList = data;

      for(let i = 0; i < this.userList.length; i++)
      {
        this.authorsMap.set(this.userList[i].id, this.userList[i].firstName);
      }
    })
  }

  showTopicMap() {
    this.service.getTopicList().subscribe(data => {
      this.topicList = data;

      for(let i = 0; i < this.topicList.length; i++)
      {
        this.topicMap.set(this.topicList[i].id, this.topicList[i].name);
      }
    })
  }
}
