import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { HttpEventType, HttpClient } from '@angular/common/http';
import { RepositoryService } from '../shared/services/repository.service';
import { Guid } from "guid-typescript";

import {
  HttpRequest,
  HttpResponse,
} from '@angular/common/http';
import { finalize } from 'rxjs/operators';
import { ImageModel } from '../_interfaces/imageModel';
import { image } from '@rxweb/reactive-form-validators';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.css']
})
export class UploadComponent implements OnInit {
  uploadProgress = 0;
  selectedFiles: File[];
  uploading = false;
  errorMsg = '';
  imagesForNewGood: ImageModel[] = [];
  submissionResults: ImageModel[] = [];
  public imageName: Guid;
  @Output() public onUploadFinished = new EventEmitter();
  @Output() public deleteFinished = new EventEmitter();

  constructor(
    private repository: RepositoryService,
    private http: HttpClient) { }

  ngOnInit() {
  }

  chooseFile(files: FileList) {
    this.selectedFiles = [];
    this.errorMsg = '';
    this.uploadProgress = 0;
    if (files.length === 0) {
      return;
    }
    for (let i = 0; i < files.length; i++) {
      var file = files[i];
      var blob = file.slice(0, file.size, 'image/png/jpg'); 
      var extension = files[i].name.split('.').pop();
      var newName = (Guid.create()).toString() +"."+ extension;
      const newFile = new File([blob], newName, {type: 'image/png/jpg'});
      this.selectedFiles.push(newFile);
    }
  }

  public deleteImg=(imagePath)=>{
    // var address ="api/upload/delete?imagePath=" + imagePath;
    // this.repository.delete(address).subscribe(
    //   () => {
    var removeIndex = this.imagesForNewGood.map(item => item.imagePath).indexOf(imagePath);
    ~removeIndex && this.imagesForNewGood.splice(removeIndex, 1);
    this.deleteFinished.emit(imagePath);
      // },
      // (error) => {
      //   // Here, you can either customize the way you want to catch the errors
      //   this.errorMsg =  error; // or rethrow the error if you have a global error handler
      // });
  }

  public createImgPath = (serverPath: string) => {
    return this.repository.createImgPath(serverPath);
  }
  
  upload() {
    if (!this.selectedFiles || this.selectedFiles.length === 0) {
      this.errorMsg = 'Please choose a file.';
      return;
    }

    const formData = new FormData();
    this.selectedFiles.forEach((f) => formData.append('images', f));

    const req = new HttpRequest(
      'POST',
      `api/upload`,
      formData,
      {
        reportProgress: true,
      }
    );
    this.uploading = true;
    this.http
      .request<ImageModel[]>(req)
      .pipe(
        finalize(() => {
          this.uploading = false;
          this.selectedFiles = null;
        })
      )
      .subscribe(
        (event) => {
          if (event.type === HttpEventType.UploadProgress) {
            this.uploadProgress = Math.round(
              (100 * event.loaded) / event.total
            );
          } else if (event instanceof HttpResponse) {
            this.submissionResults = event.body as ImageModel[];
            var imagesForNewGood = Array();
            imagesForNewGood = event.body as ImageModel[];
            imagesForNewGood.forEach(element=>{
              this.imagesForNewGood.push(element); 
            })
            this.onUploadFinished.emit(event.body);
          }
        },
        (error) => {
          // Here, you can either customize the way you want to catch the errors
          throw error; // or rethrow the error if you have a global error handler
        }
      );
  }

  humanFileSize(bytes: number): string {
    if (Math.abs(bytes) < 1024) {
      return bytes + ' B';
    }
    const units = ['kB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'];
    let u = -1;
    do {
      bytes /= 1024;
      u++;
    } while (Math.abs(bytes) >= 1024 && u < units.length - 1);
    return bytes.toFixed(1) + ' ' + units[u];
  }
}