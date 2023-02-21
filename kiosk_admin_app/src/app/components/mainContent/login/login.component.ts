import { BehaviorSubject, delay } from 'rxjs';
import { AuthService } from './../../../services/auth.service';
import { ILoginRequest } from './../../../models/Requests/loginrequest';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  isLoading: boolean = false;
  _loginUsername: string="";
  _loginPassword: string ="";
  isAuthed: boolean;
  loginRequest: ILoginRequest = {
    username: "",
    password: ""
  }
  constructor(private _authService: AuthService, private router: Router) {
    this.isAuthed = false;
    this._authService.loading$.subscribe(loadingState => {
      this.isLoading = loadingState
    });

  }


  SubmitHandler(): void {
    const { _loginUsername, _loginPassword } = this;
    const loginRequest = { username: _loginUsername, password: _loginPassword };

    if (this.isAuthed) {
      this.router.navigate(['/changeStation']);
    }

    this._authService.login(loginRequest).subscribe({
      next: (item) => {
        sessionStorage.setItem('userId', JSON.stringify(item));
        this._authService.isAuthed$.next(true);

        setTimeout(() => {
          this._authService.loading$.next(false);
          this.router.navigate(['/changeStation']);
        }, 3000);
      },
      error: (err) => {
        try {
          this._authService.loading$.next(false);
          this._authService.err.next(true);
          this._authService.errMsg.next(err);
          console.error(err);
          setTimeout(() => this._authService.err.next(false), 5000);
        } catch (e) {
          console.error('Error while handling login error', e);
        }
      },
    });
  }
  ngOnInit(): void {
    this._authService.isAuthed$.subscribe(isAuthedRes => this.isAuthed = isAuthedRes);
    if(this.isAuthed)
      this.router.navigate(["/changeStation"])
  }


}
