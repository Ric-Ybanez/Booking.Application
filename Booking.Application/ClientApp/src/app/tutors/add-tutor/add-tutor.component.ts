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
  selector: 'add-tutor',
  templateUrl: './add-tutor.component.html',
  styleUrls: ['./add-tutor.component.css']
})
// tslint:disable-next-line:component-class-suffix
export class AddTutorComponent implements OnInit {

  form: FormGroup;
  request = {} as Tutor;
  errorMessage: string;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: Tutor,
    public http: HttpClient,
    @Inject('BASE_URL') public baseUrl: string,
    public diaglogRef: MatDialogRef<AddTutorComponent>,
    private formBuilder: FormBuilder
  ) { }


  ngOnInit() {
    this.initForm();
  }

  initForm() {
    this.form = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
      firstName: ['', Validators.required],
      middleName: [''],
      lastName: ['', Validators.required],
      contactNo: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      gender: ['', Validators.required],
      email: ['', Validators.required],
      availableDateFrom: ['', Validators.required],
      availableDateTo: ['', Validators.required],
    });
  }



  onSubmit(form: FormGroup) {
    if (!form.valid) {
      return;
    }

    this.request = form.getRawValue();
    this.save(this.request);
  }

  save(req: Tutor) {
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

  add(model: Tutor) {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
    const url = `${this.baseUrl}api/Tutor`;
    return this.http.post(url, JSON.stringify(model), httpOptions);
  }
}
