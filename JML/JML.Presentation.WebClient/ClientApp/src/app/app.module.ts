import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TagInputModule } from 'ngx-chips';
import { FileUploadModule } from 'ng2-file-upload';

import {AppRoutingModule} from './shared/app-routing.module';
import {AppNotificationsModule} from './shared/app-notifications.module';
import {AppEditorModule} from './shared/app-editor.module';
import {MaterialModule} from './shared/material.module';

import {AuthInterceptor} from './shared/interceptors/auth.interceptor';

import {AppComponent} from './app.component';
import {NavMenuComponent} from './components/nav-menu/nav-menu.component';
import {HomePageComponent} from './components/home-page/home-page.component';
import {LayoutComponent} from './components/layout/layout.component';
import {AuthComponent} from './components/auth-components/auth/auth.component';
import {CreateLectureComponent} from './components/lecture-components/create-lecture/create-lecture.component';
import {LecturesPageComponent} from './components/lecture-components/lectures-page/lectures-page.component';
import {LecturesComponent} from './components/lecture-components/lectures/lectures.component';
import {LectureComponent} from './components/lecture-components/lecture/lecture.component';
import {EditLectureComponent} from './components/lecture-components/edit-lecture/edit-lecture.component';
import {RegisterComponent} from './components/auth-components/register/register.component';
import {LoaderComponent} from './components/loader/loader.component';
import {UserSummaryDialogComponent} from './components/dialogs/user-summary-dialog/user-summary-dialog.component';
import {DeleteLectureDialogComponent} from './components/dialogs/delete-lecture-dialog/delete-lecture-dialog.component';
import {AuthGuard} from './shared/guards/auth.guard';
import {AccessDeniedPageComponent} from './components/access-denied-page/access-denied-page.component';
import {RestoreAccessComponent} from './components/auth-components/restore-access/restore-access.component';
import {GroupsPageComponent} from './components/group-components/groups-page/groups-page.component';
import {GroupsListComponent} from './components/group-components/groups-list/groups-list.component';
import {CreateGroupComponent} from './components/group-components/create-group/create-group.component';
import {TestsPageComponent} from './components/knowledge-test/tests-page/tests-page.component';
import {CreateTestComponent} from './components/knowledge-test/create-test/create-test.component';
import {SettingsPageComponent} from './components/settings-page/settings-page.component';
import {TestRunnerComponent} from './components/knowledge-test/test-runner/test-runner.component';
import {TestsComponent} from './components/knowledge-test/tests/tests.component';
import {TestResultsComponent} from './components/knowledge-test/test-results/test-results.component';
import {UserProfileComponent} from './components/user-profile/user-profile.component';
import {ProfilePageComponent} from './components/profile-page/profile-page.component';
import {FileUploadComponent} from './components/file-upload/file-upload.component';
import {SafeUrlPipe} from './shared/pipes/safe-html.pipe';
import {EditGroupPageComponent} from './components/group-components/edit-group-page/edit-group-page.component';
import {TestResultsPageComponent} from './components/knowledge-test/test-results-page/test-results-page.component';

const AUTH_INTERCEPTOR = {
  provide: HTTP_INTERCEPTORS,
  useClass: AuthInterceptor,
  multi: true
};

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomePageComponent,
    LayoutComponent,
    AuthComponent,
    LecturesPageComponent,
    CreateLectureComponent,
    LecturesComponent,
    LectureComponent,
    EditLectureComponent,
    RegisterComponent,
    LoaderComponent,
    UserSummaryDialogComponent,
    DeleteLectureDialogComponent,
    AccessDeniedPageComponent,
    RestoreAccessComponent,
    GroupsPageComponent,
    GroupsListComponent,
    CreateGroupComponent,
    TestsPageComponent,
    CreateTestComponent,
    SettingsPageComponent,
    TestRunnerComponent,
    TestsComponent,
    TestResultsComponent,
    UserProfileComponent,
    ProfilePageComponent,
    FileUploadComponent,
    SafeUrlPipe,
    EditGroupPageComponent,
    TestResultsPageComponent,
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
    MaterialModule,
    FileUploadModule,
  ],
  providers: [
    AUTH_INTERCEPTOR,
    AuthGuard,
  ],
  bootstrap: [AppComponent],
  entryComponents: [
    UserSummaryDialogComponent,
    DeleteLectureDialogComponent,
  ]
})
export class AppModule {
}
