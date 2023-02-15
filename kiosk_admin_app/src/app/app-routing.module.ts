import { RegisterComponent } from './components/mainContent/register/register.component';
import { ChangeStationComponent } from './components/mainContent/change-station/change-station.component';
import { LoginComponent } from './components/mainContent/login/login.component';
import { PlaceholderComponent } from './components/mainContent/placeholder/placeholder.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {path: "", component:LoginComponent},
  {path: "changeStation", component:ChangeStationComponent},
  {path: "addUser", component:RegisterComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
