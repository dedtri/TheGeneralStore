import { AfterViewInit, Component, ElementRef, ViewChild } from '@angular/core';
import { Image } from 'src/app/models/image.model';
import { ImageService } from 'src/app/services/image.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-image-list',
  templateUrl: './image-list.component.html',
  styleUrls: ['./image-list.component.css']
})
export class ImageListComponent {

  queryResult: any = [];
  imageCount: any;
  imageBaseUrl: string = this.imageService.imageUrl;
  query: any = {
    pageSize: 6,
  };

  constructor(private imageService: ImageService) {
    this.initialize();
  }

  //#region Initialize
  initialize() {
    this.imageService.getAll(this.query).subscribe({
      next: (resp) => {
        this.queryResult = resp.entities;
        this.imageCount = resp.count;
      },
      error: (err) => {
        console.log(err);
      }
    })
  }
  //#endregion

  //#region Delete
  delete(id: number) {
    this.imageService.delete(id).subscribe({
      next: (res: any) => {
        console.log(res);
        this.onFilterChange();
      },
      error: (err) => {
        console.log(err);
      }
    });
  }
  //#endregion

  //#region onFilterChange
  onFilterChange() {
    this.query.page = 1;
    this.initialize();
  }
  //#endregion

  //#region onPageChange
  onPageChange(page: any) {
    this.query.page = page;
    this.initialize();
  }
  //#endregion
}
