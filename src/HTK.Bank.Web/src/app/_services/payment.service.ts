import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';


@Injectable()
export class PaymentService {
  constructor(private http: Http) { }

  
  testMovement(movement: any) {
    return this.http.post('http://localhost/HTK.Bank.Api/api/Mouse', movement, this.jwt()).map((response: Response) => response.json());
  }

  
  // private helper methods

  private jwt() {
    // create authorization header with jwt token
    let currentUser = JSON.parse(localStorage.getItem('currentUser'));
    let headers = new Headers({ 'UserName': currentUser.username });
    return new RequestOptions({ headers: headers });
    
  }
}
