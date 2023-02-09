import { ICampus } from './../models/DBEntities/campus';
import { AuthService } from './auth.service';
import { BehaviorSubject } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class DataService {
  _baseURL: string = "";
  selectedCampus$: BehaviorSubject<ICampus>;
  campuses$: BehaviorSubject<number[]>;
  constructor(private _http: HttpClient, private _authService: AuthService) { }
  getCampuses()
  {

  }
}
