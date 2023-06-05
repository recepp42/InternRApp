import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DepartmentAddPopupComponent } from './department-add-popup.component';

describe('DepartmentAddPopupComponent', () => {
  let component: DepartmentAddPopupComponent;
  let fixture: ComponentFixture<DepartmentAddPopupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DepartmentAddPopupComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(DepartmentAddPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
