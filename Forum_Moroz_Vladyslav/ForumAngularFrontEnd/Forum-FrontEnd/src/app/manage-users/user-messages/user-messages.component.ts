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
  isAdmin: boolean;
  currentUser: any;
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
    this.service.getCurrentUser().subscribe((res:any)=>{
      this.currentUser = res;
    },
    (err: any) => {
      this.currentUser = null;
      console.log(err, "user-massages");
    })
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
  isAdministrator(): boolean {
    this.isAdmin = false;
    if(this.currentUser==null)
    {
      this.isAdmin = false;
    }
    else if(this.currentUser.roles.includes('admin'))
    {
      this.isAdmin = true;
    }

    return this.isAdmin;
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
  delete(item: any){
    if(confirm(`Are you sure you wanna delete message ${item.id}`)){
      this.service.deleteMessage(item.id).subscribe(res => {
        var closeModalBtn = document.getElementById('add-edit-modal-close');
        if(closeModalBtn){
          closeModalBtn.click();
        }

        var showDeleteSuccess = document.getElementById('delete-success-alert');
        if(showDeleteSuccess){
          showDeleteSuccess.style.display = "block";
        }

        setTimeout(function() {
          if(showDeleteSuccess) {
            showDeleteSuccess.style.display = "none"
          }
        }, 4000);
        this.messageList = this.service.getMessagesByUserId(this.userId);
      });
    }
  }
}
