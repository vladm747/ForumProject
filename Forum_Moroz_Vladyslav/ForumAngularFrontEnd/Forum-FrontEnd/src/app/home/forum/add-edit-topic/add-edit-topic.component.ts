import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ForumApiService } from 'src/app/forum-api.service';
import { ActivatedRoute} from '@angular/router';
import {Subscription} from 'rxjs';
import { switchMap } from 'rxjs/operators';
@Component({
  selector: 'app-add-edit-topic',
  templateUrl: './add-edit-topic.component.html',
  styleUrls: ['./add-edit-topic.component.css']
})
export class AddEditTopicComponent implements OnInit {

  topicList$!: Observable<any[]>;
  currentUser: any;

  constructor(private service:ForumApiService) {

  }

  @Input() topic: any;
  id: number = 0;
  name:string = "";
  created: Date;
  userId: string = "";
  messages: Observable<any[]>;

  ngOnInit(): void {
    this.id = this.topic.id;
    this.name = this.topic.name;
    this.created = this.topic.created;
    this.userId = this.topic.userId;
    this.messages = this.topic.messages;
    this.topicList$ = this.service.getTopicList();
    this.currentUser = this.service.getCurrentUser()
  }

  addTopic() {
    var topic = {
      id: this.id,
      name:this.name,
      created: new Date(),
      userId: this.userId,
      messages:this.messages
    }
    this.service.addTopic(topic).subscribe (res =>{
      var closeModalBtn = document.getElementById('add-edit-modal-close');
      if(closeModalBtn){
        closeModalBtn.click();
      }

      var showAddSuccess = document.getElementById('add-success-alert');
      if(showAddSuccess){
        showAddSuccess.style.display = "block";
      }

      setTimeout(function() {
        if(showAddSuccess) {
          showAddSuccess.style.display = "none"
        }
      }, 4000);
    })
  }
}
