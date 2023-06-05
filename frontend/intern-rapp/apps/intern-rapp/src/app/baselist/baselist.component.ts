import { Component, Input, OnInit, ChangeDetectionStrategy, ContentChild, OnDestroy, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import {  MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { BehaviorSubject, Observable, switchMap, combineLatest, tap, merge } from 'rxjs';
import {  ReactiveFormsModule } from '@angular/forms';
import {MatSelectModule} from '@angular/material/select';
import { MatListModule} from '@angular/material/list';
import { PaginationFilterRequest } from '../entities/paginationFilterRequest';
import { ResourceItemPagingResponse } from '../entities/resourceItemPagingResponse';
import { ItemsTmplDirective } from '../items-tmpl.directive';
import { startWith } from 'rxjs/operators';
import { PaginationRequest } from '../entities/paginationRequest';
import { Filter } from '../entities/filter';
import { FilterComponent } from '../filter/filter.component';
@Component({
  selector: 'intern-rapp-baselist',
  standalone: true,
  imports: [CommonModule,MatPaginatorModule,MatSelectModule,ReactiveFormsModule,MatListModule,ItemsTmplDirective,FilterComponent],
  templateUrl: './baselistcomponent.html',
  styleUrls: ['./baselist.component.scss'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class BaselistComponent<T>{
  @Input() public filters: Filter[]|undefined|null;
  @Output() public readonly pagingUpdated = new EventEmitter<PageEvent>();
  @Output() public readonly filtered = new EventEmitter<object>();
  @ContentChild(ItemsTmplDirective) public listTmplWrapper: ItemsTmplDirective<T[]> | undefined;
  @Input() public result$: Observable<ResourceItemPagingResponse<T>> | undefined;
  
  public updatePaging(e: PageEvent): void 
  {
    this.pagingUpdated.emit(e); 
  } 
  public filterList(e: object): void 
  {
    this.filtered.emit(e);
  }
 }

