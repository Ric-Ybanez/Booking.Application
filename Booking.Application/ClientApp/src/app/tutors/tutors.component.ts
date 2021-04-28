import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { Observable } from 'rxjs';
import { AddTutorComponent } from './add-tutor/add-tutor.component';
import { EditTutorComponent } from './edit-tutor/edit-tutor.component';
import { Tutor } from './Tutor';



@Component({
  selector: 'app-tutor',
  templateUrl: './tutors.component.html',
  styleUrls: ['./tutors.component.css'],
})
export class TutorComponent implements OnInit {
  displayedColumns: string[] = ['name', 'email', 'contactNo', 'gender', 'dateOfBirth', 'availableDateFrom', 'availableDateTo', 'actions'];
  dataSource: Tutor[] = [];


  constructor(
    public dialog: MatDialog,
    public http: HttpClient,
    @Inject('BASE_URL') public baseUrl: string
  ) { }

  ngOnInit() {
    setTimeout(_ => this.loadTutors(), 0);
  }

  loadTutors() {
    this.getPlanes()
      .subscribe(data => {
        this.dataSource = data;
      },
        error => {
        });
  }

  onView(row: Tutor) {
    console.log(row);
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = false;
    dialogConfig.data = row;

    const dialogRef = this.dialog.open(EditTutorComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
      if (result === 1) {
        setTimeout(_ => this.loadTutors(), 0);
      }
    });
  }

  onAdd() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = false;
    const dialogRef = this.dialog.open(AddTutorComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
      if (result === 1) {
        setTimeout(_ => this.loadTutors(), 0);
      }
    });
  }

  getPlanes(): Observable<Tutor[]> {
    return this.http.get<Tutor[]>(`${this.baseUrl}api/Tutor`);
  }
}
