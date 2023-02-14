import { DataService } from './services/data.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'kiosk_admin_app';
  constructor(private _data: DataService)
  {
    _data.getCampuses();
  }
}
