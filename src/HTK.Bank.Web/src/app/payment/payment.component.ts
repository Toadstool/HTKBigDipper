import {Component } from '@angular/core';
import { Router } from "@angular/router";

@Component({
  selector: 'payment-component',
  templateUrl: './payment.component.html'
 })
export class PaymentComponent {

  events = [];

  constructor(private router: Router) { } 

  captureCoordinate(event) {

    var now = new Date();
    this.events.push({
      time: now,
      x: event.x,
      y: event.y,
      type: event.type
    });

    //console.log(event);
  }





  send() {
    this.router.navigate(['payment-confirm']); 

  }



}
