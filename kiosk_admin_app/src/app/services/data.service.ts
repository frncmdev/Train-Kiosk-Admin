import { ICampus } from './../models/DBEntities/campus';
import { AuthService } from './auth.service';
import { BehaviorSubject } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { of } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class DataService {
  _baseURL: string = "http://67.219.107.113/admin/api/v1/";
  selectedCampus$: BehaviorSubject<ICampus>;
  campuses$: BehaviorSubject<ICampus[]> = new BehaviorSubject(new Array<ICampus>);
  constructor(private _http: HttpClient, private _authService: AuthService) {
    this.selectedCampus$ = new BehaviorSubject<ICampus>({} as ICampus);
    this.getCampuses();
    this.getCampus();
  }
  getCampuses()
  {
    this._http.get<ICampus[]>(`${this._baseURL}Station`).subscribe(item => this.campuses$.next(item));
  }
  getCampus(): void
  {
    this._http.get<ICampus>(`${this._baseURL}Station/getSelected`).subscribe(item => this.selectedCampus$?.next(item))
  }
  changeCampus(_newSelectedStation: ICampus): void
  {
    if(this._authService.isAuthed$)
    {
      this._http.post(`${this._baseURL}/changeStation`, _newSelectedStation).subscribe();
    }
    else
    {
      return;
    }

  }
}


