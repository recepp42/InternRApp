import { Inject, Injectable, LOCALE_ID, Optional } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class AcceptHeaderInterceptor implements HttpInterceptor {

  constructor(@Optional() @Inject(LOCALE_ID) private localId: string) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle( request.clone({
      setHeaders:{
        'accept-language': this.localId,
        "authorization":`Bearer ${localStorage.getItem('access_token')}`
      }
    }));
  }
}
