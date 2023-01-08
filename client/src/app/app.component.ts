import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from 'src/_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Dating app';

  constructor( private accounService: AccountService) {
  }
  
  ngOnInit(): void {
    this.setCorrectuser();
  } 

  setCorrectuser(){
    const userString = localStorage.getItem('user');
    if (!userString) 
      return;
    const user: User = JSON.parse(userString);
    this.accounService.setCurrentUser(user);
  }
}
