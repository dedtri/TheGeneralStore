import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryService } from 'src/app/services/category.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-category-product-list',
  templateUrl: './category-product-list.component.html',
  styleUrls: ['./category-product-list.component.css']
})
export class CategoryProductListComponent {

category: any;
categories: any;
products: any[] = [];
imageUrl = environment.imageURL;

constructor(private router: Router, private route: ActivatedRoute, private categoryService: CategoryService) {

this.initialize();
this.initializeCategoryNames();

}

initializeCategoryNames() {
  this.categoryService.getAll().subscribe({
    next: (res) => {
      this.categories = res;
    },
    error: (err) => {
      console.log(err)
    }
  })
}

initialize() {
  this.route.paramMap.subscribe({
    next: (params) => {
      const name = params.get('categoryName');
      if (name) {
        this.categoryService.getByName(name).subscribe({
          next: (resp) => {
            this.category = resp;
            this.products = this.category.products.filter((product: any) => product.images.length > 0);
          },
          error: (err) => {
            console.log(err);
          }
        });
      }
    }
  })
}

goProductDetails(projectId: any) {
  this.router.navigateByUrl('categories/' + this.category.name + '/' +  projectId);
}

}
