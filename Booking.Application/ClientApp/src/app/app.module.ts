import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { AirportComponent } from './airports/airports.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LibraryModule } from './shared/library.module';
import { EditAirportComponent } from './airports/edit-airport/edit-airport.component';
import { FlexLayoutModule } from "@angular/flex-layout";
import { AddAirportComponent } from './airports/add-airport/add-airport.component';
import { PlaneComponent } from './planes/planes.component';
import { AddPlaneComponent } from './planes/add-plane/add-plane.component';
import { EditPlaneComponent } from './planes/edit-plane/edit-plane.component';
import { TutorComponent } from './tutors/tutors.component';
import { AddTutorComponent } from './tutors/add-tutor/add-tutor.component';
import { EditTutorComponent } from './tutors/edit-tutor/edit-tutor.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    AirportComponent,
    PlaneComponent,
    TutorComponent,
    EditAirportComponent,
    AddAirportComponent,
    AddPlaneComponent,
    EditPlaneComponent,
    AddTutorComponent,
    EditTutorComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    LibraryModule,
    FormsModule,
    ReactiveFormsModule,
    FlexLayoutModule,
    RouterModule.forRoot([
      { path: 'airports', component: AirportComponent, pathMatch: 'full' },
      { path: 'planes', component: PlaneComponent },
      { path: 'tutors', component: TutorComponent },
    ]),
    BrowserAnimationsModule
  ],
  entryComponents: [
    EditAirportComponent,
    AddAirportComponent,
    AddPlaneComponent,
    EditPlaneComponent,
    AddTutorComponent,
    EditTutorComponent
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
