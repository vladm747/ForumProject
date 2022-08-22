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
    return this.http.post(this.APIurl + '/Topic', data, {withCredentials: true});
  }

  deleteTopic(id: number|string) {
    return this.http.delete(this.APIurl + `/Topic/${id}`, {withCredentials: true})
  }

  getTopicById(id: number|string) {
    return this.http.get(this.APIurl + `/Topic/${id}`);
  }


  //Message

  getMessagesByTopicId(topicId : number|string|undefined): Observable<any[]> {
    return this.http.get<any>(this.APIurl + `/Message/getMessagesByTopicId?topicId=${topicId}`);
  }
  getMessagesByUserId(userId : number|string|undefined) {
    return this.http.get(this.APIurl + `/Message/getMessagesByUserId?userId=${userId}`);
  }
  getMessageById(id: number|string) {
    return this.http.get(this.APIurl + `/Message/${id}`);
  }

  addMessage(data: any) {
    return this.http.post(this.APIurl + '/Message', data, {withCredentials: true});
  }

  updateMessage(data: any) {
    return this.http.put(this.APIurl + '/Message', data,  {withCredentials: true});
  }

  deleteMessage(id: number|string) {
    return this.http.delete(this.APIurl + `/Message/${id}`,  {withCredentials: true})
  }

  //User

  getCurrentUser(): any {
    return this.http.get(this.APIurl + '/User/current', {withCredentials: true})
  }

  getUserById(id: number|string): any {
    return this.http.get(this.APIurl + `/User/${id}`)
  }

  getAllUsers() {
    return this.http.get(this.APIurl + '/User', {withCredentials: true})
  }

  //Role

  getRole(){
    return this.http.get(this.APIurl + '/Role', {withCredentials: true})
  }
}
