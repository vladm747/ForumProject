import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Emitters } from '../emmiters/emmiters';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  authenticated = false;
  constructor(
    private http: HttpClient
  ) { }

  ngOnInit(): void {
    Emitters.authEmitter.subscribe(
      (auth: boolean) => {
        this.authenticated = auth; 
      }
    );
  }

  logOut(): void {
    this.http.post('https://localhost:44394/api/Auth/SignOut', {}, {withCredentials: true})
      .subscribe(() => this.authenticated = false);
  }
}
