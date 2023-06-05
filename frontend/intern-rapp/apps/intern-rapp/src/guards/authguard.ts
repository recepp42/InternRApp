import { Injectable } from '@angular/core';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  Router,
} from '@angular/router';
import { AuthService } from '../app/services/auth-service.service';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
      if (this.authService.IsAuthenticated) {
      return true; // Allow access to the private route
    } else {
      this.router.navigateByUrl('/login'); // Redirect to the login page if the user is not authenticated
      return false; // Deny access to the private route
    }
    }
  
}
