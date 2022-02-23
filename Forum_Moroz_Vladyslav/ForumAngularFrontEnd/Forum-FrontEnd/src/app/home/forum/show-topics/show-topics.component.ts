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
  message: string = '';
  authorsMap: Map<number, string> = new Map()
  userList: any=[];

  constructor(
    private service: ForumApiService
    ) { }

  ngOnInit(): void {
    this.topicList = this.service.getTopicList();
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
}
