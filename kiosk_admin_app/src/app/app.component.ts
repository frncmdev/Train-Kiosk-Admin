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

  constructor(private _authService: AuthService)
  {
  }
}
