import {
  ChangeDetectionStrategy,
  Component,
  OnDestroy,
  OnInit,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { BaselistComponent } from '../baselist/baselist.component';
import { ItemsTmplDirective } from '../items-tmpl.directive';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { TranslateModule } from '@ngx-translate/core';
import { BaseList } from '../baselist/baseList';
import {
  filter,
  Observable,
  switchMap,
  Subject,
  tap,
  map,
  take,
  takeUntil,
  of,
  BehaviorSubject,
} from 'rxjs';
import { PaginationFilterRequest } from '../entities/paginationFilterRequest';
import { CreateInternship } from '../entities/createInternship';
import { InternshipDetailItem } from '../entities/internshipDetailItem';
import { InternshipService } from '../services/internship.service';
import { FilterType } from '../enums/filterType';
import { ResourceItemPagingResponse } from '../entities/resourceItemPagingResponse';
import { InternshipItem } from '../entities/internshipItem';
import { Router } from '@angular/router';
import { DeletePopupComponent } from '../delete-popup/delete-popup.component';
import { DepartmentService } from '../services/department.service';
import { LanguageService } from '../services/language.service';
import { LanguageItem } from '../entities/languageItem';
import { DepartementItemWithMinimalData } from '../entities/depItemWithMinimalData';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ExportPopupOptionsComponent } from '../export-popup-options/export-popup-options.component';
import { ExportInternshipOptions } from '../entities/exportInternshipOptions';
import { AuthService } from '../services/auth-service.service';
import { Role } from '../enums/role';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { SnackbarComponent } from '../snackbar/snackbar.component';

