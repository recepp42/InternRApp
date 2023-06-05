import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { CreateLocation } from '../entities/createLocation';
import { MatDividerModule } from '@angular/material/divider';
import { TranslateModule } from '@ngx-translate/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'intern-rapp-location-add-popup',
  standalone: true,
  imports: [CommonModule,MatDividerModule,TranslateModule,MatDialogModule,MatFormFieldModule,ReactiveFormsModule, MatIconModule,MatInputModule],
  templateUrl: './location-add-popup.component.html',
  styleUrls: ['./location-add-popup.component.scss'],
})
export class LocationAddPopupComponent {
  addForm = new FormGroup({
    locationCity: new FormControl('',[Validators.required]),
    locationStreet: new FormControl('',[Validators.required]),
    locationHouseNumber: new FormControl(null,[Validators.required]),
    locationZipCode: new FormControl('',[Validators.required]),
    })

  constructor(public dialogRef: MatDialogRef<LocationAddPopupComponent>) {
  }
  
  closeDialog(save:boolean){
    let data: CreateLocation|undefined
    if(save){
      data={city:this.addForm.controls.locationCity.getRawValue(),
        streetname:this.addForm.controls.locationStreet.getRawValue(),
        housenumber:this.addForm.controls.locationHouseNumber.getRawValue(),
        zipcode:this.addForm.controls.locationZipCode.getRawValue()
      }
    }

    this.dialogRef.close(save?data:undefined)
  }
}
