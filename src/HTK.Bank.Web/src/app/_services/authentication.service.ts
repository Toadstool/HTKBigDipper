import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map'
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { User } from './../_models/index'

@Injectable()
export class AuthenticationService {

  private loggedIn = new BehaviorSubject<boolean>(false);

  constructor(private http: Http) { }

  login(username: string, password: string) {
    return Observable.create(observer => {

      var user = new User();
      user.username = username;
      user.firstName = username;

      localStorage.setItem('currentUser', JSON.stringify(user));
      this.loggedIn.next(true);
      console.log('logged');

      observer.next(user);
      observer.complete();
    });

  }

  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    this.loggedIn.next(false);
  }


  get isLoggedIn() {
    
    return this.loggedIn.asObservable();
  }

  checkIfLogged() {
    if (localStorage.getItem('currentUser')) {
      this.loggedIn.next(true);
      console.log('logged');
    }
  }

}
