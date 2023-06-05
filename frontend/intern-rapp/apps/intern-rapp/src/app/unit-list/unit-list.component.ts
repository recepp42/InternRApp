import {
  ChangeDetectionStrategy,
  Component,
  inject,
  OnInit,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { MatSelectModule } from '@angular/material/select';
import { MatListModule } from '@angular/material/list';
import { filter, Observable, switchMap, Subject, tap } from 'rxjs';
import { BaselistComponent } from '../baselist/baselist.component';
import { DepartmentService } from '../services/department.service';
import { HttpClient, HttpClientModule } from '@angular/common/http';

import { PaginationFilterRequest } from '../entities/paginationFilterRequest';
import { ItemsTmplDirective } from '../items-tmpl.directive';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { DeletePopupComponent } from '../delete-popup/delete-popup.component';
import { Filter } from '../entities/filter';
import { FilterType } from '../enums/filterType';
import { map } from 'rxjs/operators';
import { DepartmentItem } from '../entities/departmentItem';
import { ResourceItemPagingResponse } from '../entities/resourceItemPagingResponse';
import { BaseList } from '../baselist/baseList';
import { DepartmentUpdateComponent } from '../department-update/department-update.component';
import { CreateDepartment } from '../entities/CreateDepartment';
import { DepartmentAddPopupComponent } from '../department-add-popup/department-add-popup.component';
import { TranslateModule } from '@ngx-translate/core';
import { DepartmentUpdate } from '../entities/departmentUpdate';
import { DepartmentItemDetail } from '../entities/departmentItemDetail';
import { convertFormsArrayToObjectForUpdatedUnit } from '../preface-translation-form/buildFormGroupForPrefaceTranslation';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { AuthService } from '../services/auth-service.service';

@Component({
  selector: 'intern-rapp-unit-list',
  standalone: true,
  imports: [
    CommonModule,
    MatListModule,
    MatDialogModule,
    MatSelectModule,
    ReactiveFormsModule,
    HttpClientModule,
    ItemsTmplDirective,
    MatListModule,
    MatIconModule,
    MatInputModule,
    BaselistComponent,
    DeletePopupComponent,
    TranslateModule,
    MatCheckboxModule,
  ],
  templateUrl: './unit-list.component.html',
  styleUrls: ['./unit-list.component.scss'],
  providers: [HttpClient, DepartmentService],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class UnitListComponent
  extends BaseList<DepartmentItem>
  implements OnInit
{
  public deleteSubject = new Subject<number>();
  public addSubject = new Subject<CreateDepartment | undefined>();
  public updateSubject = new Subject<number>();
  private selectedIds: number[] = [];
  constructor(
    private unitService: DepartmentService,
    public dialog: MatDialog,
    public authService:AuthService
  ) {
    super();
  }

  private popUpConfig = {
    width: '500px',
    closeOnNavigation: true,
    disableClose: false,
    hasBackdrop: true,
    position: { top: '6%', right: '35%' },
  };
  getGridItems$(
    paginationFilterRequest: PaginationFilterRequest
  ): Observable<ResourceItemPagingResponse<DepartmentItem>> {
    return this.unitService.filterAndPaginateDepartments(
      paginationFilterRequest
    );
  }
  ngOnInit(): void {
    this.filters = [
      {
        label: 'departmentNameLabel',
        name: 'filterValue',
        type: FilterType.Text,
        observable: undefined,
        optionBuilder: (items: unknown[]) => undefined,
        defaultValue: undefined,
      },
    ];
    const delete$ = this.configureDelete$();
    const update$ = this.configureUpdate$();
    const add$ = this.configureAdd$();
    this.configureItems([delete$, update$, add$]);
  }

  private configureDelete$() {
    return this.deleteSubject.pipe(
      switchMap((id) => {
        const dialogRef = this.dialog.open(
          DeletePopupComponent,
          this.popUpConfig
        );
        dialogRef.componentInstance.title = 'department';
        return dialogRef
          .afterClosed()
          .pipe(map((confirm) => (confirm ? id : undefined)));
      }),
      filter((id) => !!id),
      switchMap((id) => this.unitService.deleteDepartment(this.selectedIds))
    );
  }

  private configureUpdate$() {
    return this.updateSubject.pipe(
      switchMap((id) => {
        return this.unitService.getById(id);
      }),
      switchMap((data) => {
        const dialogRef = this.dialog.open(
          DepartmentUpdateComponent,
          this.popUpConfig
        );
        dialogRef.componentInstance.data = data as DepartmentItemDetail;
        return dialogRef
          .afterClosed()
          .pipe(map((confirm) => (confirm ? confirm : undefined)));
      }),
      filter((id) => !!id),
      switchMap((depItem) =>
        this.unitService.updateDepartment(depItem as DepartmentUpdate)
      )
    );
  }

  private configureAdd$() {
    return this.addSubject.pipe(
      switchMap((data) => {
        const dialogRef = this.dialog.open(
          DepartmentAddPopupComponent,
          this.popUpConfig
        );
        return dialogRef
          .afterClosed()
          .pipe(
            map((confirm) => (confirm !== undefined ? confirm : undefined))
          );
      }),
      filter((id) => !!id),
      switchMap((depItem) => {
        return this.unitService.addDepartment(depItem);
      })
    );
  }

  filterUpdating(filter: {}) {
    const record = filter as Record<string, never>;
    const activeFilters: Record<string, unknown> = {};
    this.filters?.forEach((x) => {
      activeFilters['unitName'] = `${record[x.name]}`;
    });

    this.filterUpdated(activeFilters);
  }
  addDepartment() {
    this.addSubject.next(undefined);
  }

  updateDepartment = (id: number) => {
    this.updateSubject.next(id);
  };
  delete() {
    this.deleteSubject.next(1);
  }
  public addToSelectedUnits(completed: boolean, id: number) {
    if (!completed) {
      this.selectedIds = this.selectedIds.filter((x) => x !== id);
      return;
    }
    this.selectedIds.push(id);
  }
}
