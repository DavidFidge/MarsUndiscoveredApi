import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MorgueComponent } from './morgue.component';

describe('MorgueComponent', () => {
  let component: MorgueComponent;
  let fixture: ComponentFixture<MorgueComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MorgueComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MorgueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
