import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ProductService } from 'src/app/services/product.service';
import { LoginComponent } from '../login/login/login.component';
import { Login } from 'src/app/models/login.model';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
   
    credentials: Login | any = {};
    cartItems = 0;
  
    constructor(public dialog: MatDialog, private productService: ProductService, private jwtHelper: JwtHelperService, private router: Router) {
      
      this.updateUserInfo();

      let cartData = localStorage.getItem('localCart');
      if(cartData) {
        this.cartItems = JSON.parse(cartData).length;
      }
      this.productService.cartData.subscribe((res) => {
        this.cartItems = res.length;
      })

      window.addEventListener('userLoggedIn', this.updateUserInfo.bind(this));
    }

    isRegisterPage = (): boolean => {
      if (this.router.url.includes('/register'))
    {
       return true;
    }
  return false;
    }

    updateUserInfo() {
      let storedCredentialsString = localStorage.getItem("credentials");
      if (storedCredentialsString)
      {
        this.credentials =  JSON.parse(storedCredentialsString);
      }
    }

    isUserAuthenticated = (): boolean => {
      const token = localStorage.getItem("jwt");
      if (token && !this.jwtHelper.isTokenExpired(token)){
        return true;
      }
      localStorage.removeItem("jwt");
      localStorage.removeItem("credentials");
      localStorage.removeItem("refreshToken");
      return false;
    }

    login() {
      const dialogRef = this.dialog.open(LoginComponent, {
        panelClass: 'transparent-dialog'
      });
    }
  
    logOut = () => {
      this.router.navigateByUrl("/");
    localStorage.removeItem("jwt");
    localStorage.removeItem("credentials");
    localStorage.removeItem("refreshToken");
    // this.credentials.role = "";
  }
}
