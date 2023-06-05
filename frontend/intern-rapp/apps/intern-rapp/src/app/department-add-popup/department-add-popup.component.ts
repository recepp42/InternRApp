import { Component, Input, OnDestroy, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  MatDialog,
  MatDialogModule,
  MatDialogRef,
} from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import {
  ReactiveFormsModule,
  FormGroup,
  FormControl,
  Validators,
  FormArray,
  AbstractControl,
} from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { DepartmentService } from '../services/department.service';
import {
  catchError,
  debounceTime,
  distinctUntilChanged,
  EMPTY,
  filter,
  map,
  Observable,
  shareReplay,
  startWith,
  Subject,
  switchMap,
  take,
  takeUntil,
  tap,
} from 'rxjs';
import { MatInputModule } from '@angular/material/input';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { MatOptionSelectionChange } from '@angular/material/core';
import { MatChipInputEvent, MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { CreateDepartment } from '../entities/CreateDepartment';
import { TranslateModule } from '@ngx-translate/core';
import { LanguageItem } from '../entities/languageItem';
import { LanguagePopupChoiceComponent } from '../language-popup-choice/language-popup-choice.component';
import { PrefaceTranslationFormComponent } from '../preface-translation-form/preface-translation-form.component';
import { MatTabsModule } from '@angular/material/tabs';
import { buildFormGroupForTranslations, convertFormsArrayToObjectForNewUnit } from '../preface-translation-form/buildFormGroupForPrefaceTranslation';
import { LanguageService } from '../services/language.service';
@Component({
  selector: 'intern-rapp-department-add-popup',
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
    PrefaceTranslationFormComponent,
    MatTabsModule,
  ],
  templateUrl: './department-add-popup.component.html',
  styleUrls: ['./department-add-popup.component.scss'],
  providers: [HttpClient, DepartmentService],
})
export class DepartmentAddPopupComponent implements OnInit,OnDestroy {
  filteredOptions!: Observable<(string | undefined)[]>;
  separatorKeysCodes: number[] = [ENTER, COMMA];
  options: string[] = [];
  public addVersionSubject$ = new Subject<CreateDepartment>();
  public languages$ = new Observable<LanguageItem>();
  public addSubject$ = new Subject<CreateDepartment | undefined>();
  private languageObs$: Observable<LanguageItem[]> | undefined;

  private destroySubject$ = new Subject();
  //per unit
  public addUnitForm: FormGroup | undefined;

  managerEmailsTobeAdded: string[] = [];

  constructor(
    private unitService: DepartmentService,
    public dialogRef: MatDialogRef<DepartmentAddPopupComponent>,
    public dialog: MatDialog,
    private languageService: LanguageService
  ) {}

  private popUpConfig = {
    width: '400px',
    closeOnNavigation: true,
    disableClose: false,
    hasBackdrop: true,
    position: { top: '250px', right: '500px' },
  };
  ngOnDestroy(): void {
    this.destroySubject$.next(undefined);
    this.destroySubject$.complete();
  }
  public get tabsArrayLength() {
    return (this.addUnitForm?.controls['translateTabs'] as FormArray).length;
  }
  public get tabs() {
    return (this.addUnitForm?.controls['translateTabs'] as FormArray).controls;
  }
  public get tabIsInvalid() {
    let isInvalid = false;
    (this.addUnitForm?.controls['translateTabs'] as FormArray).controls.forEach(
      (x) => {
        if (x.invalid) {
          isInvalid = true;
        }
      }
    );
    return isInvalid;
  }
  add(event: MatChipInputEvent): void {
    const value = (event.value || '').trim();

    // Add email of the manager
    if (value) {
      this.managerEmailsTobeAdded.push(value);
    }

    // Clear the input value
    // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
    event.chipInput!.clear();
    this.addUnitForm?.controls['managerEmails'].setValue(null);
  }
  ngOnInit(): void {
    //backend is going to change the data it is returning(at the moment it is mocked data)
    this.addUnitForm = new FormGroup({
      departmentName: new FormControl('', [Validators.required]),
      managerEmails: new FormControl(''),
      translateTabs: new FormArray([]),
    });
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
    this.filteredOptions = this.addUnitForm.controls[
      'managerEmails'
    ].valueChanges.pipe(
      startWith(''),
      debounceTime(300),
      distinctUntilChanged(),
      //map(searchString=>searchString as string|undefined)),
      switchMap((searchString) =>
        this.unitService
          .getAllSupervisorNamesContaining(searchString as string | undefined)
          .pipe(catchError(() => EMPTY))
      )
    );
  }

  addToListOfManagers(event: MatOptionSelectionChange) {
    this.addUnitForm?.controls['managerEmails'].patchValue(
      event.source.value.toString()
    );
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
                this.addUnitForm?.controls['translateTabs'] as FormArray
              ).controls;
              controls.push(
                buildFormGroupForTranslations(
                  undefined,
                  data !== undefined ? data.id : undefined,
                  data !== undefined ? data.code?.toString() : undefined
                )
              );
            })
          );
        }),
        take(1),
        takeUntil(this.destroySubject$)
      )
      .subscribe();
    // resultaat toevoegen aan formArray versions
  }
  public getFormGroupOutOfAbstractControl(
    abstractControl: AbstractControl
  ): FormGroup {
    return abstractControl as FormGroup;
  }
  closeDialog(save: boolean) {
    let data: CreateDepartment | undefined;
    if (save) {
      data = this.mapToSubmittableNewUnitObject();
    }
    this.dialogRef.close(save ? data : undefined);
  }

  private mapToSubmittableNewUnitObject() {
    const internShipToBeReturned: CreateDepartment | undefined = {
      name: this.addUnitForm?.controls['departmentName'].getRawValue(),
      superVisorEmails: this.managerEmailsTobeAdded,
      prefaces: convertFormsArrayToObjectForNewUnit(
        this.addUnitForm?.controls['translateTabs'] as FormArray
      ),
    };
    return internShipToBeReturned;
  }
  remove(managerEmail: string): void {
    this.managerEmailsTobeAdded = this.managerEmailsTobeAdded.filter(
      (x) => x != managerEmail
    );
  }
}
