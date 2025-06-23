// import { Injectable } from '@angular/core';
// import {
//   HttpEvent,
//   HttpHandler,
//   HttpInterceptor,
//   HttpRequest
// } from '@angular/common/http';
// import { Observable, timer } from 'rxjs';
// import { finalize, switchMap, takeUntil } from 'rxjs/operators';
// import { LoadingService } from './services/loading.service';

// @Injectable()
// export class LoaderInterceptor implements HttpInterceptor {
//   constructor(private loaderService: LoadingService) {}

//   intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
//     const delayedLoading$ = timer(300).pipe(
//       switchMap(() => {
//         this.loaderService.show();
//         return next.handle(req);
//       })
//     );

//     return delayedLoading$.pipe(
//       finalize(() => this.loaderService.hide())
//     );
//   }
// }
