import {NgModule} from '@angular/core';
import {QuillModule} from 'ngx-quill';
import {QuillConfig} from 'ngx-quill/src/quill-editor.interfaces';

const config: QuillConfig = {
};

@NgModule({
  imports: [
    QuillModule.forRoot(config),
  ],
  exports: [
    QuillModule,
  ]
})
export class AppEditorModule {

}
