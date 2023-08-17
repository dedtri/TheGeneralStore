import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ProductCreateComponent } from '../product-create/product-create.component';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent {

  constructor(public dialog: MatDialog) {}

  openCreateDialog() {
    const dialogRef = this.dialog.open(ProductCreateComponent, {
      disableClose: true,
      width: '100%',
      height: '90%'
    });
}
}
