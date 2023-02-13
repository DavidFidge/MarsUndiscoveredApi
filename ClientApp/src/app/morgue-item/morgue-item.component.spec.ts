import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MorgueItemComponent } from './morgue-item.component';

describe('MorgueItemComponent', () => {
  let component: MorgueItemComponent;
  let fixture: ComponentFixture<MorgueItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MorgueItemComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MorgueItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
