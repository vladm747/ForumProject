import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ForumApiService } from 'src/app/forum-api.service';
import { ActivatedRoute} from '@angular/router';
import {Subscription} from 'rxjs';

@Component({
  selector: 'app-user-messages',
  templateUrl: './user-messages.component.html',
  styleUrls: ['./user-messages.component.css']
})

export class UserMessagesComponent implements OnInit {

  userId:string;
  messageList: any=[];
  authorsMap: Map<number|string, string> = new Map()
  topicMap: Map<number, string> = new Map()
  userList: any=[];
  topicList: any=[];

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
    this.messageList=this.service.getMessagesByUserId(this.userId);
    this.showTopicMap();
    this.showAuthorMap();
  }


  showAuthorMap() {
    this.service.getAllUsers().subscribe(data => {
      this.userList = data;

      for(let i = 0; i < this.userList.length; i++)
      {
        this.authorsMap.set(this.userList[i].id, this.userList[i].firstName + " " + this.userList[i].lastName);
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
