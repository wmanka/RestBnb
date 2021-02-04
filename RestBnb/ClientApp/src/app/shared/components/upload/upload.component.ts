import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.css'],
})
export class UploadComponent {
  @Output() public onUploadFinished = new EventEmitter();

  public progress: number;
  public message: string;

  public uploadFile = (files) => {
    if (files.length === 0) {
      return;
    }
    let filesToUpload: File[] = files;
    const formData = new FormData();

    Array.from(filesToUpload).map((file, index) => {
      return formData.append('file' + index, file, file.name);
    });

    this.onUploadFinished.emit(formData);

    // this.http
    //   .post(ApiRoutes.PropertyImages.Create.replace(), formData, {
    //     reportProgress: true,
    //     observe: 'events',
    //   })
    //   .subscribe((event) => {
    //     if (event.type === HttpEventType.UploadProgress)
    //       this.progress = Math.round((100 * event.loaded) / event.total);
    //     else if (event.type === HttpEventType.Response) {
    //       this.message = 'Upload success.';
    //       this.onUploadFinished.emit(event.body);
    //     }
    //   });
  };
}
