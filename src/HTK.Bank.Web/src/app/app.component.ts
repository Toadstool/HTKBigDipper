import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from './_services/index'
import { Observable } from 'rxjs/Observable';

@Component({
    moduleId: module.id,
    selector: 'app',
    templateUrl: 'app.component.html'
})

export class AppComponent implements OnInit {

  isLoggedIn: Observable<boolean>;

  constructor(private authService: AuthenticationService) {   
  }

  ngOnInit() {
    this.isLoggedIn = this.authService.isLoggedIn;
    this.authService.checkIfLogged();
  }


}
