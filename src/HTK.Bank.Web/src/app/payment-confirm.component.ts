import {Component } from '@angular/core';

@Component({
  selector: 'payment-confirm-component',
  templateUrl: './payment-confirm.component.html'
 })
export class PaymentConfirmComponent {

  events = [];

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

  }



}
