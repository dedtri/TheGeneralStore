import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Login } from 'src/app/models/login.model';
import { AuthGuard } from 'src/app/services/authguard.service';

@Component({
  selector: 'app-userprofile',
  templateUrl: './userprofile.component.html',
  styleUrls: ['./userprofile.component.css']
})
export class UserprofileComponent {

  credentials: any = {};

  constructor(private authService: AuthGuard, private route: ActivatedRoute, private router: Router) {

    this.initialize();

  }

  initialize() {
    this.route.paramMap.subscribe({
      next: (params) => {
        const id = params.get('id');
        if (id) {
          //Call API
          this.authService.getUserById(id)
            .subscribe({
              next: (response) => {
                  this.credentials.id = response.id
                  this.credentials.firstname = response.firstName
                  this.credentials.lastname = response.lastName
              }
            });
        }
      }
    })
  }

  goMyOrders() {
    this.router.navigateByUrl('dashboard/' + this.credentials.id + '/myorders')
  }
}
