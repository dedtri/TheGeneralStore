import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Component, ElementRef, OnDestroy, Renderer2 } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { catchError, of, tap, throwError } from 'rxjs';
import { AuthenticatedResponse } from 'src/app/models/authenticatedResponse.model';
import { Login } from 'src/app/models/login.model';
import { AuthGuard } from 'src/app/services/authguard.service';
import { Location } from '@angular/common'

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnDestroy {

  addLoginRequest: Login = { id: 0, email: '', password: '', role: '', firstName: '', lastName: ''};
  
  loginForm: FormGroup | any;
  submitted = false;
  errorMessage: string = "";
  invalidRegister: boolean = true;
  
  
  constructor(private authguardService: AuthGuard, private router: Router, private formBuilder: 
    FormBuilder, private http: HttpClient, private location: Location) {

this.buildValidator();

  }

  ngOnDestroy() {
    const event = new CustomEvent('userLoggedIn');
    window.dispatchEvent(event);
  }

  buildValidator() {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.minLength(4)]],
      password: ['', [Validators.minLength(6), Validators.required]],
      firstname: ['', [Validators.required, Validators.minLength(2)]],
      lastname: ['', [Validators.required, Validators.minLength(2)]],
    });
  }
  
  registerUser() {
    this.submitted = true;
    if (this.loginForm.valid) {
      if (this.addLoginRequest) {
  this.addLoginRequest = {
  ...this.addLoginRequest,
  email: this.loginForm.get('email')?.value,
  password: this.loginForm.get('password')?.value,
  firstName: this.loginForm.get('firstname')?.value,
  lastName: this.loginForm.get('lastname')?.value,  
  };
  }
    this.authguardService.register(this.addLoginRequest).pipe(
      tap(logins => {
        this.errorMessage = '';
      }),
      catchError((error: HttpErrorResponse) => {
        
        if (error.status === 500) {
          this.errorMessage = 'Email already exists.'; 
        } else {
          this.errorMessage = 'Http failure response';
        }
        this.invalidRegister = true;
        return of(error);
      })
    )
    .subscribe({
      next: (members) => {
        if (this.loginForm.valid && this.errorMessage === '') {
          this.invalidRegister = false;
          this.http.post<AuthenticatedResponse>("https://localhost:7113/api/auth/login", this.addLoginRequest, {
            headers: new HttpHeaders({ "Content-Type": "application/json"})
          })
          .subscribe({
            next: (response: AuthenticatedResponse) => {
              const token = response.token;
              const refreshToken = response.refreshToken;
              localStorage.setItem("jwt", token); 
              localStorage.setItem("refreshToken", refreshToken);
              this.authguardService.getUser(this.addLoginRequest.email)
              .subscribe({
              next: (response) => {
              this.addLoginRequest = response;

              let storedCredentials = {
                role: this.addLoginRequest.role,
                id: this.addLoginRequest.id
              };
    
              localStorage.setItem("credentials", JSON.stringify(storedCredentials));
              }
            });  
            }
          })
        }
      }
    });
  } else
  {
    for (const key in this.loginForm.controls) {
      if (this.loginForm.controls.hasOwnProperty(key)) {
        const control = this.loginForm.get(key);
        if (control && control.invalid) {
          console.log(key, control.errors);
        }
      }
    }
  }
  }

  logOut = () => {
    localStorage.removeItem("jwt");
    localStorage.removeItem("credentials");
    this.addLoginRequest.role = "";
    this.router.navigateByUrl("/");
  }

  goBack() {
    this.location.back();
  }

}
