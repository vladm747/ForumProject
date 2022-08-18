import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Emitters } from '../emmiters/emmiters';
import { ForumApiService } from '../forum-api.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  authenticated = false;
  isAdmin: boolean;
  currentUser: any;
  constructor(
    private service: ForumApiService,
    private http: HttpClient

  ) { }

  ngOnInit(): void {
    Emitters.authEmitter.subscribe(
      (auth: boolean) => {
        this.authenticated = auth;
      }
    );
    this.service.getCurrentUser().subscribe((res:any) => {
      this.currentUser = res;
    },
    (err:any) => {
      this.currentUser = null;
      console.log(err);
    });

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

  logOut(): void {
    this.http.post('https://localhost:44394/api/Auth/SignOut', {}, {withCredentials: true})
      .subscribe(() => {
        this.authenticated = false;
        this.isAdmin = false;
      });
  }
}
