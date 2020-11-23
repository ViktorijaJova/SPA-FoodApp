import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { AuthService } from './auth.service';


@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

    constructor(private router: Router,
                private authService : AuthService) {}


    canActivate(next: ActivatedRouteSnapshot,
                state: RouterStateSnapshot) {

        if(localStorage.getItem("token") != null) {
            let roles = next.data['permittedRoles'] as Array<string>

            if (roles) {
                if(this.authService.roleMatch(roles)) {
                    return true;
                } else {
                    this.router.navigate(["/forbidden"])
                    return false;
                }                
            } else {
                this.router.navigate(["/user/login"])
            }
        }            
    }
}