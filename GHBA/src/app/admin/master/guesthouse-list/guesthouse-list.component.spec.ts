import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GuesthouseListComponent } from './guesthouse-list.component';

describe('GuesthouseListComponent', () => {
  let component: GuesthouseListComponent;
  let fixture: ComponentFixture<GuesthouseListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GuesthouseListComponent]
    });
    fixture = TestBed.createComponent(GuesthouseListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
