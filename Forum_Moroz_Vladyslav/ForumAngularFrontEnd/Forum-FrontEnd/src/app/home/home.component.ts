import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Emitters } from '../emmiters/emmiters';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  message: string = '';

  constructor(
    private http: HttpClient
  ) { }

  ngOnInit(): void {
    this.http.get('https://localhost:44394/api/User/current', {withCredentials: true}).subscribe(
      (res: any) => {
        this.message = `Hi, ${res.firstName} ${res.lastName}!`;
        Emitters.authEmitter.emit(true);
      },
      (err: any) => {
        console.log(err);
        this.message = 'You are not logged in!';
        Emitters.authEmitter.emit(false);
      }
    )
  }

}