@Component({
  selector: 'intern-rapp-intern-ship-list',
  standalone: true,
  imports: [
    CommonModule,
    MatDividerModule,
    MatIconModule,
    MatListModule,
    BaselistComponent,
    TranslateModule,
    ItemsTmplDirective,
    ReactiveFormsModule,
    HttpClientModule,
    MatDialogModule,
    MatCheckboxModule,
    MatFormFieldModule,
    MatInputModule,
    MatSnackBarModule
  ],
  templateUrl: './intern-ship-list.component.html',
  styleUrls: ['./intern-ship-list.component.scss'],
})
export class InternShipListComponent
  extends BaseList<InternshipItem>
  implements OnInit, OnDestroy
{
  public deleteSubject = new Subject<number>();
  public addSubject = new Subject<CreateInternship | undefined>();
  public updateSubject = new Subject<InternshipDetailItem>();
  private destroySubj$ = new Subject<void>();
  private selectedIds: number[] = [];
  private exportSubj$ = new Subject<void>();
  constructor(
    private internshipService: InternshipService,
    private unitService: DepartmentService,
    private languageService: LanguageService,
    private router: Router,
    public dialog: MatDialog,
    public authService: AuthService,
    private _snackBar: MatSnackBar
  ) {
    super();
  }

  private popUpConfig = {
    width: '400px',
    closeOnNavigation: true,
    disableClose: false,
    hasBackdrop: true,
    position: { top: '250px', right: '41%' },
  };
  ngOnDestroy(): void {
    this.destroySubj$.next();
    this.destroySubj$.complete();
  }
  private openSnackBar() {
    this._snackBar.openFromComponent(SnackbarComponent, {
      duration: 10000,
    });
  }
  public addInternship() {
    this.internshipService.entityTobeUpdated = undefined;
    this.router.navigateByUrl('internships/create');
  }
  public updateInternship(id: unknown) {
    this.internshipService
      .getInternshipById(id as number)
      .pipe(
        tap((data) => {
          this.internshipService.entityTobeUpdated = data;

          this.router.navigateByUrl('internships/create', {
            onSameUrlNavigation: 'reload',
          });
        }),
        take(1),
        takeUntil(this.destroySubj$)
      )
      .subscribe();
  }
  private configureDelete$() {
    return this.deleteSubject.pipe(
      switchMap((id) => {
        const dialogRef = this.dialog.open(
          DeletePopupComponent,
          this.popUpConfig
        );
        dialogRef.componentInstance.title = 'internship';
        return dialogRef
          .afterClosed()
          .pipe(map((confirm) => (confirm ? id : undefined)));
      }),
      filter((id) => !!id), //undefined checken
      switchMap((id) =>
        this.internshipService.deleteInternship(this.selectedIds ?? 0)
      )
    );
  }
  getGridItems$(
    paginationFilterRequest: PaginationFilterRequest
  ): Observable<ResourceItemPagingResponse<InternshipItem>> {
    return this.internshipService.filterAndPaginateLanguages(
      paginationFilterRequest
    );
  }
  ngOnInit(): void {
    const activeFilters: Record<string, unknown> = {};
    activeFilters['schoolYear'] = this.calculateCurrentSchoolyear();
    this.filterUpdated(activeFilters);
    this.exportSubj$
      .pipe(
        switchMap(() => {
          const dialogRef = this.dialog.open(
            ExportPopupOptionsComponent,
            this.popUpConfig
          );
          // dialogRef.componentInstance.title = 'internship';
          return dialogRef
            .afterClosed()
            .pipe(
              map(
                (confirm) => (confirm as ExportInternshipOptions) ?? undefined
              )
            );
        }),
        filter((id) => !!id), //undefined checken
        switchMap((data) => this.internshipService.exportInternships(data)),
        take(1),
        takeUntil(this.destroySubj$)
      )
      .subscribe();

    this.filters = [
      {
        label: 'languageNameLabel',
        name: 'languages',
        type: FilterType.MultiSelectDropdown,
        observable: this.languageService
          .filterAndPaginateLanguages({
            pageSize: 250,
            pageIndex: 1,
            filterString: '',
          })
          .pipe(map((data) => data.items)),
        optionBuilder: (items: unknown[], value: LanguageItem) => {
          const optionBuilder: unknown[] = [];
          const displayValue =
            (items as LanguageItem[]).find((x) => x.id === value.id)?.code ??
            '';
          optionBuilder.push(displayValue);
          optionBuilder.push(value.id);
          return optionBuilder;
        },
        defaultValue: undefined,
      },
      {
        label: 'unitNameLabel',
        name: 'units',
        type: FilterType.MultiSelectDropdown,
        observable: this.unitService
          .filterAndPaginateDepartmentsWithMinimalData({
            filterString: '',
            pageIndex: 1,
            pageSize: 100,
          })
          .pipe(map((data) => data.items)),
        optionBuilder: (
          items: unknown[],
          value: DepartementItemWithMinimalData
        ) => {
          const optionBuilder: unknown[] = [];
          const displayValue =
            (items as DepartementItemWithMinimalData[]).find(
              (x) => x.id === value.id
            )?.name ?? '';
          optionBuilder.push(displayValue);
          optionBuilder.push(value.id);
          return optionBuilder;
        },
        defaultValue: undefined,
      },
      {
        label: 'schoolYearLabel',
        name: 'schoolyear',
        type: FilterType.SingleSelectDropdown,
        observable: this.availableDatesAsObservable(),
        optionBuilder: (items: unknown[], item: string) => {
          const optionBuilder: unknown[] = [];
          const displayValue =
            (items as string[]).find((x) => x === item) ?? '';
          optionBuilder.push(displayValue);
          optionBuilder.push(item);
          return optionBuilder;
        },
        defaultValue: this.calculateCurrentSchoolyear(),
      },
    ];

    this.configureItems([this.configureDelete$()]);
  }
  private calculateCurrentSchoolyear() {
    const date = new Date();
    const year = date.getFullYear();
    const month = date.getMonth();
    if (month <= 6) {
      return `${year - 1}-${year}`;
    } else {
      return `${year}-${year + 1}`;
    }
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
  filterUpdating(filter: {}) {
    const record = filter as Record<string, never>;
    const activeFilters: Record<string, unknown> = {};

    if (record['schoolyear']) {
      activeFilters['schoolYear'] = record['schoolyear'];
    }
    if (record['units']) {
      const unitIds = record['units'] as string[];
      let unitFilterString = '';
      for (let unitId of unitIds) {
        unitFilterString += `unitIds=${unitId}&`;
      }
      unitFilterString = unitFilterString.slice(0, unitFilterString.length - 1);
      activeFilters['unitIds'] = unitFilterString;
    }
    if (record['languages']) {
      const unitIds = record['languages'] as string[];
      let languageFilterString = '';
      for (let languageId of unitIds) {
        languageFilterString += `languageIds=${languageId}&`;
      }
      languageFilterString = languageFilterString.slice(
        0,
        languageFilterString.length - 1
      );
      activeFilters['languageIds'] = languageFilterString;
    }
    this.filterUpdated(activeFilters);
  }
  public addToSelectedInternships(completed: boolean, id: number) {
    console.log(completed);
    console.log(id);

    if (!completed) {
      this.selectedIds = this.selectedIds.filter((x) => x !== id);
      return;
    }
    this.selectedIds.push(id);
  }
  public copyInternship() {
    this.internshipService
      .copyToNextYear(this.selectedIds)
      .pipe(
        tap(() => {
          // this.router.navigateByUrl('/internships', {
          //   onSameUrlNavigation: 'reload',
          // });
          this.openSnackBar()
        }),
        take(1),
        takeUntil(this.destroySubj$)
      )
      .subscribe();
  }
  delete() {
    this.deleteSubject.next(0);
  }
  exportButtonHandler() {
    this.exportSubj$.next();
  }
}
