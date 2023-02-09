import { DataService } from './data.service';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  isAuthed$: BehaviorSubject<boolean> = new BehaviorSubject(false);
  baseUrl: string = "https://";
  constructor(private _data: DataService) { }
}
