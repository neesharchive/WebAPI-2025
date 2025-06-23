import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookingStatusDialogComponent } from './booking-status-dialog.component';

describe('BookingStatusDialogComponent', () => {
  let component: BookingStatusDialogComponent;
  let fixture: ComponentFixture<BookingStatusDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BookingStatusDialogComponent]
    });
    fixture = TestBed.createComponent(BookingStatusDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
