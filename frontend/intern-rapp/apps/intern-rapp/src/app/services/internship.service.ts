import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { PaginationFilterRequest } from '../entities/paginationFilterRequest';
import { APIConfiguration } from '../configurations/APIConfiguration';
import { ResourceItemPagingResponse } from '../entities/resourceItemPagingResponse';
import { InternshipItem } from '../entities/internshipItem';
import { catchError, map, retry, tap } from 'rxjs';
import { CreateInternship } from '../entities/createInternship';
import { InternshipTranslationUpdateDto } from '../entities/internshipTranslationUpdateDto';
import { InternshipDetailItem } from '../entities/internshipDetailItem';
import { InternshipUpdateDto } from '../entities/internshipUpdateDto';
import { ExportInternshipOptions } from '../entities/exportInternshipOptions';
import { DomSanitizer } from '@angular/platform-browser';

@Injectable({
  providedIn: 'root',
})
export class InternshipService {
  constructor(private http: HttpClient,private sanitizer:DomSanitizer) {}
  public entityTobeUpdated: InternshipDetailItem | undefined;
  private baseSuffixApi = '/api/InternShip';
  public filterAndPaginateLanguages(
    filterPaginationRequest: PaginationFilterRequest
  ) {
    return this.http
      .get<ResourceItemPagingResponse<InternshipItem>>(
        APIConfiguration.baseString +
          `${this.baseSuffixApi}?PageIndex=${filterPaginationRequest.pageIndex}&PageSize=${filterPaginationRequest.pageSize}&${filterPaginationRequest.filterString}`
      )
      .pipe(catchError((err, caught) => caught));
  }
  public getInternshipById(id: number) {
    return this.http.get<InternshipDetailItem>(
      APIConfiguration.baseString + `${this.baseSuffixApi}/${id}`
    );
  }
  public createInternship(internship: CreateInternship) {
    return this.http
      .post(
        APIConfiguration.baseString + `${this.baseSuffixApi}`,
        {
          schoolYear: internship.schoolYear,
          unitId: internship.unitId,
          maxCountOfStudents: internship.maxCountOfStudents,
          currentCountOfStudents: internship.currentCountOfStudents,
          trainingType: Number(internship.trainingType),
          locations: internship.locations,
          versions: internship.versions,
        },
        {
          headers: {
            'content-type': 'application/json',
          },
        }
      )
      .pipe(
        catchError((err, caught) => caught),
        retry(2)
      );
  }
  public copyToNextYear(ids: number[]) {
    return this.http
      .post(
        APIConfiguration.baseString + `${this.baseSuffixApi}/copyToNextYear`,
        ids,
        {
          headers: {
            'content-type': 'application/json',
          },
        }
      )
      .pipe(
        catchError((err, caught) => caught),
        retry(2)
      );
  }
  public updateInternship(internship: InternshipUpdateDto) {
    return this.http
      .put(
        APIConfiguration.baseString + `${this.baseSuffixApi}`,
        {
          schoolYear: internship.schoolYear,
          internshipId: internship.internShipId,
          unitId: internship.unitId,
          maxCountOfStudents: internship.maxCountOfStudents,
          currentCountOfStudents: internship.currentCountOfStudents,
          trainingType: Number(internship.trainingType),
          locations: internship.locations,
          versions: internship.versions,
        },
        {
          headers: {
            'content-type': 'application/json',
          },
        }
      )
      .pipe(
        catchError((err, caught) => caught),
        retry(2)
      );
  }
  deleteInternship(ids: number[]) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: JSON.stringify(ids),
    };
    return this.http.delete(
      APIConfiguration.baseString + `${this.baseSuffixApi}`,
      httpOptions
    );
  }
  public exportInternships(filterCriteria: ExportInternshipOptions) {
  

    let queryString = '';
    for (let unit of filterCriteria.unitIds) {
      queryString += `unitIds=${unit}`;
      if (
        filterCriteria.unitIds.indexOf(unit) <
        filterCriteria.unitIds.length - 1
      ) {
        queryString += '&';
      }
    }
    queryString += `&schoolYear=${filterCriteria.schoolYear}&languageId=${filterCriteria.languageId}`;
      return this.http
        .get(
          APIConfiguration.baseString +
            `${this.baseSuffixApi}/export?${queryString}`,
          { responseType: 'blob' }
        )
        .pipe(
          tap((response: Blob) => {
            const blob = new Blob([response], {
              type: 'application/octet-stream',
            });
                      const linkElement = document.createElement('a');
                      linkElement.href = window.URL.createObjectURL(blob);
                      linkElement.download = 'internships.docx';
                      linkElement.dispatchEvent(new MouseEvent('click'));
          })
        );
    
    };
  }

