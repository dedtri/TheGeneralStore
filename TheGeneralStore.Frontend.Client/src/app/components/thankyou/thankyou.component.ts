import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-thankyou',
  templateUrl: './thankyou.component.html',
  styleUrls: ['./thankyou.component.css']
})
export class ThankyouComponent {

  constructor(private router: Router) {
  
  }

  goHome() {
this.router.navigateByUrl("/");
  }

  logOut = () => {
  this.router.navigateByUrl("/");
  localStorage.removeItem("jwt");
  localStorage.removeItem("credentials");
  localStorage.removeItem("refreshToken");
  // this.credentials.role = "";
}
}
