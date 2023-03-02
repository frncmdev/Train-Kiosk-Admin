import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { LoginComponent } from './components/mainContent/login/login.component';
import { ChangeStationComponent } from './components/mainContent/change-station/change-station.component';
import { PlaceholderComponent } from './components/mainContent/placeholder/placeholder.component';
import { PageNotFoundComponent } from './components/mainContent/page-not-found/page-not-found.component';
import { LoadingComponent } from './components/popups/loading/loading.component';
import { ErrorComponent } from './components/popups/error/error.component';
import { ChangeStationSuccessComponent } from './components/popups/success/change-station-success/change-station-success.component';
import { SanitizerPipe } from './pipes/sanitizer.pipe';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ChangeStationComponent,
    PlaceholderComponent,
    PageNotFoundComponent,
    LoadingComponent,
    ErrorComponent,
    ChangeStationSuccessComponent,
    SanitizerPipe
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
