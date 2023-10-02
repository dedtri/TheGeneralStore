import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { Product } from 'src/app/models/product.model';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-product-create-details',
  templateUrl: './product-create-details.component.html',
  styleUrls: ['./product-create-details.component.css']
})
export class ProductCreateDetailsComponent {

  @Input() createProduct!: any;
  @Output() createProductChange = new EventEmitter<any>();
  categories: any = {};

    // Create form controls with validators
    nameControl = new FormControl('', Validators.required);
    categoryIdControl = new FormControl('', Validators.required);
    descriptionControl = new FormControl('', Validators.required);
    priceControl = new FormControl('', Validators.required);

  constructor(private categoryService: CategoryService) {
    this.initialize();
  }

  onFormControlChange() {
    if (
      this.nameControl.valid &&
      this.categoryIdControl.valid &&
      this.descriptionControl.valid &&
      this.priceControl.valid
    ) {
      this.createProduct.name = this.nameControl.value;
      this.createProduct.categoryId = this.categoryIdControl.value;
      this.createProduct.description = this.descriptionControl.value;
      this.createProduct.price = this.priceControl.value;
      
      this.createProductChange.emit(this.createProduct);
    }
  }
  
  initialize() {
    this.categoryService.getAll().subscribe({
      next: (res) => {
        this.categories = res;
        console.log(this.categories);
      },
      error: (err) => {
        console.log(err)
      }
    })
  }

}
