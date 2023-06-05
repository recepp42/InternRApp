import { PageEvent } from '@angular/material/paginator';
import {
  BehaviorSubject,
  Observable,
  Subject,
  combineLatest,
  merge,
  startWith,
  switchMap,
  catchError,
  of,
  debounceTime,
} from 'rxjs';
import { Filter } from '../entities/filter';
import { PaginationFilterRequest } from '../entities/paginationFilterRequest';
import { PaginationRequest } from '../entities/paginationRequest';
import { ResourceItemPagingResponse } from '../entities/resourceItemPagingResponse';

export abstract class BaseList<T> {
  public listData$: Observable<ResourceItemPagingResponse<T>> | undefined;
  public filters: Filter[] | undefined;
  private readonly paginationsSub$: BehaviorSubject<PaginationRequest>;
  private readonly filtersSub$: BehaviorSubject<string>;

  constructor(pageSize = 10) {
    this.paginationsSub$ = new BehaviorSubject<PaginationRequest>({
      pageNumber: 1,
      pageSize,
    });
    this.filtersSub$ = new BehaviorSubject<string>('');
  }
  public get currentPage$(): Observable<PaginationRequest> {
    return this.paginationsSub$.asObservable();
  }
  public get filtered$(): Observable<{}> {
    return this.filtersSub$.asObservable();
  }
  public pagingUpdated(e: PageEvent): void {
    this.paginationsSub$.next({
      pageNumber: e.pageIndex + 1,
      pageSize: e.pageSize,
    });
  }
    public filterUpdated(newFilters: {}): void {
        const filters = Object(newFilters);
        let filterString=""
        for (let key in filters) {
            if ((filters[key] as string).split('=').length>=2) {
            filterString += `${filters[key]}&`;
                
            }
            else {
                filterString += `${key}=${filters[key]}&`; 
            }
        }
      filterString=filterString.slice(0, filterString.length - 1);
        this.filtersSub$.next(filterString);
  }
  protected abstract getGridItems$(
    filter: PaginationFilterRequest,
    dependable?: unknown
  ): Observable<ResourceItemPagingResponse<T>>;
  protected configureItems(
    refreshObservables?: Array<Observable<unknown>>
  ): void {
    this.listData$ = this.configureListData$(refreshObservables);
  }
  protected configureListData$(
    refreshObservables?: Array<Observable<unknown>>
  ): Observable<ResourceItemPagingResponse<T>> {

  
    const refresh$ =
      refreshObservables && refreshObservables.length > 0
        ? merge(...refreshObservables)
        : of(undefined);
    return combineLatest([
      this.paginationsSub$,
      this.filtersSub$,
      refresh$.pipe(startWith(undefined)),
    ]).pipe(
      debounceTime(300),
      switchMap(([currentPage, filters, dependable]) => {
        return this.getGridItems$(
          {
            pageSize: currentPage.pageSize,
            pageIndex: currentPage.pageNumber,
            filterString: filters,
          },
          dependable
        ).pipe(catchError((err, caught) => caught));
      })
    );
  }
}
