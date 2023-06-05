import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExportPopupOptionsComponent } from './export-popup-options.component';

describe('ExportPopupOptionsComponent', () => {
  let component: ExportPopupOptionsComponent;
  let fixture: ComponentFixture<ExportPopupOptionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ExportPopupOptionsComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(ExportPopupOptionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
