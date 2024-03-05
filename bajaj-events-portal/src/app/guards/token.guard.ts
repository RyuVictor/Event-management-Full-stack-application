import { inject } from '@angular/core';
import { CanActivateFn, Router} from '@angular/router';

import { AuthStorageService } from '../shared/services/auth-storage.service';

export const tokenGuard: CanActivateFn = (route, state) => {
  const _storageService = inject(AuthStorageService);
  const _router = inject(Router);
  const accessToken = _storageService.getAccessToken('accessToken');
  if(accessToken) return true;
  _router.navigate(['/login'],{
    queryParams : {
      returnUrl : state.url
    }
  })
  return false;
};
