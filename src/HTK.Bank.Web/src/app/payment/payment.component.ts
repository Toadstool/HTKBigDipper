import {Component } from '@angular/core';
import { Router } from "@angular/router";
import { PaymentService } from './../_services/index'

@Component({
  selector: 'payment-component',
  templateUrl: './payment.component.html'
 })
export class PaymentComponent {

  events = [];

  constructor(private router: Router, private paymentService: PaymentService) { } 

  captureCoordinate(event) {

    var now = new Date();
    this.events.push({
      time: now,
      x: event.x,
      y: event.y,
      type: event.type
    });
    
  }

  send() {

    console.log(this.events.length);

    this.paymentService.testMovements(this.events).subscribe(x => {

      this.router.navigate(['payment-confirm', { verified: x.Verified, score: x.Score }]);

      console.log(x);
    });
    
    return false;


    
  }



}
