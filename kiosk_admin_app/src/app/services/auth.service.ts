import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { IRegisterRequest } from './../models/Requests/registerRequest';
import { ILoginRequest } from './../models/Requests/loginrequest';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  err: BehaviorSubject<boolean>;
  errMsg: BehaviorSubject<any>;
  loading$: BehaviorSubject<boolean>;
  isAuthed$: BehaviorSubject<boolean> = new BehaviorSubject(false);
  baseUrl: string = "https://api.callumhopkins.au/admin/api/v1/";
  constructor(private _http: HttpClient) {
    this.loading$ = new BehaviorSubject<boolean>(false);
    this.err = new BehaviorSubject<boolean>(false);
    this.errMsg = new BehaviorSubject<any>("");
   }
  login(_request: ILoginRequest): Observable<HttpResponse<string>>
  {
    this.loading$.next(true)
    let requestHeaders = new HttpHeaders();
    requestHeaders = requestHeaders.append('Content-Type', 'application/json')
    return this._http.post<HttpResponse<string>>(`${this.baseUrl}Auth/login`, _request);
  }

  logOut()
  {
    this.loading$.next(true);
    sessionStorage.removeItem("userId");
    this.isAuthed$.next(false);

  }
}
