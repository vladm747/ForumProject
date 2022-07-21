import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ForumApiService } from 'src/app/forum-api.service';
@Component({
  selector: 'app-add-edit-message',
  templateUrl: './add-edit-message.component.html',
  styleUrls: ['./add-edit-message.component.css']
})
export class AddEditMessageComponent implements OnInit {

  messageList$!: Observable<any[]>;
  currentUser: any;

  constructor(private service: ForumApiService) { }

  @Input() message:any;
  id: number = 0;
  title: string = "";
  content: string = "";
  userId: string = "";
  topicId: number = 0;

  ngOnInit(): void {
    this.id = this.message.id;
    this.title = this.message.title;
    this.content = this.message.content;
    this.userId = this.message.userId;
    this.topicId = this.message.topicId;
    this.currentUser = this.service.getCurrentUser();
  }

  addMessage() {
    var message = {
      id: this.id,
      title: this.title,
      content: this.content,
      userId: this.userId,
      topicId: this.topicId
    }
    this.service.addMessage(message).subscribe(res =>{
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
