import { bootstrapApplication } from '@angular/platform-browser';
import {
  provideRouter,
  withEnabledBlockingInitialNavigation,
} from '@angular/router';
import { AppComponent } from './app/app.component';
import { appRoutes } from './app/app.routes';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {
  APP_INITIALIZER,
  Inject,
  LOCALE_ID,
  importProvidersFrom,
  inject,
} from '@angular/core';
import {
  HttpClient,
  HttpClientModule,
  HTTP_INTERCEPTORS,
} from '@angular/common/http';
import { MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import {
  TranslateLoader,
  TranslateModule,
  TranslateService,
} from '@ngx-translate/core';
import { customTranslate } from './app/loader/customTranslate';
import { AcceptHeaderInterceptor } from './app/interceptors/accept-header.interceptor';
import { AppInitializerFactory } from './app/appInitializerFactory';
import { AuthGuard } from './guards/authguard';

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(appRoutes, withEnabledBlockingInitialNavigation()),
    importProvidersFrom(
      BrowserAnimationsModule,
      BrowserAnimationsModule,
      HttpClientModule,
      MatTableModule,
      MatFormFieldModule,
      MatPaginatorModule,
      MatSortModule,
      TranslateModule.forRoot({
        loader: {
          provide: TranslateLoader, // Main provider for loader
          useClass: customTranslate, // Custom Loader
          deps: [HttpClient], // Dependencies which helps serving loader
        },
      })
    ),
    AuthGuard,
    {
      provide: LOCALE_ID,
      useFactory: (translateService: TranslateService) =>
        translateService.currentLang ?? 'Nl',
      deps: [TranslateService],
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AcceptHeaderInterceptor,
      multi: true,
    },
    {
      provide: APP_INITIALIZER,
      useFactory: AppInitializerFactory,
      deps: [TranslateService],
      multi: true,
    },
  ],
}).catch((err) => console.error(err));

