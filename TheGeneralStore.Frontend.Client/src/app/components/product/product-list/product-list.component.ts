import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ProductCreateComponent } from '../product-create/product-create.component';
import { ProductService } from 'src/app/services/product.service';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent {

  products: any = [];
  productCount: any;
  imageUrl = environment.imageURL;
  query: any = {
    pageSize: 5,
  };

  constructor(public dialog: MatDialog, private productService: ProductService, private router: Router) {

    this.initialize();

  }
  
  initialize() {
    this.productService.getAll(this.query).subscribe({
      next: (res) => {
        this.products = res.entities.filter((product: any) => product.images.length > 0 && product.category != null);
        this.productCount = res.count;
      },
      error: (err) => {
        console.log(err)
      }
    })
  }

  openCreateDialog() {
    const dialogRef = this.dialog.open(ProductCreateComponent, {
      disableClose: true,
      width: '50%',
      height: '90%'
    });
  
    dialogRef.afterClosed().subscribe(e => {
      this.initialize();
    })
  }

  delete(product: any, event: Event) {

    event.stopPropagation();

    product.isDeleted = 1;
  
    this.productService.update(product.id, product).subscribe({
      next: (res) => {
       console.log(res);
       this.initialize();     
      },
      error: (err) => {
        console.log(err);
      }
    })
  }

  goProductDetails(projectId: any) {
    this.router.navigateByUrl('products/' + projectId)
 }

 onPageChange(page: any) {
  this.query.page = page;
  this.initialize();
}
}
