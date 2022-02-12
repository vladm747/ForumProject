import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup} from '@angular/forms';
import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  form: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private http: HttpClient
    ) {

  }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      FirstName: '',
      LastName: '',
      Email: '',
      Password: '',
      ConfirmPassword: ''
    });
  }

  submit(): void{

    this.http.post('https://localhost:8000/api/Auth/register', this.form.getRawValue())
    .subscribe(res => {
      console.log(res);
    })
  }
}
