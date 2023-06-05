import { B } from '@angular/cdk/keycodes';
import { Route } from '@angular/router';
import { LanguageListComponent } from './language-list/language-list.component';
import { LocationListComponent } from './location-list/location-list.component';
import { UnitListComponent } from './unit-list/unit-list.component';
import { InternShipListComponent } from './intern-ship-list/intern-ship-list.component';
import { InternshipAddComponent } from './internship-add/internship-add.component';
import { InternshipDetailItem } from './entities/internshipDetailItem';
import { AuthGuard } from '../guards/authguard';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';

export const appRoutes: Route[] = [
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'register',
    component:RegistrationComponent
  },
  {
    path: 'departments',
    component: UnitListComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'languages',
    component: LanguageListComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'internships',
    component: InternShipListComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'internships/create',
    component: InternshipAddComponent,
    data: { internshipTobeUpdated: undefined },
    canActivate: [AuthGuard],
  },
  {
    path: 'locations',
    component: LocationListComponent,
    canActivate: [AuthGuard],
  },
  {
    path: '**',
    redirectTo: '/departments',
    pathMatch: 'full',
  },
];
