import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError } from 'rxjs';
import { APIConfiguration } from '../configurations/APIConfiguration';
import { PaginationFilterRequest } from '../entities/paginationFilterRequest';
import { ResourceItemPagingResponse } from '../entities/resourceItemPagingResponse';
import { LocationItem, } from '../entities/locationItem';
import { CreateLocation } from '../entities/createLocation';

@Injectable({
  providedIn: 'root',
})
export class LocationService {
  constructor(private http: HttpClient) {}

  private baseSuffixApi = '/api/Location';

  filterAndPaginateLocations$(
    filterPaginationRequest: PaginationFilterRequest
  ) {
    return this.http
      .get<ResourceItemPagingResponse<LocationItem>>(
        APIConfiguration.baseString +
          `${this.baseSuffixApi}?PageIndex=${filterPaginationRequest.pageIndex}&PageSize=${filterPaginationRequest.pageSize}&${filterPaginationRequest.filterString}`
      )
      .pipe(catchError((err, caught) => caught));
  }

  addLocation$(itemToBeAdded: CreateLocation) {
    return this.http
      .post(APIConfiguration.baseString + `${this.baseSuffixApi}`, {
        city: itemToBeAdded?.city,
        streetname: itemToBeAdded?.streetname,
        housenumber: itemToBeAdded?.housenumber,
        zipcode: itemToBeAdded?.zipcode,
      })
      .pipe(catchError((err, caught) => caught));
  }

  updateLocation$(itemToBeUpdated: LocationItem | undefined) {
    return this.http
      .patch(APIConfiguration.baseString + `${this.baseSuffixApi}`, {
        id: itemToBeUpdated?.id,
        city: itemToBeUpdated?.city,
        streetname: itemToBeUpdated?.streetname,
        housenumber: itemToBeUpdated?.housenumber,
        zipcode: itemToBeUpdated?.zipcode,
      })
      .pipe(catchError((err, caught) => caught));
  }

  deleteLocation$(ids: number[] | undefined) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: JSON.stringify(ids),
    };
    return this.http
      .delete<number>(
        APIConfiguration.baseString + `${this.baseSuffixApi}`,
        httpOptions
      )
      .pipe(catchError((err, caught) => caught));
  }
}
