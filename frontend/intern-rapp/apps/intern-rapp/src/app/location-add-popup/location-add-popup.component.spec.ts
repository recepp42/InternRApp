import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LocationAddPopupComponent } from './location-add-popup.component';

describe('LocationAddPopupComponent', () => {
  let component: LocationAddPopupComponent;
  let fixture: ComponentFixture<LocationAddPopupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LocationAddPopupComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(LocationAddPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
