import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import {
  MatButtonModule,
  MatCardModule,
  MatInputModule,
  MatSnackBarModule,
  MatToolbarModule,
  MatNativeDateModule,
  MatDatepickerModule,
  MatIconModule,
  MatIconRegistry,
  MatOptionModule,
  MatSelectModule,
} from '@angular/material';

import { MatMomentDateModule, MAT_MOMENT_DATE_ADAPTER_OPTIONS } from '@angular/material-moment-adapter';

import { AppComponent } from './app.component';

import { ContactsService } from './services/contacts.service';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { ContactsListComponent } from './contacts-list/contacts-list.component';
import { ContactComponent } from './contact/contact.component';

const routes = [
  {
    path: '',
    component: HomeComponent,
  },
  {
    path: 'contact/new',
    component: ContactComponent,
  },
  {
    path: 'contact/:contactId',
    component: ContactComponent,
  },
];

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    ContactsListComponent,
    ContactComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(routes),
    BrowserAnimationsModule,
    MatButtonModule,
    MatCardModule,
    MatInputModule,
    MatSnackBarModule,
    MatToolbarModule,
    MatNativeDateModule,
    MatDatepickerModule,
    MatIconModule,
    MatOptionModule,
    MatSelectModule,
    MatMomentDateModule,
  ],
  providers: [
    ContactsService,
    MatIconRegistry,
    { provide: MAT_MOMENT_DATE_ADAPTER_OPTIONS, useValue: { useUtc: true } }
  ],
  bootstrap: [AppComponent]
})

export class AppModule { }
