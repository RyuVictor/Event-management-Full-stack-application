import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

import { User } from '../models/user';
import { AuthResponse } from '../models/auth-response';

import { RefreshToken } from '../models/refresh-token-model';

@Injectable()
export class SecurityService {

  constructor(private _httpClient : HttpClient) { }
  private _serviceBaseUrl : string = "https://localhost:7081/api/Users";
  authenticateUserCredential(user : User):Observable<AuthResponse>{
    return this._httpClient.post<AuthResponse>(`${this._serviceBaseUrl}/login`,user)
  }
  getRefreshedAccessToken(refreshTokenModel : RefreshToken):Observable<AuthResponse>{
    return this._httpClient.post<AuthResponse>(`${this._serviceBaseUrl}/refresh`,refreshTokenModel)
  }
}

