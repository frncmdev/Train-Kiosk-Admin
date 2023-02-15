import { DataService } from './../../../services/data.service';
import { Observable, of, find, filter } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { ICampus } from 'src/app/models/DBEntities/campus';

@Component({
  selector: 'app-change-station',
  templateUrl: './change-station.component.html',
  styleUrls: ['./change-station.component.scss']
})
export class ChangeStationComponent implements OnInit {
  selected$: Observable<ICampus> = this._dataService.selectedCampus$;
  campusList$: Observable<ICampus[]> = this._dataService.campuses$;
  selectedValue: number = 1;
  constructor(private _dataService: DataService) { }

  ngOnInit(): void {
    this.campusList$.subscribe(array =>
      {
        console.log(array)
        let sortedArray: ICampus[] = this.sortObjectsBySelected(array)
        this.campusList$ = of(sortedArray);
      })
  }
  clickHandler()
  {
    // console.log(this.selectedValue)
    let _tempArr: ICampus[] = []
    this.campusList$.subscribe(_array => {

      _tempArr = _array
    })
    let _selected = _tempArr[this.selectedValue-1]
    console.log(_selected);

    this._dataService.changeCampus(_selected).subscribe(arg => console.log(arg));

    // console.log(selected);
    // this.campusList$.pipe(
    //   filter(x => x.campusId === this.selectedValue-1))
    // .subscribe(value => console.log(value));
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
