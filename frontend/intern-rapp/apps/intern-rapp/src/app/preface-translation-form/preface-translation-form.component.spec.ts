import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PrefaceTranslationFormComponent } from './preface-translation-form.component';

describe('PrefaceTranslationFormComponent', () => {
  let component: PrefaceTranslationFormComponent;
  let fixture: ComponentFixture<PrefaceTranslationFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PrefaceTranslationFormComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(PrefaceTranslationFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
