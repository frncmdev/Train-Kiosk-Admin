import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { IRegisterRequest } from './../models/Requests/registerRequest';
import { ILoginRequest } from './../models/Requests/loginrequest';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  isAuthed$: BehaviorSubject<boolean> = new BehaviorSubject(false);
  baseUrl: string = "http://67.219.107.113/admin/api/v1/";
  constructor(private _http: HttpClient) { }
  login(_request: ILoginRequest): Observable<HttpResponse<string>>
  {
    let requestHeaders = new HttpHeaders();
    requestHeaders = requestHeaders.append('Content-Type', 'application/json')
    return this._http.post<HttpResponse<any>>(`${this.baseUrl}Auth/login/`, _request);
  }
  register(_request: IRegisterRequest): void
  {

  }
  auth(_request: string)
  {

  }
  logOut()
  {
    sessionStorage.removeItem("userId");
    this.isAuthed$.next(false);
  }
}
