import { Router } from '@angular/router';
import { AuthService } from './../../../services/auth.service';
import { ITrainStation } from './../../../models/DBEntities/trainstation';
import { DataService } from './../../../services/data.service';
import { Observable, of, find, filter, BehaviorSubject,map } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { ICampus } from 'src/app/models/DBEntities/campus';

@Component({
  selector: 'app-change-station',
  templateUrl: './change-station.component.html',
  styleUrls: ['./change-station.component.scss']
})
export class ChangeStationComponent implements OnInit {
  isLoading: boolean = false;
  success: boolean = false;
  selected$: Observable<ICampus> = this._dataService.selectedCampus$;
  campusList$: Observable<ICampus[]> = this._dataService.campuses$;
  selectedValue: number = 1;
  constructor(private _dataService: DataService, private _authService: AuthService, private router: Router) {
    this._authService.loading$.subscribe(loadingState => {
      this.isLoading = loadingState
    });
    this._dataService.success$.subscribe(successState => {
      this.success = successState;
    })
   }

  ngOnInit(): void {
    this.campusList$.subscribe(array =>
      {
        console.log(array)
        let sortedArray: ICampus[] = this.sortObjectsBySelected(array)
        this.campusList$ = of(sortedArray);
      })
      if(!this._authService.isAuthed$.getValue())
        this.router.navigate(["/"])
    }
  signOut()
  {
    this._authService.logOut();
    setTimeout(()=> {
      this._authService.loading$.next(false);
      this.router.navigate(["/"])
    }, 3000)


  }
  async clickHandler() {
    const selected: ICampus = await this.campusList$.pipe(
      map(list => list.find(id => id.campusId == this.selectedValue))
    ).toPromise() as ICampus;
    console.log(selected);
    this._dataService.selectedCampus$.next(selected);
    this.selected$ = this._dataService.selectedCampus$;

    try {
      const trainStation: ITrainStation = await this._dataService.getClosestStation({
        campusId: selected.campusId,
        campusName: selected.campusName,
        trainstationId: selected.trainstationId
      }).toPromise() as ITrainStation;

      const request: ICampus = {
        campusId: selected.campusId,
        campusName: selected.campusName,
        isSelected: selected.isSelected,
        trainstationId: selected.trainstationId,
        trainstation: {
          trainstationId: trainStation.trainstationId,
          trainstationName: trainStation.trainstationName,
          travelTime: trainStation.travelTime,
          campuses: trainStation.campuses
        }
      };

      console.log(request);

      await this._dataService.changeCampus(request).toPromise();
      await this._authService.loading$.next(false);
      await this._dataService.success$.next(true);
      setTimeout(() => this._dataService.success$.next(false), 5000);
    } catch (err) {
      this._authService.err.next(true);
      this._authService.errMsg.next(err);
    }
  }
  sortObjectsBySelected(objects: ICampus[]): ICampus[] {
    const selectedObjects: ICampus[] = [];
    const unselectedObjects: ICampus[] = [];

    for (const obj of objects) {
      if (obj.isSelected) {
        selectedObjects.push(obj);
      } else {
        unselectedObjects.push(obj);
      }
    }

    // sort selected objects first
    selectedObjects.sort((a, b) => {
      if (a.isSelected && !b.isSelected) {
        return -1;
      } else if (!a.isSelected && b.isSelected) {
        return 1;
      } else {
        return 0;
      }
    });

    // sort unselected objects next
    unselectedObjects.sort((a, b) => {
      if (a.isSelected && !b.isSelected) {
        return -1;
      } else if (!a.isSelected && b.isSelected) {
        return 1;
      } else {
        return 0;
      }
    });

    // combine the sorted arrays and return the result
    return [...selectedObjects, ...unselectedObjects];
  }
}
