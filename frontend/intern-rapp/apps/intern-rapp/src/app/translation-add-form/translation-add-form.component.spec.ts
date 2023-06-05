import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TranslationAddFormComponent } from './translation-add-form.component';

describe('TranslationAddFormComponent', () => {
  let component: TranslationAddFormComponent;
  let fixture: ComponentFixture<TranslationAddFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TranslationAddFormComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(TranslationAddFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
