import { AuthService } from './services/auth.service';
import { ICampus } from './models/DBEntities/campus';
import { DataService } from './services/data.service';
import { Component } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'kiosk_admin_app';
  isAuthed$: boolean = false;

  constructor(private _authService: AuthService)
  {
    this._authService.isAuthed$.subscribe(_authedStatus$ => this.isAuthed$ = _authedStatus$);

  }
}
