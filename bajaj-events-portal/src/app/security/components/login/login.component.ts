import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';

import { AuthResponse } from '../../models/auth-response';
import { User } from '../../models/user';

import { SecurityService } from '../../services/security.service';
import { AuthStorageService } from '../../../shared/services/auth-storage.service';


@Component({
  selector: 'bajaj-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent  implements OnInit, OnDestroy{
  private _returnUrl : string;
  errorMessage : string = "";
  constructor(private _securityservice : SecurityService, private _router : Router, private _storageService: AuthStorageService, private _activatedRoute :  ActivatedRoute){}
  title : string = "Bajaj EP Authentication Window";
  user : User = new User();
  authResponse : AuthResponse;
  private _securityServiceSubscription : Subscription;
  onAuthentication():void{
    console.log(this.user);
    this._securityservice.authenticateUserCredential(this.user).subscribe({
      next : data => {
        this.authResponse = data;
        
        console.log(this.authResponse);
        if(this.authResponse.isAuthenticated){
            this._storageService.storeAccessToken(this.authResponse.token,this.authResponse.roleName,this.authResponse.email)
            this._storageService.storeRefreshToken(this.authResponse.refreshToken);
            if(this._returnUrl) this._router.navigate([this._returnUrl]);
            else this._router.navigate(['/home']);
        }
      },
      error : err => {
        this.errorMessage = err.error.message,
        setTimeout(() => {
          this.errorMessage = "";
        }, 5000);
      }
      
    })
  }
  ngOnInit(): void {
    this._returnUrl = this._activatedRoute.snapshot.queryParams['returnUrl'];
  }
  ngOnDestroy(): void {
    if(this._securityServiceSubscription) this._securityServiceSubscription.unsubscribe();
  }
}
