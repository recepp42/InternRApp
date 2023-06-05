import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LanguageUpdatePopupComponent } from './language-update-popup.component';

describe('LanguageUpdatePopupComponent', () => {
  let component: LanguageUpdatePopupComponent;
  let fixture: ComponentFixture<LanguageUpdatePopupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LanguageUpdatePopupComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(LanguageUpdatePopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
