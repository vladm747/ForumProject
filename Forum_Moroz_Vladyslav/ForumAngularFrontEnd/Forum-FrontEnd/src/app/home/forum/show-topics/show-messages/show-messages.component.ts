import { Component, OnInit } from '@angular/core';
import { Observable, switchMap } from 'rxjs';
import { ForumApiService } from 'src/app/forum-api.service';
import { ActivatedRoute} from '@angular/router';
import {Subscription} from 'rxjs';



@Component({
  selector: 'app-show-messages',
  templateUrl: './show-messages.component.html',
  styleUrls: ['./show-messages.component.css']
})
export class ShowMessagesComponent implements OnInit {
  userId:string;
  currentUser: any;
  currentUserId: string;

  topicList: any=[];
  messageList: Observable<any[]>;
  authorsMap: Map<number, string> = new Map();
  topicMap: Map<number, string> = new Map();
  topicId: number;
  titleName: string;
  userList: any=[];
  message: any;
  activateAddEditMessageComponent: boolean;
  isRegist: boolean;
  isAdmin: boolean;
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
    this.messageList = this.service.getMessagesByTopicId(this.topicId);
    this.activateRoute.paramMap.pipe(
      switchMap(params => params.getAll('userId'))
    ).subscribe(data=> this.userId = data);
    this.service.getCurrentUser().subscribe(
      (res: any) => {
        this.currentUser = res;
        this.currentUserId = this.currentUser.id;
      },
      (err: any) => {
        this.currentUser = null;
        console.log(err);
      }
      )
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
  modalAdd() {
    this.message = {
      id:0,
      title:null,
      content: null,
      creationDateTime: null,
      userId: null,
      topicId: Number(this.topicId)
    }
    this.activateAddEditMessageComponent = true;
    this.titleName = "Add Message";
    this.isRegist = true;
  }

  modalEdit(item: any){
    this.message = item;
    this.titleName = "Edit Message";
    this.activateAddEditMessageComponent = true;
  }

  modalClose(){
    this.activateAddEditMessageComponent=false;
    this.messageList = this.service.getMessagesByTopicId(this.topicId);
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
        this.messageList = this.service.getMessagesByTopicId(this.topicId);
      });
    }

  }
}
