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

import { AppComponent } from './app.component';

import { WebService } from './web.service';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { UserComponent } from './user/user.component';
import { ContactsListComponent } from './contacts-list/contacts-list.component';
import { ContactComponent } from './contact/contact.component';

const routes = [
  {
    path: '',
    component: HomeComponent,
  },
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'user',
    component: UserComponent
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
    RegisterComponent,
    LoginComponent,
    UserComponent,
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
  ],
  providers: [
    WebService,
    MatIconRegistry
  ],
  bootstrap: [AppComponent]
})

export class AppModule { }
