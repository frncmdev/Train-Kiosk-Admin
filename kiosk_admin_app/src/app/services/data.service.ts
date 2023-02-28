import { ICampusDTO } from './../models/Requests/campusDTO';
import { ICampus } from './../models/DBEntities/campus';
import { AuthService } from './auth.service';
import { BehaviorSubject, Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { ITrainStation } from '../models/DBEntities/trainstation';
@Injectable({
  providedIn: 'root'
})
export class DataService {
  success$: BehaviorSubject<boolean>;
  _baseURL: string = "https://api.callumhopkins.au/admin/api/v1/";
  selectedCampus$: BehaviorSubject<ICampus>;
  campuses$: BehaviorSubject<ICampus[]> = new BehaviorSubject(new Array<ICampus>);
  constructor(private _http: HttpClient, private _authService: AuthService) {
    this.selectedCampus$ = new BehaviorSubject<ICampus>({} as ICampus);
    this.getCampuses();
    this.getCampus();
    this.success$ = new BehaviorSubject(false);
  }
  getCampuses()
  {
    this._http.get<ICampus[]>(`${this._baseURL}Station`).subscribe(item => this.campuses$.next(item));
  }
  getCampus(): void
  {
    this._http.get<ICampus>(`${this._baseURL}Station/getSelected`).subscribe(item => this.selectedCampus$?.next(item))
  }
  changeCampus(_newSelectedStation: ICampus): Observable<Object>
  {
    this._authService.loading$.next(true);
    return this._http.post(`${this._baseURL}Station/changeStation`, _newSelectedStation)

  }
  getClosestStation(_campusDTO: ICampusDTO): Observable<ITrainStation>
  {
    return this._http.get<ITrainStation>(`${this._baseURL}Station/getStationToSelected?CampusId=${_campusDTO.campusId}&CampusName=${_campusDTO.campusName}&TrainstationId=${_campusDTO.trainstationId}`);
  }
}


