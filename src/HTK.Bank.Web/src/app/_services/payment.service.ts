import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { environment } from './../../environments/environment';

@Injectable()
export class PaymentService {

  

  constructor(private http: Http) { }

  
  saveMovements(movement: any) {
    return this.http.post(`${environment.apiUrl}/Movement/SaveMovements`, movement, this.jwt()).map((response: Response) => response.json());
  }


  testMovements(movement: any) {
    return this.http.post(`${environment.apiUrl}/AI/Test`, movement, this.jwt()).map((response: Response) => response.json());
  }

  
  // private helper methods

  private jwt() {
    // create authorization header with jwt token
    let currentUser = JSON.parse(localStorage.getItem('currentUser'));
    let headers = new Headers({ 'UserName': currentUser.username });
    return new RequestOptions({ headers: headers });
    
  }
}
