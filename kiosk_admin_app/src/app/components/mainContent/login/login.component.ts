import { BehaviorSubject } from 'rxjs';
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

  _loginUsername: string="";
  _loginPassword: string ="";
  isAuthed: boolean;
  loginRequest: ILoginRequest = {
    username: "",
    password: ""
  }
  constructor(private _authService: AuthService, private router: Router) {
    this.isAuthed = false;
  }


  SubmitHandler()
  {
    this.loginRequest.username = this._loginUsername;
    this.loginRequest.password = this._loginPassword

    if(this.isAuthed)
      if(!this._authService.auth())
        return;
      this.router.navigate(["/changeStation"])
    this._authService.login(this.loginRequest).subscribe(item =>
      {
        sessionStorage.setItem("userId", JSON.stringify(item));
        this._authService.isAuthed$.next(true);
        this.router.navigate(["/changeStation"])
      }, error => {
        console.error(error)
      });
  }
  ngOnInit(): void {
    this._authService.isAuthed$.subscribe(isAuthedRes => this.isAuthed = isAuthedRes);
  }


}
