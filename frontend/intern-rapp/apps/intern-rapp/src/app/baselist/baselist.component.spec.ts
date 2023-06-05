import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BaselistComponent } from './baselist.component';

describe('FilterlistComponent', () => {
  let component: BaselistComponent;
  let fixture: ComponentFixture<BaselistComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BaselistComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(BaselistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
