import { HobbyArticleModule } from './../hobby-article/hobby-article.module';
import { RouterModule } from '@angular/router';
import { MatListModule } from '@angular/material/list';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';

import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatRadioModule } from '@angular/material/radio';
import { MatButtonModule } from '@angular/material/button';
import {MatToolbarModule} from '@angular/material/toolbar';
import { NavigationListComponent } from './navigation-list/navigation-list.component';
import { IndexComponent } from './index/index.component';
import { UserCardComponent } from './user-card/user-card.component';
import { HomeComponent } from './home/home.component';
import {MatCardModule} from '@angular/material/card';
import { AboutComponent } from './about/about.component';


@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
    NavigationListComponent,
    IndexComponent,
    UserCardComponent,
    HomeComponent,
    AboutComponent,
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  imports: [
    CommonModule,
    BrowserAnimationsModule,
    RouterModule,
    MatTableModule,
    MatSortModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatIconModule,
    MatSelectModule,
    MatRadioModule,
    MatButtonModule,
    MatToolbarModule,
    MatListModule,
    RouterModule,
    MatCardModule,
    HobbyArticleModule
  ],
  exports: [
    HeaderComponent,
    FooterComponent,
    NavigationListComponent,
    IndexComponent,
    MatToolbarModule,
    MatListModule,
    HomeComponent,
    UserCardComponent
]
})
export class CoreModule { }