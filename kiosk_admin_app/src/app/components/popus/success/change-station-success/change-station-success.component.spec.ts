import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChangeStationSuccessComponent } from './change-station-success.component';

describe('ChangeStationSuccessComponent', () => {
  let component: ChangeStationSuccessComponent;
  let fixture: ComponentFixture<ChangeStationSuccessComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChangeStationSuccessComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChangeStationSuccessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
