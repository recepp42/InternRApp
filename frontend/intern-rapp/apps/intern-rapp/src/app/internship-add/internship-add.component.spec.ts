import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InternshipAddComponent } from './internship-add.component';

describe('InternshipAddComponent', () => {
  let component: InternshipAddComponent;
  let fixture: ComponentFixture<InternshipAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InternshipAddComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(InternshipAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
