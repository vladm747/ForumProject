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
  isAdmin: boolean;
  currentUser: any;
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
    this.service.getCurrentUser().subscribe(
      (res:any)=>{
        this.currentUser = res;
      },
      (err:any)=>{
        this.currentUser = null;
        console.log(err, "manage-users")
      }
      )
    this.userList = this.service.getAllUsers();
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

  delete(item: any){
    if(confirm(`Are you sure you wanna delete message ${item.email}`)){
      this.service.deleteUser(item.email).subscribe(res => {
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
        this.userList = this.service.getAllUsers();
      });
    }

  }
}
