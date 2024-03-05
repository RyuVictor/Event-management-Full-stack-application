import { Component, OnInit } from '@angular/core'; 
import { Router } from '@angular/router';

import { AuthStorageService } from '../../../shared/services/auth-storage.service';
import { __setFunctionName } from 'tslib';

@Component({
  selector: 'bajaj-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.css'
})
export class NavBarComponent implements OnInit {
  constructor(private _router : Router, private _storageService : AuthStorageService){}
  logo : string = "../images/bajaj-logo2.png" 
  isAuthenticated : boolean = false;
  role : string; 
  ngOnInit(): void {
    this._router.events.subscribe({
        next : event => {
          this.isAuthenticated = this._storageService.isAuthenticated;
          this.role = this._storageService.getRole("role");
        }
        
    })
  }
  logout():void{
    this.isAuthenticated = false;
    this._storageService.clearStorage()
  }
}
