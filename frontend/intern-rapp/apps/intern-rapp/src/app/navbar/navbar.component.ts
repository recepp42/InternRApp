import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatToolbarModule} from '@angular/material/toolbar';
import { RouterModule } from '@angular/router';
import { UnitListComponent } from '../unit-list/unit-list.component';
import { LanguageListComponent } from '../language-list/language-list.component';
import { LanguageDropdownComponent } from '../language-dropdown/language-dropdown.component';
import { LanguageService } from '../services/language.service';
import { BehaviorSubject, Observable, tap, combineLatest, switchMap } from 'rxjs';
import { TranslateModule } from '@ngx-translate/core';
import { AuthService } from '../services/auth-service.service';
@Component({
  selector: 'intern-rapp-navbar',
  standalone: true,
  imports: [CommonModule,MatToolbarModule,RouterModule,UnitListComponent,LanguageListComponent,LanguageDropdownComponent,TranslateModule],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent {
  constructor(public authService: AuthService) { }
 public  logout() {
    this.authService.logout()
  }
}
