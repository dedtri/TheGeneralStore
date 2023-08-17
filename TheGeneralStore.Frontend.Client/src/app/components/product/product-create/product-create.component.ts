import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-product-create',
  templateUrl: './product-create.component.html',
  styleUrls: ['./product-create.component.css']
})
export class ProductCreateComponent {

createProduct: any = {};
productExist: boolean = false;

isProjectDetails: boolean | any = true;
isImageUpload: boolean | any;

constructor(public dialogRef: MatDialogRef<ProductCreateComponent>) {}

cancel() {
    this.dialogRef.close(); 
}

}
