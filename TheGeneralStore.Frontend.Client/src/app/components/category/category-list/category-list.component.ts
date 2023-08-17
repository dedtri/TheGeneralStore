import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { CategoryService } from 'src/app/services/category.service';
import { CategoryCreateComponent } from '../category-create/category-create.component';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css']
})
export class CategoryListComponent {

  categories: any = {};

  constructor(public dialog: MatDialog, private router: Router, private categoryService: CategoryService) {

    this.initialize();

  }

  initialize() {
    this.categoryService.getAll().subscribe({
      next: (res) => {
        this.categories = res;
      },
      error: (err) => {
        console.log(err)
      }
    })
  }

  openCreateDialog() {
    const dialogRef = this.dialog.open(CategoryCreateComponent, {
      width: '50%',
      height: '30%'
    });

    dialogRef.afterClosed().subscribe(e => {
      this.initialize();
    })
  }

  goCategoryProductList(categoryId: any) {
    this.router.navigateByUrl('categoryName/' + categoryId)
  }

  delete(category: any, event: Event) {

    event.stopPropagation();

    this.categoryService.delete(category.id).subscribe({
      next: (res) => {
        console.log(res);
        this.initialize();
      },
      error: (err) => {
        console.log(err);
      }
    })
  }

}
