import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { LoginComponent } from './components/mainContent/login/login.component';
import { ChangeStationComponent } from './components/mainContent/change-station/change-station.component';
import { RegisterComponent } from './components/mainContent/register/register.component';
import { PlaceholderComponent } from './components/mainContent/placeholder/placeholder.component';
import { PageNotFoundComponent } from './components/mainContent/page-not-found/page-not-found.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ChangeStationComponent,
    RegisterComponent,
    PlaceholderComponent,
    PageNotFoundComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
