import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LocationUpdatePopupComponent } from './location-update-popup.component';

describe('LocationUpdatePopupComponent', () => {
  let component: LocationUpdatePopupComponent;
  let fixture: ComponentFixture<LocationUpdatePopupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LocationUpdatePopupComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(LocationUpdatePopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
