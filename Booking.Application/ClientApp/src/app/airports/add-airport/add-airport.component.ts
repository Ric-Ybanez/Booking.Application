import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Airport } from '../airport';

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
  selector: 'add-airport',
  templateUrl: './add-airport.component.html',
  styleUrls: ['./add-airport.component.css']
})
// tslint:disable-next-line:component-class-suffix
export class AddAirportComponent implements OnInit {

  form: FormGroup;
  request = {} as Airport;
  errorMessage: string;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: Airport,
    public http: HttpClient,
    @Inject('BASE_URL') public baseUrl: string,
    public diaglogRef: MatDialogRef<AddAirportComponent>,
    private formBuilder: FormBuilder
  ) { }


  ngOnInit() {
    this.initForm();
  }

  initForm() {
    this.form = this.formBuilder.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      isAvailable: [true],
    });
  }


  onSubmit(form: FormGroup) {
    if (!form.valid) {
      return;
    }

    this.request = form.getRawValue();
    this.save(this.request);
  }

  save(req: Airport) {
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

  add(model: Airport) {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
    const url = `${this.baseUrl}api/airport`;
    return this.http.post(url, JSON.stringify(model), httpOptions);
  }
}
