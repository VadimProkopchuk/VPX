import {NgModule} from '@angular/core';
import {Routes, RouterModule, PreloadAllModules} from '@angular/router';
import {LayoutComponent} from '../components/layout/layout.component';
import {HomePageComponent} from '../components/home-page/home-page.component';
import {CreateLectureComponent} from '../components/lecture-components/create-lecture/create-lecture.component';
import {LecturesPageComponent} from '../components/lecture-components/lectures-page/lectures-page.component';
import {LecturesComponent} from '../components/lecture-components/lectures/lectures.component';
import {LectureComponent} from '../components/lecture-components/lecture/lecture.component';

const routes: Routes = [
  {
    path: '', component: LayoutComponent, children: [
      {path: '', redirectTo: '/', pathMatch: 'full'},
      {path: '', component: HomePageComponent},
      { path: 'lecture/:url', component: LectureComponent },
      {
        path: 'lectures',
        component: LecturesPageComponent,
        children: [
          { path: 'all', component: LecturesComponent },
          { path: 'create', component: CreateLectureComponent },
        ]
      }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    preloadingStrategy: PreloadAllModules
  })],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
