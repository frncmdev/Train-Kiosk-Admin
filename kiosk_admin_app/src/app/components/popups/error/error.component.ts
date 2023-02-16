import { DataService } from './../../../services/data.service';
import { AuthService } from './../../../services/auth.service';
import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-error',
  templateUrl: './error.component.html',
  styleUrls: ['./error.component.scss']
})
export class ErrorComponent implements OnInit {
  error: any;
  constructor(private _authService: AuthService, private _dataService: DataService) { }

  ngOnInit(): void {
    this._authService.errMsg.subscribe(_errMsg => {
      this.error = _errMsg
    })
  }

}
