import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChangeStationComponent } from './change-station.component';

describe('ChangeStationComponent', () => {
  let component: ChangeStationComponent;
  let fixture: ComponentFixture<ChangeStationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChangeStationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChangeStationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
