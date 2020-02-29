import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TagInputModule } from 'ngx-chips';

import {AppRoutingModule} from './shared/app-routing.module';
import {AppNotificationsModule} from './shared/app-notifications.module';
import {AppEditorModule} from './shared/app-editor.module';

import {AuthInterceptor} from './shared/interceptors/auth.interceptor';

import {AppComponent} from './app.component';
import {NavMenuComponent} from './components/nav-menu/nav-menu.component';
import {HomeComponent} from './components/home/home.component';
import {LayoutComponent} from './components/layout/layout.component';
import {AuthComponent} from './components/auth-components/auth/auth.component';
import {UserInfoComponent} from './components/user-info/user-info.component';
import {LecturesPageComponent} from './components/lectures-page/lectures-page.component';
import {CreateLectureComponent} from './components/create-lecture/create-lecture.component';
import {LecturesComponent} from './components/lectures/lectures.component';
import {LectureComponent} from './components/lecture/lecture.component';

const AUTH_INTERCEPTOR = {
  provide: HTTP_INTERCEPTORS,
  useClass: AuthInterceptor,
  multi: true
};

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    LayoutComponent,
    AuthComponent,
    UserInfoComponent,
    LecturesPageComponent,
    UserInfoComponent,
    CreateLectureComponent,
    LecturesComponent,
    LectureComponent,
  ],
  imports: [
    TagInputModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    AppRoutingModule,
    AppNotificationsModule,
    AppEditorModule,
  ],
  providers: [
    AUTH_INTERCEPTOR,
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
