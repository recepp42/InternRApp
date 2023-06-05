import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { LanguageItem } from '../entities/languageItem';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { TranslateModule } from '@ngx-translate/core';
@Component({
  selector: 'intern-rapp-language-popup-choice',
  standalone: true,
  imports: [CommonModule,MatFormFieldModule,MatSelectModule,ReactiveFormsModule,TranslateModule,MatDialogModule,MatIconModule,MatDividerModule],
  templateUrl: './language-popup-choice.component.html',
  styleUrls: ['./language-popup-choice.component.scss'],
})
export class LanguagePopupChoiceComponent {
  @Input() languageItems:LanguageItem[]|undefined
  constructor(public dialogRef: MatDialogRef<LanguagePopupChoiceComponent>){}
  languageChoiceForm=new FormGroup({
    languageName: new FormControl('',[Validators.required]),
    })
    closeDialog(save: boolean){
     
      let item:LanguageItem|undefined
      if(save){
        this.languageItems?.forEach(element => {
          if(element.id===Number(this.languageChoiceForm.controls['languageName'].value)){
            item=element;
            
          }
        });
   
      }
    this.dialogRef.close(save?item:undefined)
    }
    
}
