import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { MatIconModule } from '@angular/material/icon';
import { MatOptionModule } from '@angular/material/core';
import { Observable, map, of } from 'rxjs';
import { LanguageService } from '../services/language.service';
import { ResourceItemPagingResponse } from '../entities/resourceItemPagingResponse';
import { LanguageItem } from '../entities/languageItem';
import { DepartmentService } from '../services/department.service';
import { DepartmentItem } from '../entities/departmentItem';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { ExportInternshipOptions } from '../entities/exportInternshipOptions';
import { MatTooltipModule } from '@angular/material/tooltip';

@Component({
  selector: 'intern-rapp-export-popup-options',
  standalone: true,
  imports: [
    CommonModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatDividerModule,
    MatIconModule,
    TranslateModule,
    MatOptionModule,
    MatInputModule,
    MatSelectModule,
    MatTooltipModule
  ],
  templateUrl: './export-popup-options.component.html',
  styleUrls: ['./export-popup-options.component.scss'],
})
export class ExportPopupOptionsComponent implements OnInit {
  exportOptionsForm: FormGroup | undefined;
  public languageObs$ = new Observable<LanguageItem[]>();
  public unitObs$ = new Observable<DepartmentItem[]>();
  public schoolYearObs$ = new Observable<string[]>();
  constructor(
    private languageService: LanguageService,
    private unitService: DepartmentService,
    public dialogRef: MatDialogRef<ExportPopupOptionsComponent>
  ) {}

  ngOnInit(): void {
    this.exportOptionsForm = new FormGroup({
      languageId: new FormControl('', [Validators.required]),
      unitIds: new FormControl('',), 
      schoolYear: new FormControl('', [Validators.required]),
    });
    this.languageObs$ = this.languageService
      .filterAndPaginateLanguages({
        pageIndex: 1,
        pageSize: 10,
        filterString: '',
      })
      .pipe(map((data) => data.items));
    this.unitObs$ = this.unitService
      .filterAndPaginateDepartments({
        filterString: '',
        pageSize: 250,
        pageIndex: 1,
      })
      .pipe(map((data) => data.items));
    this.schoolYearObs$ = this.availableDatesAsObservable();
  }
  private availableDatesAsObservable() {
    const availableDates = [];
    const year = new Date().getFullYear();
    const previousYear = year - 1;
    for (let i = 0; i < 20; i++) {
      availableDates[i] = `${previousYear - i + 1}-${year - i + 1}`;
    }
    return of(availableDates);
  }
  closeDialog(save: boolean) {
    this.dialogRef.close(save?this.exportOptionsForm?.getRawValue():undefined)
  }
}
