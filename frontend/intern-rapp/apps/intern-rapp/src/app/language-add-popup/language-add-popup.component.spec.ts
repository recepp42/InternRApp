import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LanguageAddPopupComponent } from './language-add-popup.component';

describe('LanguageAddPopupComponent', () => {
  let component: LanguageAddPopupComponent;
  let fixture: ComponentFixture<LanguageAddPopupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LanguageAddPopupComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(LanguageAddPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
