import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.css']
})
export class TestErrorComponent implements OnInit {

  baseUrl = 'https://localhost:5001/api/';
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }

  Get404Error(){
    this.http.get(this.baseUrl+'buggy/not-found').subscribe({
      next: response=> console.log(response),
      error: error=> console.log(error)
    })
  }

  Get400Error(){
    console.log(55);
    this.http.get(this.baseUrl+'buggy/bad-request').subscribe({
      next: response=> console.log(response),
      error: error=> console.log(error)
    })
  }

  Get500Error(){
    this.http.get(this.baseUrl+'buggy/server-error').subscribe({
      next: response=> console.log(response),
      error: error=> console.log(error)
    })
  }

  Get401Error(){
    this.http.get(this.baseUrl+'buggy/auth').subscribe({
      next: response=> console.log(response),
      error: error=> console.log(error)
    })
  }

  Get400ValidatinError(){
    this.http.post(this.baseUrl+'account/register',{}).subscribe({
      next: response=> console.log(response),
      error: error=> console.log(error)
    })
  }
}
