import { AuthGuard } from './guards/auth.guard';
import { PageNotFoundComponent } from './components/mainContent/page-not-found/page-not-found.component';
import { ChangeStationComponent } from './components/mainContent/change-station/change-station.component';
import { LoginComponent } from './components/mainContent/login/login.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {path: "", component:LoginComponent},
  {path: "changeStation", component:ChangeStationComponent, canActivate:[AuthGuard]},
  {path: "**", component:PageNotFoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
