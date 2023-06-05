import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LocationItem } from '../entities/locationItem';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'intern-rapp-location-update-popup',
  standalone: true,
  imports: [CommonModule,MatDialogModule,MatDividerModule,TranslateModule,ReactiveFormsModule,MatFormFieldModule,MatInputModule,MatIconModule],
  templateUrl: './location-update-popup.component.html',
  styleUrls: ['./location-update-popup.component.scss'],
})
export class LocationUpdatePopupComponent implements OnInit{
  @Input() data!: LocationItem
  updateForm=new FormGroup({
    locationCity: new FormControl('',[Validators.required]),
    locationStreet: new FormControl('',[Validators.required]),
    locationHouseNumber: new FormControl(1,[Validators.required]),
    locationZipCode: new FormControl('',[Validators.required]),
  })

  constructor(public dialogRef: MatDialogRef<LocationUpdatePopupComponent>){
  }

  ngOnInit(): void {
    this.updateForm.controls.locationCity.patchValue(this.data?.city)
    this.updateForm.controls.locationStreet.patchValue(this.data?.streetname)
    this.updateForm.controls.locationHouseNumber.patchValue(this.data?.housenumber) 
    this.updateForm.controls.locationZipCode.patchValue(this.data?.zipcode)
    console.log(this.data);
  }
  

    close(){
      this.data.city=this.updateForm.controls.locationCity.getRawValue();
      this.data.streetname=this.updateForm.controls.locationStreet.getRawValue();
      this.data.housenumber=this.updateForm.controls.locationHouseNumber.getRawValue();
      this.data.zipcode=this.updateForm.controls.locationZipCode.getRawValue();

      this.dialogRef.close(this.updateForm.valid?this.data:undefined) //close de dialog met gekregen data of met undefined? 
    }
}
