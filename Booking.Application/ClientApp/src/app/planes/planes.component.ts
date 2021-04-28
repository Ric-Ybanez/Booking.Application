import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { Observable } from 'rxjs';
import { AddPlaneComponent } from './add-plane/add-plane.component';
import { Plane } from './plane';
import { EditPlaneComponent } from './edit-plane/edit-plane.component';
import { Airport } from '../airports/airport';



@Component({
  selector: 'app-plane',
  templateUrl: './planes.component.html',
  styleUrls: ['./planes.component.css'],
})
export class PlaneComponent implements OnInit {
  displayedColumns: string[] = ['name', 'description', 'airport', 'isAvailable', 'actions'];
  dataSource: Plane[] = [];


  constructor(
    public dialog: MatDialog,
    public http: HttpClient,
    @Inject('BASE_URL') public baseUrl: string
  ) { }

  ngOnInit() {
    setTimeout(_ => this.loadAirplanes(), 0);
  }

  loadAirplanes() {
    this.getPlanes()
      .subscribe(data => {
        this.dataSource = data;
      },
        error => {
        });
  }

  onView(row: Plane) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.panelClass = 'product-dialog';
    dialogConfig.autoFocus = false;
    dialogConfig.data = row;

    const dialogRef = this.dialog.open(EditPlaneComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
      if (result === 1) {
        setTimeout(_ => this.loadAirplanes(), 0);
      }
    });
  }

  onAdd() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = false;
    dialogConfig.panelClass = 'product-dialog';
    const dialogRef = this.dialog.open(AddPlaneComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
      if (result === 1) {
        setTimeout(_ => this.loadAirplanes(), 0);
      }
    });
  }

  getPlanes(): Observable<Plane[]> {
    return this.http.get<Plane[]>(`${this.baseUrl}api/Plane`);
  }
}
