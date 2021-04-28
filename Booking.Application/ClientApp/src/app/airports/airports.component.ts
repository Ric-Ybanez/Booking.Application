import { DataSource } from '@angular/cdk/table';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { Observable } from 'rxjs';
import { AddAirportComponent } from './add-airport/add-airport.component';
import { Airport } from './airport';
import { EditAirportComponent } from './edit-airport/edit-airport.component';



@Component({
  selector: 'app-home',
  templateUrl: './airports.component.html',
  styleUrls: ['./airports.component.css'],
})
export class AirportComponent implements OnInit {
  displayedColumns: string[] = ['name', 'description', 'isAvailable', 'actions'];
  dataSource: Airport[]  = [];

  constructor(
    public dialog: MatDialog,
    public http: HttpClient,
    @Inject('BASE_URL') public baseUrl: string
  ) { }

  ngOnInit() {
    setTimeout(_ => this.loadAirports(), 0);
  }

  loadAirports() {
    this.getPlanes()
      .subscribe(data => {
        this.dataSource = data;
      },
        error => {
        });
  }

  onView(row: Airport) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.panelClass = 'product-dialog';
    dialogConfig.autoFocus = false;
    dialogConfig.data = row;

    const dialogRef = this.dialog.open(EditAirportComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
      if (result === 1) {
        setTimeout(_ => this.loadAirports(), 0);
      }
    });
  }

  onAdd() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = false;
    dialogConfig.panelClass = 'product-dialog';
    const dialogRef = this.dialog.open(AddAirportComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
      if (result === 1) {
        setTimeout(_ => this.loadAirports(), 0);
      }
    });
  }

  getPlanes(): Observable<Airport[]> {
    return this.http.get<Airport[]>(`${this.baseUrl}api/Airport`);
  }
}
