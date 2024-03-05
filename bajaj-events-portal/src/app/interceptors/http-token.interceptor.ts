import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { BehaviorSubject, Observable, catchError, switchMap } from 'rxjs';

import { AuthStorageService } from "../shared/services/auth-storage.service";
import { SecurityService } from "../security/services/security.service";

@Injectable()
export class HttpTokenInterceptor implements HttpInterceptor {
  constructor(private _securityService: SecurityService, private _storageService: AuthStorageService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (!req.url.includes('/api/users')) {
      const headers = req.headers
        .set('Content-Type', 'application/json')
        .set("Authorization", `Bearer ${this._storageService.getAccessToken("accessToken")}`);
      const authReq = req.clone({ headers });
      return next.handle(authReq).pipe(catchError((error) => {
        if (error instanceof HttpErrorResponse && error.status === 401) {
          console.log('401 Error Has come!')
          return this.handle401Error(authReq, next);
        } else {
          return next.handle(req);
        }
      }));
    } else {
      return next.handle(req);
    }
  }
  private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  private handle401Error(request: HttpRequest<any>, next: HttpHandler) {
    return this._securityService.getRefreshedAccessToken({
      accessToken: this._storageService.getAccessToken('accessToken'),
      refreshToken: this._storageService.getRefreshToken('refreshToken'),
    }).pipe(
      switchMap((token: any) => {
        console.log(token);
        this.refreshTokenSubject.next(token.token);
        this._storageService.storeAccessToken(token.token, this._storageService.getRole('role'), this._storageService.getUserName('userName'));
        return next.handle(this.setTokenHeader(request, token.token));
      }));
  }

  private setTokenHeader(request: HttpRequest<any>, token: string) {
    return request.clone({
      setHeaders: {
        'Authorization': `Bearer ${token}`
      },
    });
  }
}