import { ICampus } from './../../../../models/DBEntities/campus';
import { ITrainStation } from './../../../../models/DBEntities/trainstation';
import { DataService } from './../../../../services/data.service';
import { Component, OnInit } from '@angular/core';
// import { Data } from '@angular/router';

@Component({
  selector: 'app-change-station-success',
  templateUrl: './change-station-success.component.html',
  styleUrls: ['./change-station-success.component.scss']
})
export class ChangeStationSuccessComponent implements OnInit {
  success: boolean = false;
  station: ICampus = this._dataService.selectedCampus$.getValue();
  constructor(private _dataService: DataService) {
    this._dataService.success$.subscribe(status => {
      this.success = status
    })
    this._dataService.getCampus();
    this._dataService.selectedCampus$.subscribe(_selectedCampus => {
      this.station = _selectedCampus;
    })
  }

  ngOnInit(): void {
  }

}
