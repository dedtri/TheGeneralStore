import { Location } from '@angular/common';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { AuthenticatedResponse } from 'src/app/models/authenticatedResponse.model';
import { Login } from 'src/app/models/login.model';
import { AuthGuard } from 'src/app/services/authguard.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  invalidLogin: boolean = false;
  invalidConn: boolean = false;
  credentials: Login = { id: 0, email: '', password: '', role: '', firstName: '', lastName: ''};

  constructor(public dialogRef: MatDialogRef<LoginComponent>, private router: Router, private http: HttpClient, private authguard: AuthGuard, private location: Location) { }

  login = (form: NgForm) => {
    if (form.valid) {
      this.http.post<AuthenticatedResponse>("https://localhost:7113/api/auth/login", this.credentials, {
        headers: new HttpHeaders({ "Content-Type": "application/json" })
      })
        .subscribe({
          next: (response: AuthenticatedResponse) => {
            const token = response.token;
            const refreshToken = response.refreshToken;
            localStorage.setItem("jwt", token);
            localStorage.setItem("refreshToken", refreshToken);
            this.invalidLogin = false;

            this.authguard.getUser(this.credentials.email)
              .subscribe({
                next: (response) => {
                  this.credentials = response;

                  let storedCredentials = {
                    role: this.credentials.role,
                    id: this.credentials.id
                  };

                  localStorage.setItem("credentials", JSON.stringify(storedCredentials));

                  const event = new CustomEvent('userLoggedIn');
                  window.dispatchEvent(event);

                  this.dialogRef.close();
                }
              });
          },
          error: (err: HttpErrorResponse) => {
            if (err.status == 401) {
              this.invalidConn = false;
              this.invalidLogin = true;
            }
            else {
              this.invalidLogin = false;
              this.invalidConn = true;
            }
          }
        })
    }
  }

  close() {
    this.dialogRef.close();
  }
}
