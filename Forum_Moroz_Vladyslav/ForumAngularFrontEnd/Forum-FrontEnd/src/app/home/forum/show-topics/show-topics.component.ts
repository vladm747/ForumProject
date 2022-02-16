import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ForumApiService } from 'src/app/forum-api.service';
@Component({
  selector: 'app-show-topics',
  templateUrl: './show-topics.component.html',
  styleUrls: ['./show-topics.component.css']
})
export class ShowTopicsComponent implements OnInit {

  topicList: Observable<any[]>;
  messageList: Observable<any[]>;

  forumMessagesMap: Map<number, string> = new Map()

  constructor(
    private service: ForumApiService
    ) { }

  ngOnInit(): void {
    this.topicList = this.service.getTopicList();
  }

}
