import { Component, Inject, Input, OnChanges, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { Image } from 'src/app/models/image.model';
import { Product } from 'src/app/models/product.model';
import { ImageService } from 'src/app/services/image.service';

@Component({
  selector: 'app-image-upload',
  templateUrl: './image-upload.component.html',
  styleUrls: ['./image-upload.component.css']
})
export class ImageUploadComponent implements OnChanges {

  selectedFile = null;
  frm: FormGroup | any;
  imageFile: File | any;
  statusString: string = "";
  // previewImage = "";
  imageBaseUrl: string = this.imageService.imageUrl;

  query: any = {
    ProductId: 0,
    imageType: 0
  };
  queryResult: any;
  images: any = []; 

  @Input() productId: any;

  constructor(private formBuilder: FormBuilder, private imageService: ImageService, public dialog: MatDialog, 
    @Inject(MAT_DIALOG_DATA) public data: Product) {
        this.setProjectId(this.data);
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['productId']) {
      this.setProjectId(this.productId);
    }
  }

  setProjectId(productId: any) {
    this.frm = this.formBuilder.group({
      'imageFile': [, Validators.required],
      'ProductId': productId,
    })
    this.query.ProductId = productId;
    this.initialize();
  }

  initialize() {
    this.imageService.getAll(this.query).subscribe({
      next: (resp) => {
        this.queryResult = resp;
        // this.images = this.queryResult.entities
        // console.log(this.images);
      },
      error: (err) => {
        console.log(err);
      }
    })
  }

  //#region onFileSelected
  onFileSelected(event: any) {
    if (event.target.files[0].type.startsWith("image/")) {
      var reader = new FileReader
      reader.readAsDataURL(event.target.files[0]);
      reader.onload = (event: any) => {
        // this.previewImage = event.target.result;
      }
      this.imageFile = event.target.files[0]
    }
    else {
      // this.previewImage = "";
      this.imageFile = undefined;
    }
  }
  //#endregion

  //#region onSubmit
  onSubmit() {

    this.statusString = "Wait...";

    if (this.imageFile == null)
      return this.statusString = "No image found to upload!";

    if (this.imageFile.type.startsWith("image/")) {
      const frmData: Image = Object.assign(this.frm.value);
      frmData.imageFile = this.imageFile;

      this.images.push(this.imageFile);

      console.log(this.images);

      this.imageService.add(frmData).subscribe({
        next: (res) => {
          this.images = res;
          this.initialize();
        },
        error: (err) => {
          this.statusString = "Error on server side..";
          console.log(err);
        }
      })
    }
    else {
      this.statusString = "Unsupported File Format";
    }
    return;
  }
  //#endregion
}
