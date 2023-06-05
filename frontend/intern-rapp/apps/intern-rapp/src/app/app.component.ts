 import { NxWelcomeComponent } from './nx-welcome.component';
import { RouterModule } from '@angular/router';
import { ChangeDetectionStrategy, Component, Inject, OnInit, inject } from '@angular/core';
import {MatToolbarModule} from '@angular/material/toolbar';
import { NavbarComponent } from './navbar/navbar.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { UnitListComponent } from './unit-list/unit-list.component';
import { TranslateService } from '@ngx-translate/core';
import { LOCALE_ID} from '@angular/core';
import { LanguageOption } from './enums/languageDropdownOptions';
@Component({
  standalone: true,
  imports: [NxWelcomeComponent, RouterModule,MatToolbarModule,NavbarComponent, ReactiveFormsModule,HttpClientModule,UnitListComponent,
  ],
  selector: 'intern-rapp-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class AppComponent{

  title = 'intern-rapp';
  constructor( ){}


}
