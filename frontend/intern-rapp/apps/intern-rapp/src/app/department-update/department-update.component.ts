import { ChangeDetectionStrategy, Component, Input, OnChanges, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDialog, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import { AbstractControl, FormArray, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatInputModule } from '@angular/material/input';
import { HttpClientModule } from '@angular/common/http';
import { MatChipInputEvent, MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import {COMMA, ENTER} from '@angular/cdk/keycodes';
import { Observable, Subject, filter, map, shareReplay, switchMap, take, takeUntil, tap } from 'rxjs';
import { MatOptionSelectionChange } from '@angular/material/core';
import { TranslateModule } from '@ngx-translate/core';
import { DepartmentItemDetail } from '../entities/departmentItemDetail';
import { buildFormGroupForTranslations, convertFormsArrayToObjectForUpdatedUnit } from '../preface-translation-form/buildFormGroupForPrefaceTranslation';
import { MatTabsModule } from '@angular/material/tabs';
import { LanguageItem } from '../entities/languageItem';
import { LanguagePopupChoiceComponent } from '../language-popup-choice/language-popup-choice.component';
import { PrefaceTranslationFormComponent } from '../preface-translation-form/preface-translation-form.component';
import { DepartmentUpdate } from '../entities/departmentUpdate';
import { LanguageService } from '../services/language.service';
import { DepartmentService } from '../services/department.service';

@Component({
  selector: 'intern-rapp-department-update',
  standalone: true,
  imports: [
    CommonModule,
    MatDialogModule,
    MatDividerModule,
    TranslateModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatAutocompleteModule,
    MatInputModule,
    HttpClientModule,
    MatChipsModule,
    MatIconModule,
    MatTabsModule,
    PrefaceTranslationFormComponent,
  ],
  templateUrl: './department-update.component.html',
  styleUrls: ['./department-update.component.scss'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class DepartmentUpdateComponent implements OnInit, OnDestroy {
  @Input() data!: DepartmentItemDetail;
  filteredOptions!: Observable<(string | undefined)[]>;
  separatorKeysCodes: number[] = [ENTER, COMMA];
  options: string[] = [];
  updateForm: FormGroup | undefined;
  managerEmailsAfterUpdate: string[] = [];
  private languageObs$: Observable<LanguageItem[]> | undefined;
  private popUpConfig = {
    width: '400px',
    closeOnNavigation: true,
    disableClose: false,
    hasBackdrop: true,
    position: { top: '250px', right: '500px' },
  };
  private destroySubj$ = new Subject();
  constructor(
    public dialogRef: MatDialogRef<DepartmentUpdateComponent>,
    public dialog: MatDialog,
    private languageService: LanguageService,
    private unitService:DepartmentService
  ) {}
  
  ngOnDestroy(): void {
    this.destroySubj$.next(undefined);
    this.destroySubj$.complete();
  }
  public get tabsArrayLength() {
    return (this.updateForm?.controls['translateTabs'] as FormArray).length;
  }
  public get tabs() {
    return (this.updateForm?.controls['translateTabs'] as FormArray).controls;
  }
  public get tabIsInvalid() {
    let isInvalid = false;
    (this.updateForm?.controls['translateTabs'] as FormArray).controls.forEach(
      (x) => {
        if (x.invalid) {
          isInvalid = true;
        }
      }
    );
    return isInvalid;
  }
  public getFormGroupOutOfAbstractControl(
    abstractControl: AbstractControl
  ): FormGroup {
    return abstractControl as FormGroup;
  }
  ngOnInit(): void {
    this.updateForm = new FormGroup({
      departmentName: new FormControl('', [Validators.required]),
      managerEmails: new FormControl(''),
      translateTabs: new FormArray([]),
    });
    this.updateForm?.controls['departmentName'].patchValue(this.data?.name);
    this.filteredOptions = this.updateForm?.controls[
      'managerEmails'
    ].valueChanges.pipe(
      switchMap((data) => {
        return this.unitService.getAllSupervisorNamesContaining(data);
      })
    );
    this.managerEmailsAfterUpdate = this.data?.managerEmails;
      this.languageObs$ = this.languageService
        .filterAndPaginateLanguages({
          filterString: '',
          pageIndex: 1,
          pageSize: 250,
        })
        .pipe(
          shareReplay(1),
          map((data) => data.items)
        );
    this.data.prefaceTranslations?.forEach((x) => {
      debugger;
      const controls = (this.updateForm?.controls['translateTabs'] as FormArray)
        .controls;
      controls.push(
        buildFormGroupForTranslations(
          x,
          x.language.id,
          x.language.name ?? undefined
        )
      );
    });
  }
  addLanguage() {
    this.languageObs$
      ?.pipe(
        map((data) => {
          const itemsTobeReturned: LanguageItem[] = [];
          for (let i = 0; i < data.length; i++) {
            let isValid = true;
            for (let j = 0; j < this.tabs.length; j++) {
              if (this.tabs[j].getRawValue().languageId === data[i].id) {
                isValid = false;
                break;
              }
            }
            if (isValid) itemsTobeReturned.push(data[i]);
          }
          return itemsTobeReturned;
        }),
        switchMap((data) => {
          const dialogRef = this.dialog.open(
            LanguagePopupChoiceComponent,
            this.popUpConfig
          );
          dialogRef.componentInstance.languageItems = data;
          return dialogRef.afterClosed().pipe(
            map((confirm) => (confirm ? confirm : undefined)),
            filter((data) => !!data),
            tap((data) => {
              const controls = (
                this.updateForm?.controls['translateTabs'] as FormArray
              ).controls;
              controls.push(
                buildFormGroupForTranslations(
                  undefined,
                  data !== undefined ? data.id : undefined,
                  data !== undefined ? data.name?.toString() : undefined
                )
              );
            })
          );
        }),
        take(1),
        takeUntil(this.destroySubj$)
      )
      .subscribe();
    // resultaat toevoegen aan formArray versions
  }
  add(event: MatChipInputEvent): void {
    const value = (event.value || '').trim();
    if (value) {
      this.managerEmailsAfterUpdate.push(value);
    }
    // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
    event.chipInput!.clear();
    this.updateForm?.controls['managerEmails'].setValue(null);
  }
  remove(managerEmail: string): void {
    this.managerEmailsAfterUpdate = this.managerEmailsAfterUpdate.filter(
      (x) => x != managerEmail
    );
  }
  addToListOfManagers(event: MatOptionSelectionChange) {
    this.updateForm?.controls['managerEmails'].patchValue(
      event.source.value.toString()
    );
  }
  close(save: boolean) {
    const entityTobeUpdated: DepartmentUpdate = {
      id: this.data.id,
      managerEmails: this.managerEmailsAfterUpdate,
      name: this.updateForm?.controls['departmentName'].value,
      prefaceTranslations: convertFormsArrayToObjectForUpdatedUnit(
        this.updateForm?.controls['translateTabs'] as FormArray
      ),
    };

    this.dialogRef.close(save ? entityTobeUpdated : undefined);
  }
 
}
