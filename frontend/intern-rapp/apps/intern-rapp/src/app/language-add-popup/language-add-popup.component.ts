import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDividerModule } from '@angular/material/divider';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { HttpClientModule } from '@angular/common/http';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { LanguageItem } from '../entities/languageItem';
import { CreateLanguage } from '../entities/createLanguage';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'intern-rapp-language-add-popup',
  standalone: true,
  imports: [CommonModule,MatDialogModule,MatDividerModule,TranslateModule,ReactiveFormsModule,MatFormFieldModule,MatInputModule,HttpClientModule,MatIconModule],
  templateUrl: './language-add-popup.component.html',
  styleUrls: ['./language-add-popup.component.scss'],
})
export class LanguageAddPopupComponent implements OnInit{
  addLanguageForm:FormGroup|undefined
  constructor(public dialogRef: MatDialogRef<LanguageAddPopupComponent>) {
  
  }
  ngOnInit(): void {
    this.addLanguageForm = new FormGroup({
      languageName: new FormControl('', [Validators.required]),
      languageCode: new FormControl('', [Validators.required]),
    });
  }
  
  closeDialog(save:boolean){
    let data: CreateLanguage|undefined
    if(save){
      data = {
        name: this.addLanguageForm?.controls['languageName'].getRawValue(),
        code : this.addLanguageForm?.controls['languageCode'].getRawValue(),
      };
    }

    this.dialogRef.close(save?data:undefined)
  }
  

}
