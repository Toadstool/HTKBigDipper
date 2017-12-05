import { Component, OnInit} from '@angular/core';
import { AuthenticationService } from './_services/index'
import { Observable } from 'rxjs/Observable';

@Component({
    moduleId: module.id,
    selector: 'app',
    templateUrl: 'app.component.html'
})

export class AppComponent implements OnInit {

  isLoggedIn = false;

  constructor(
    private authService: AuthenticationService) {   
  }

  ngOnInit() {
    this.authService.isLoggedIn.subscribe(data => {
      this.isLoggedIn = data;
      
    });
    this.authService.checkIfLogged();
  }


}
