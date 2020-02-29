import {NgModule} from '@angular/core';
import {Routes, RouterModule, PreloadAllModules} from '@angular/router';
import {LayoutComponent} from '../components/layout/layout.component';
import {HomeComponent} from '../components/home/home.component';
import {LecturesPageComponent} from '../components/lectures-page/lectures-page.component';

import {CreateLectureComponent} from '../components/create-lecture/create-lecture.component';
import {LecturesComponent} from '../components/lectures/lectures.component';
import {LectureComponent} from '../components/lecture/lecture.component';

const routes: Routes = [
  {
    path: '', component: LayoutComponent, children: [
      {path: '', redirectTo: '/', pathMatch: 'full'},
      {path: '', component: HomeComponent},
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
  /*{
    /*th: '', component: MainLayoutComponent, children: [
      {path: '', redirectTo: '/', pathMatch: 'full'},
      {path: '', component: HomePageComponent},
      {path: 'post/:id', component: PostPageComponent}
    ]
*/
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    preloadingStrategy: PreloadAllModules
  })],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
