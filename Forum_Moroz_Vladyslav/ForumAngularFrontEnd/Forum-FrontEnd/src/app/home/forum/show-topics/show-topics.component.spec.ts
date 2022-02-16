import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowTopicsComponent } from './show-topics.component';

describe('ShowTopicsComponent', () => {
  let component: ShowTopicsComponent;
  let fixture: ComponentFixture<ShowTopicsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShowTopicsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowTopicsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
