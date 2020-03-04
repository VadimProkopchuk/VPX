import {NgModule} from '@angular/core';
import {Routes, RouterModule, PreloadAllModules} from '@angular/router';
import {LayoutComponent} from '../components/layout/layout.component';
import {HomePageComponent} from '../components/home-page/home-page.component';
import {CreateLectureComponent} from '../components/lecture-components/create-lecture/create-lecture.component';
import {LecturesPageComponent} from '../components/lecture-components/lectures-page/lectures-page.component';
import {LecturesComponent} from '../components/lecture-components/lectures/lectures.component';
import {LectureComponent} from '../components/lecture-components/lecture/lecture.component';
import {EditLectureComponent} from '../components/lecture-components/edit-lecture/edit-lecture.component';
import {AuthGuard} from './guards/auth.guard';
import {Role} from './models/role';
import {AccessDeniedPageComponent} from '../components/access-denied-page/access-denied-page.component';

const routes: Routes = [
  {
    path: '', component: LayoutComponent, children: [
      { path: '', redirectTo: 'home', pathMatch: 'full' },
      { path: 'home', component: HomePageComponent },
      { path: 'lecture/:url', component: LectureComponent, canActivate: [AuthGuard] },
      {
        path: 'lectures',
        component: LecturesPageComponent,
        canActivate: [AuthGuard],
        children: [
          { path: 'all', component: LecturesComponent, canActivate: [AuthGuard] },
          { path: 'create', component: CreateLectureComponent, canActivate: [AuthGuard], data: { roles: [Role.Teacher, Role.Admin] } },
          { path: 'edit/:url', component: EditLectureComponent, canActivate: [AuthGuard], data: { roles: [Role.Teacher, Role.Admin] } }
        ]
      },
      { path: 'access-denied', component: AccessDeniedPageComponent }
    ]
  },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    preloadingStrategy: PreloadAllModules
  })],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
