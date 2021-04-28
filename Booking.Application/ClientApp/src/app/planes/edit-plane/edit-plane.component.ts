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
  selector: 'edit-plane',
  templateUrl: './edit-plane.component.html',
  styleUrls: ['./edit-plane.component.css']
})
// tslint:disable-next-line:component-class-suffix
export class EditPlaneComponent implements OnInit {

  form: FormGroup;
  request = {} as Plane;
  errorMessage: string;
  airports: Airport[] = [];

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: Plane,
    public http: HttpClient,
    @Inject('BASE_URL') public baseUrl: string,
    public diaglogRef: MatDialogRef<EditPlaneComponent>,
    private formBuilder: FormBuilder
  ) { }


  ngOnInit() {
    this.initForm();
    this.loadAirports();
  }


  loadAirports() {
    this.getAirports()
      .subscribe(data => {
        this.airports = data;
      },
        error => {
        });
  }

  initForm() {
    this.form = this.formBuilder.group({
      id: [this.data.id, Validators.required],
      name: [this.data.name, Validators.required],
      description: [this.data.description, Validators.required],
      isAvailable: [this.data.isAvailable],
      airportId: [this.data.airportId]
    });
  }


  onSubmit(form: FormGroup) {
    if (!form.valid) {
      return;
    }

    this.request = form.getRawValue();
    console.log(this.request);
    this.save(this.request);
  }

  save(req: Plane) {
    this.update(req)
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

  update(model: Plane) {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
    const url = `${this.baseUrl}api/plane/${model.id}`;
    return this.http.put(url, JSON.stringify(model), httpOptions);
  }

  getAirports(): Observable<Airport[]> {
    return this.http.get<Airport[]>(`${this.baseUrl}api/Airport`);
  }
}
