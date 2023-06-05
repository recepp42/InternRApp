import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FilterType } from '../enums/filterType';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { Filter } from '../entities/filter';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { TranslateModule } from '@ngx-translate/core';
import {  MatOptionModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';

@Component({
  selector: 'intern-rapp-filter',
  standalone: true,
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatOptionModule,
    ReactiveFormsModule,
    MatSelectModule,
    MatInputModule,
    MatIconModule,
    TranslateModule,
  ],
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.scss'],
})
export class FilterComponent implements OnInit {
  @Input() filters: Filter[] = [];
  @Output() filtered = new EventEmitter<object>();
  public filterForm = new FormGroup({});
  public filtertype: typeof FilterType;
  constructor() {
    this.filtertype = FilterType;
  }
  ngOnInit(): void {
    
    this.filters.forEach((x) => {
      this.filterForm.addControl(x.name, new FormControl(x.defaultValue));
    });

 
  }
  filter() {
    this.filtered.emit(this.filterForm.getRawValue());
  }
  // compareValues(o1: any, o2: any) {
  //   debugger
  //   console.log("in comparing")
  //   return o1===o2
  // }
}
