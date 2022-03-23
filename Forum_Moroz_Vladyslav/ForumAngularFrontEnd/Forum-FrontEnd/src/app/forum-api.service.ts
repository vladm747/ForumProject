import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ForumApiService {

  readonly APIurl = 'https://localhost:44394/api';

  constructor(
    private http: HttpClient
  ) { }

  //Topic
  getTopicList(): Observable<any[]> {
    return this.http.get<any>(this.APIurl + '/Topic/');
  }

  addTopic(data: any) {
    return this.http.post(this.APIurl + '/Topic/', data);
  }

  deleteTopic(id: number|string, data: any) {
    return this.http.delete(this.APIurl + `/Topic/${id}`)
  }

  getTopicById(id: number|string) {
    return this.http.get(this.APIurl + `/Topic/${id}`);
  }


  //Message

  getMessageListByTopicId(topicId : number|string|undefined): Observable<any[]> {
    return this.http.get<any>(this.APIurl + `/Message/getMessagesByTopicId/${topicId}`);
  }

  getMessageById(id: number|string) {
    return this.http.get(this.APIurl + `/Message/${id}`);
  }

  addMessage(data: any) {
    return this.http.post(this.APIurl + '/Message/create', data);
  }

  updateMessage(data: any) {
    return this.http.put(this.APIurl + 'Message', data);
  }

  deleteMessage(id: number|string, data: any) {
    return this.http.delete(this.APIurl + `/Message/${id}`)
  }

  //User

  getCurrentUser() {
    return this.http.get(this.APIurl + '/User/current')
  }

  getUserById(id: number|string): any {
    return this.http.get(this.APIurl + `/User/${id}`)
  }

  getAllUsers() {
    return this.http.get(this.APIurl + '/User/GetAllUsers')
  }

  //Role

  getRole(){
    return this.http.get(this.APIurl + '/Role', {withCredentials: true})
  }
}
