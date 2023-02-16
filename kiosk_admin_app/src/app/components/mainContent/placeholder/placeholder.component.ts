import { AuthService } from './../../../services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-placeholder',
  templateUrl: './placeholder.component.html',
  styleUrls: ['./placeholder.component.scss']
})
export class PlaceholderComponent implements OnInit {
  error: boolean = false;

  constructor(private _authService: AuthService) {
    this._authService.err.subscribe(_error => this.error = _error)

   }

  ngOnInit(): void {
  }

}
