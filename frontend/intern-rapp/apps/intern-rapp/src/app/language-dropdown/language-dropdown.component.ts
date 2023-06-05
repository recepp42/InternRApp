import {  Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatOptionModule, MatOptionSelectionChange } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { LanguageOption } from '../enums/languageDropdownOptions';
import { TranslateService,TranslateModule } from '@ngx-translate/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { map, tap } from 'rxjs';
@Component({
  selector: 'intern-rapp-language-dropdown',
  standalone: true,
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatOptionModule,
    MatSelectModule,
    TranslateModule,
    ReactiveFormsModule,
  ],
  templateUrl: './language-dropdown.component.html',
  styleUrls: ['./language-dropdown.component.scss'],
})
export class LanguageDropdownComponent implements OnInit {
  constructor(public translateService: TranslateService) {}
  public availableLanguages = Object.keys(LanguageOption) as [];
  public dropDownForm: FormGroup | undefined;
  ngOnInit(): void {
    this.dropDownForm = new FormGroup({
      language: new FormControl(this.translateService.currentLang, [
        Validators.required,
      ]),
    });

 
  }

  private capitalize = (s: string) => s[0].toUpperCase() + s.slice(1);
  languageChanged(event: MatOptionSelectionChange) {
    if (!event.isUserInput) return;
    this.translateService.use(event.source.value);
    this.translateService.currentLang = event.source.value;
  //  this.dropDownForm?.controls['language'].patchValue(event.source.value);
  }

}
