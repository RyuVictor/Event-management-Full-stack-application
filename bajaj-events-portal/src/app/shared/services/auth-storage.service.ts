import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthStorageService {
  isAuthenticated : boolean = false;
  storeAccessToken(accessToken: string, role: string,email:string): void {
    localStorage.setItem("accessToken", accessToken);
    localStorage.setItem("role", role);
    localStorage.setItem("userName", email);
    this.isAuthenticated = true;
  }
  storeRefreshToken(refreshToken: string): void {
    localStorage.setItem("refreshToken", refreshToken);
  }
  getRole(key: string): string {
    return localStorage.getItem(key) as string;
  }
  getUserName(key: string): string {
    return localStorage.getItem(key) as string;
  }
  getAccessToken(accessTokenKey: string): string {
    return localStorage.getItem(accessTokenKey) as string;
  }
  getRefreshToken(refreshTokenKey: string): string {
    return localStorage.getItem(refreshTokenKey) as string;
  }
  clearStorage(): void {
    this.isAuthenticated = false;
    localStorage.clear();
  }
}
