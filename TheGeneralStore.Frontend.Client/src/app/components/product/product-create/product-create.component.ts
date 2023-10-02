import { Component, ElementRef, ViewChild } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { ProductCreateDetailsComponent } from './product-create-details/product-create-details.component';
import { ProductService } from 'src/app/services/product.service';
import { Product } from 'src/app/models/product.model';

@Component({
  selector: 'app-product-create',
  templateUrl: './product-create.component.html',
  styleUrls: ['./product-create.component.css']
})
export class ProductCreateComponent {

@ViewChild(ProductCreateDetailsComponent) pdChild: any;

createProduct: any = {};
productExist: boolean = false;

isProjectDetails: boolean | any = true;
isImageUpload: boolean | any;

constructor(public dialogRef: MatDialogRef<ProductCreateComponent>, private el: ElementRef, private productService: ProductService ) {

}

onChildProductChange(updatedProduct: any) {
  this.createProduct = updatedProduct;
}

save() {
  this.createProduct = this.pdChild.createProduct;

  if (this.productExist === false)
  {
  this.productService.add(this.createProduct).subscribe({
    next: (resp: any) => {
      console.log(resp);
      this.createProduct = resp;
      this.productExist = true;
    },
    error: (err) => {
      console.log(err);
    }
  });
  }
  else
  {
    this.productService.get(this.createProduct.id).subscribe({
      next: (res) => {
        this.createProduct = res;
    
        if (this.createProduct.images.length > 0) {
          this.productService.update(this.createProduct.id, this.createProduct).subscribe({
            next: (res) => {
              console.log("Success");
            },
            error: (err) => {
              console.log(err);
            }
          });
          this.dialogRef.close();
        } else
        {
          console.log("Product missing image!");
        }
      },
      error: (err) => {
        console.log(err);
      }
    });
}
}

cancel() {
  if (this.productExist === false)
  this.dialogRef.close(); 
else
{
  this.createProduct.isDeleted = 1;
  
  this.productService.update(this.createProduct.id, this.createProduct).subscribe({
    next: (res) => {
     this.dialogRef.close();            
    },
    error: (err) => {
      console.log(err);
    }
  })
}
}

navigate(name: string) {
  if(this.productExist === true)
  {
  const pnLink = this.el.nativeElement.querySelector('#pdLink');
  const udLink = this.el.nativeElement.querySelector('#udLink');

  pnLink.classList.remove('activeComponent');
  udLink.classList.remove('activeComponent');
  this.el.nativeElement.querySelector(`#${name}Link`).classList.add('activeComponent');

  this.isProjectDetails = name === 'pd';
  this.isImageUpload = name === 'ud';
}
}


}
