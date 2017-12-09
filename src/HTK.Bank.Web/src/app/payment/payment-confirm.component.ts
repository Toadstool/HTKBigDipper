import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute} from "@angular/router";

@Component({
  selector: 'payment-confirm-component',
  templateUrl: './payment-confirm.component.html'
})
export class PaymentConfirmComponent implements OnInit {

  verification="";
  score="0%";

  constructor(
    private route: ActivatedRoute,
    private router: Router) { }


  ngOnInit() {

    if (this.route.snapshot.params['verification']==true)
    {
      this.verification = "completed";
    }
    else
    {
      this.verification = "you have failed verification process";
    }
    
    this.score = this.route.snapshot.params['score']+"%";
   
  }

}

