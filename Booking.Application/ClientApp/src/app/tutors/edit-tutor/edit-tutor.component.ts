import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Tutor } from '../Tutor';

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
  selector: 'edit-tutor',
  templateUrl: './edit-tutor.component.html',
  styleUrls: ['./edit-tutor.component.css']
})
// tslint:disable-next-line:component-class-suffix
export class EditTutorComponent implements OnInit {

  form: FormGroup;
  request = {} as Tutor;
  errorMessage: string;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: Tutor,
    public http: HttpClient,
    @Inject('BASE_URL') public baseUrl: string,
    public diaglogRef: MatDialogRef<EditTutorComponent>,
    private formBuilder: FormBuilder
  ) { }


  ngOnInit() {
    this.initForm();
  }

  initForm() {
    this.form = this.formBuilder.group({
      id: [this.data.id, Validators.required],
      firstName: [this.data.firstName, Validators.required],
      middleName: [this.data.middleName],
      lastName: [this.data.lastName, Validators.required],
      contactNo: [this.data.contactNo, Validators.required],
      gender: [this.data.gender, Validators.required],
      email: [this.data.email, Validators.required],
      dateOfBirth: [this.data.dateOfBirth, Validators.required],
      availableDateFrom: [this.data.availableDateFrom, Validators.required],
      availableDateTo: [this.data.availableDateTo, Validators.required],
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

  save(req: Tutor) {
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

  update(model: Tutor) {
    console.log(model);
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
    const url = `${this.baseUrl}api/tutor/${model.id}`;
    return this.http.put(url, JSON.stringify(model), httpOptions);
  }
}
