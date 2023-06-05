import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatInputModule } from '@angular/material/input';
import { HttpClientModule } from '@angular/common/http';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'intern-rapp-delete-popup',
  standalone: true,
  imports: [CommonModule,MatDialogModule,MatDividerModule,TranslateModule,ReactiveFormsModule,MatFormFieldModule,MatAutocompleteModule,MatInputModule,HttpClientModule,MatChipsModule,MatIconModule],
  templateUrl: './delete-popup.component.html',
  styleUrls: ['./delete-popup.component.scss'],
})
export class DeletePopupComponent {
  @Input() title!:string

  constructor(public dialogRef: MatDialogRef<DeletePopupComponent>){

  }
  closeDialog(hasData: boolean) {
    console.log(hasData)
    this.dialogRef.close(hasData?this.title:undefined)
  }

}
