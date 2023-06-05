import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LanguagePopupChoiceComponent } from './language-popup-choice.component';

describe('LanguagePopupChoiceComponent', () => {
  let component: LanguagePopupChoiceComponent;
  let fixture: ComponentFixture<LanguagePopupChoiceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LanguagePopupChoiceComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(LanguagePopupChoiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
