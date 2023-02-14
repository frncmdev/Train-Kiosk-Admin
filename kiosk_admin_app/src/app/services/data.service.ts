import { ICampus } from './../models/DBEntities/campus';
import { AuthService } from './auth.service';
import { BehaviorSubject } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class DataService {
  _baseURL: string = "http://67.219.107.113/admin/api/v1/";
  selectedCampus$: BehaviorSubject<ICampus> | undefined;
  campuses$: BehaviorSubject<ICampus[]> | undefined;
  constructor(private _http: HttpClient, private _authService: AuthService) { }
  getCampuses()
  {
    this._http.get(`${this._baseURL}Station`).subscribe(item => console.log(item));
  }
  getCampus(): void
  {

  }
  changeCampus(): void
  {


  }
}
