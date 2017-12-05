import { Component, OnInit } from '@angular/core';

import { User } from '../_models/index';
import { UserService } from '../_services/index';

@Component({
    moduleId: module.id,
    templateUrl: 'home.component.html'
})

export class HomeComponent implements OnInit {
    currentUser: User;

    constructor(private userService: UserService) {
      
       this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
    }

    ngOnInit() {
       
    }
  
}
