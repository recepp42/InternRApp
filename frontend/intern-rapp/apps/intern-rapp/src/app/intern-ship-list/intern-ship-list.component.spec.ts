import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InternShipListComponent } from './intern-ship-list.component';

describe('InternShipListComponent', () => {
  let component: InternShipListComponent;
  let fixture: ComponentFixture<InternShipListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InternShipListComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(InternShipListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
