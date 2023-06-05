import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { APIConfiguration } from '../configurations/APIConfiguration';
import { catchError, Observable, of, retry, shareReplay, take } from 'rxjs';
import {  CreateDepartment } from '../entities/CreateDepartment';
import { PaginationRequest } from '../entities/paginationRequest';
import { DepartmentItem } from '../entities/departmentItem';
import { PaginationFilterRequest } from '../entities/paginationFilterRequest';
import { ResourceItemPagingResponse } from '../entities/resourceItemPagingResponse';
import { DepartementItemWithMinimalData } from '../entities/depItemWithMinimalData';
import { DepartmentItemDetail } from '../entities/departmentItemDetail';
import { DepartmentUpdate } from '../entities/departmentUpdate';

@Injectable({
  providedIn: 'root',
})
export class DepartmentService {
  constructor(private http: HttpClient) {}
  private baseSuffixApi = '/api/Unit';
 getAllSupervisorNamesContaining(
    name: string | undefined
  ): Observable<string[]> {
    return this.http
      .get<string[]>(
        APIConfiguration.baseString +
          `/api/ApplicationUser/?filterValue=${name}`
      )
      .pipe(catchError((err, caught) => caught));
  }
  postDepartment(department: CreateDepartment) {
    return this.http
      .post(APIConfiguration.baseString + `${this.baseSuffixApi}`, department)
      .pipe(catchError((err, caught) => caught));
  }
  filterAndPaginateDepartments(
    filterPaginationRequest: PaginationFilterRequest
  ) {
    return this.http
      .get<ResourceItemPagingResponse<DepartmentItem>>(
        APIConfiguration.baseString +
          `${this.baseSuffixApi}?PageIndex=${filterPaginationRequest.pageIndex}&PageSize=${filterPaginationRequest.pageSize}&${filterPaginationRequest.filterString}`
      )
      .pipe(catchError((err, caught) => caught));
  }
  filterAndPaginateDepartmentsWithMinimalData(
    filterPaginationRequest: PaginationFilterRequest
  ) {
    return this.http
      .get<ResourceItemPagingResponse<DepartementItemWithMinimalData>>(
        APIConfiguration.baseString +
          `${this.baseSuffixApi}/getAllWithminimaldata?PageIndex=${filterPaginationRequest.pageIndex}&PageSize=${filterPaginationRequest.pageSize}&Filter=${filterPaginationRequest.filterString}`
      )
      .pipe(catchError((err, caught) => caught));
  }
  deleteDepartment(ids: number[] | undefined) {
      const httpOptions = {
        headers: new HttpHeaders({
          'Content-Type': 'application/json',
        }),
        body: JSON.stringify(ids),
      };
    return this.http
      .delete<number>(
        APIConfiguration.baseString + `${this.baseSuffixApi}`,httpOptions
      )
      .pipe(catchError((err, caught) => caught));
  }
  updateDepartment(itemToBeUpdated: DepartmentUpdate | undefined) {
    return this.http
      .patch(APIConfiguration.baseString + `${this.baseSuffixApi}`, {
        id: itemToBeUpdated?.id,
        name: itemToBeUpdated?.name,
        managerEmails: itemToBeUpdated?.managerEmails,
        prefaceTranslations: itemToBeUpdated?.prefaceTranslations,
      })
      .pipe(
        catchError((error) => {
          throw error
          return of(null);
        }),
        // retry once
        retry(1),
        // only take the first value emitted (either successful or null)
        take(1)
      );
  }

  addDepartment(itemToBeAdded: CreateDepartment) {
    const body = {
      name: itemToBeAdded.name,
      superVisorEmails: itemToBeAdded.superVisorEmails,
      prefaceTranslations: itemToBeAdded.prefaces,
    };
    return this.http
      .post(APIConfiguration.baseString + `${this.baseSuffixApi}`, body)
      .pipe(
       
            catchError((error) => {
              console.error(error);
              return of(null);
            }),
          // retry once
          retry(1),
          // only take the first value emitted (either successful or null)
          take(1)
        )
      
  }
  public getById(id: number) {
    return this.http
      .get<DepartmentItemDetail>(
        APIConfiguration.baseString + `${this.baseSuffixApi}/${id}`
      )
      .pipe(
        catchError((error) => {
          console.error(error);
          return of(null);
        }),
        // retry once
        retry(1),
        // only take the first value emitted (either successful or null)
        take(1)
      )
  }
}

