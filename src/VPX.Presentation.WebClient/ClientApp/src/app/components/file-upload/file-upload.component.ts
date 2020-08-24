import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {FileUploader} from 'ng2-file-upload';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css']
})
export class FileUploadComponent implements OnInit {

  public uploader: FileUploader = new FileUploader({ url: environment.apiEndpoint + 'images', itemAlias: 'file' });

  @Output()
  uploaded = new EventEmitter<string>();

  ngOnInit() {
    this.uploader.onAfterAddingFile = (file) => { file.withCredentials = false; };
    this.uploader.onCompleteItem = (item: any, response: any) => {
      this.uploaded.emit(JSON.parse(response).image);
    };
  }
}
