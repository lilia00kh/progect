import { Router } from '@angular/router';
import { AuthenticationService } from './../shared/services/authentication.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {

  constructor() {
  }

  ngOnInit(): void {
  }

  goToBottom(){
    window.scrollTo(0,document.body.scrollHeight);
  }
}
