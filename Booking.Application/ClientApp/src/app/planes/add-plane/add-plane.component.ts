import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Plane } from '../plane';
import { Observable } from 'rxjs';
import { Airport } from '../../airports/airport';

export const MY_FORMATS = {
  parse: {
    dateInput: 'LL',
  },
  display: {
    dateInput: 'LL',
    monthYearLabel: 'MMM YYYY',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'MMMM YYYY',
  },
};
@Component({
  // tslint:disable-next-line:component-selector
  selector: 'add-plane',
  templateUrl: './add-plane.component.html',
  styleUrls: ['./add-plane.component.css']
})
// tslint:disable-next-line:component-class-suffix
export class AddPlaneComponent implements OnInit {

  form: FormGroup;
  request = {} as Plane;
  errorMessage: string;
  airports: Airport[] = [];

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: Airport,
    public http: HttpClient,
    @Inject('BASE_URL') public baseUrl: string,
    public diaglogRef: MatDialogRef<AddPlaneComponent>,
    private formBuilder: FormBuilder
  ) { }


  ngOnInit() {
    this.initForm();
    this.loadAirports();
  }

  initForm() {
    this.form = this.formBuilder.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      isAvailable: [true],
      airportId: ['', Validators.required]
    });
  }

  loadAirports() {
    this.getAirports()
      .subscribe(data => {
        this.airports = data;
      },
        error => {
        });
  }


  onSubmit(form: FormGroup) {
    if (!form.valid) {
      return;
    }

    this.request = form.getRawValue();
    this.save(this.request);
  }

  save(req: Plane) {
    this.add(req)
      .subscribe(
        data => {
          this.diaglogRef.close(1);
        },
        error => {
          console.log(error);
        });
  }

  close() {
    this.diaglogRef.close();
  }

  add(model: Plane) {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
    const url = `${this.baseUrl}api/Plane`;
    return this.http.post(url, JSON.stringify(model), httpOptions);
  }

  getAirports(): Observable<Airport[]> {
    return this.http.get<Airport[]>(`${this.baseUrl}api/Airport`);
  }
}
