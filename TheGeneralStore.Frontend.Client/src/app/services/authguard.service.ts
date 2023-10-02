import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';
import { Login } from '../models/login.model';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthenticatedResponse } from '../models/authenticatedResponse.model';
import { MatDialog } from '@angular/material/dialog';
import { LoginComponent } from '../components/login/login/login.component';
@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate  {

  private readonly url = environment.serverURL + 'Auth/';

  constructor(public dialog: MatDialog, private router:Router, private jwtHelper: JwtHelperService, private http: HttpClient){}
  
  async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const token = localStorage.getItem("jwt");

    if (token && !this.jwtHelper.isTokenExpired(token)){
      return true;
    }

    const isRefreshSuccess = await this.tryRefreshingTokens(token); 
    if (!isRefreshSuccess) { 
      if (this.router.url === ('/'))
      {
        this.router.navigateByUrl('/');
      } 
      const dialogRef = this.dialog.open(LoginComponent, {
        panelClass: 'transparent-dialog'
      });
    }

    return isRefreshSuccess;
  }

  private async tryRefreshingTokens(token: string | null): Promise<boolean> {
    // Try refreshing tokens using refresh token
    const refreshToken: string | null = localStorage.getItem("refreshToken");
    if (!token || !refreshToken) { 
      return false;
    }
    
    const credentials = JSON.stringify({ accessToken: token, refreshToken: refreshToken });
    let isRefreshSuccess: boolean;

    const refreshRes = await new Promise<AuthenticatedResponse>((resolve, reject) => {
      this.http.post<AuthenticatedResponse>("https://localhost:5001/api/token/refresh", credentials, {
        headers: new HttpHeaders({
          "Content-Type": "application/json"
        })
      }).subscribe({
        next: (res: AuthenticatedResponse) => resolve(res),
        error: (_) => { reject; isRefreshSuccess = false;}
      });
    });

    localStorage.setItem("jwt", refreshRes.token);
    localStorage.setItem("refreshToken", refreshRes.refreshToken);
    isRefreshSuccess = true;

    return isRefreshSuccess;
  }


  getUser(email: string): Observable<Login> {
    return this.http.get<Login>(this.url + email);
  }

  getUserById(id: number | string): Observable<Login> {
    return this.http.get<Login>(this.url + 'id/' + id);
  }

  register(addUserRequest: Login): Observable<Login> {
    return this.http.post<Login>(this.url + 'register', addUserRequest);
  }

  deleteUser(email: string): Observable<Login> {
    return this.http.delete<Login>(this.url + email);
  }

  updateUser(id: string, updateUserRequest: Login): Observable<Login> {
    return this.http.put<Login>(this.url + id, updateUserRequest);
  }

  getUsers(): Observable<Login[]> {
    return this.http.get<Login[]>(this.url);
  }
}