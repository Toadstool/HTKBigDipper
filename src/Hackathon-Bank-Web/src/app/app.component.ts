import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Hackathon Bank';
  events = [];
  captureCoordinate(event){

    var now = new Date();
    this.events.push({
      time: now,
      x: event.x,
      y: event.y,
      type: event.type
    });

    //console.log(event);
  }
  sent(){
    
  }

}
