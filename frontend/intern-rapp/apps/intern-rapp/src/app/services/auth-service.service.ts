import { L } from '@angular/cdk/keycodes';
import { Injectable, OnInit } from '@angular/core';
import { loginRequestDto } from '../entities/loginRequestDto';
import { HttpClient } from '@angular/common/http';
import { APIConfiguration } from '../configurations/APIConfiguration';
import { catchError, of, take, tap } from 'rxjs';
import { LoginResponseDto } from '../entities/loginResponseDto';
import { User } from '../entities/user';
import { Router } from '@angular/router';
import { RegistrationRequest } from '../entities/registrationRequest';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  public currentUser!:User
  public IsAuthenticated = localStorage.getItem('access_token') ? true : false;
  constructor(private http: HttpClient, private router: Router) {
    if (this.IsAuthenticated) {
      this.http
        .get<User>(`${APIConfiguration.baseString}/api/login`)
        .pipe(
          tap((data) => {
            console.log(data);
            this.currentUser=data
            this.router.navigateByUrl('/internships');
          }),
          take(1),
          catchError((error) => {
            this.logout()
            return of([])
          })
        ).subscribe();
    }
  }
 
  
   public login(loginRequest: loginRequestDto) {
      return this.http.post<LoginResponseDto>(`${APIConfiguration.baseString}/api/login`, {
        email: loginRequest.email,
        password: loginRequest.password
      }).pipe(tap(data => {
        
        localStorage.setItem('access_token', data.access_token)
        this.IsAuthenticated = true
        this.currentUser = data.user
        this.router.navigateByUrl('/internships');
      }), take(1))
    }
  
  public logout() {
    localStorage.removeItem('access_token')
    this.IsAuthenticated = false
    this.router.navigateByUrl("/login")
  }
  public register(registrationRequest:RegistrationRequest) {
    return this.http
      .post<LoginResponseDto>(`${APIConfiguration.baseString}/api/login/registration`, {
        email: registrationRequest.email,
        password: registrationRequest.password,
        submitPassword: registrationRequest.submitPassword,
      })
      .pipe(
        take(1)
      );
  }
}
