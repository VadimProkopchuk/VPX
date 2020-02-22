import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';

import {AppRoutingModule} from './shared/app-routing.module';

import {AppComponent} from './app.component';
import {NavMenuComponent} from './components/nav-menu/nav-menu.component';
import {HomeComponent} from './components/home/home.component';
import {LayoutComponent} from './components/layout/layout.component';
import {FetchDataComponent} from './components/fetch-data/fetch-data.component';
import {AuthInterceptor} from './shared/interceptors/auth.interceptor';
import {AuthComponent} from './components/auth-components/auth/auth.component';
import {UserInfoComponent} from './components/user-info/user-info.component';
import {LecturesPageComponent} from './components/lectures-page/lectures-page.component';
import {AppNotificationsModule} from './shared/app-notifications.module';

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
    FetchDataComponent,
    AuthComponent,
    AlertComponent,
    UserInfoComponent,
    LecturesPageComponent,
    UserInfoComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
    ReactiveFormsModule,
    AppNotificationsModule,
  ],
  providers: [
    AUTH_INTERCEPTOR,
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
