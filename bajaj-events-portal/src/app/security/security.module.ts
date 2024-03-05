import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './components/login/login.component';

import { HttpClientModule } from "@angular/common/http";
import { SecurityService } from './services/security.service'; 
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    LoginComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule
  ],
  exports:[
  ],
  providers:[
    SecurityService
  ]
})
export class SecurityModule { }