import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ForumApiService } from 'src/app/forum-api.service';
import { ActivatedRoute} from '@angular/router';
import {Subscription} from 'rxjs';
@Component({
  selector: 'app-show-messages',
  templateUrl: './show-messages.component.html',
  styleUrls: ['./show-messages.component.css']
})
export class ShowMessagesComponent implements OnInit {

  messageList: Observable<any[]>;

  topicId: number | undefined;

  private routeSubscription: Subscription;
  private querySubscription: Subscription;

  constructor(
    private service: ForumApiService,
    private activateRoute: ActivatedRoute){
      this.routeSubscription = activateRoute.params.subscribe(params=>this.topicId=params['topicId']);
        this.querySubscription = activateRoute.queryParams.subscribe(
            (queryParam: any) => {
                this.topicId = queryParam['topicId'];
            }
        );
     }

  ngOnInit(): void {
    this.messageList = this.service.getMessageListByTopicId(this.topicId);
  }

}
