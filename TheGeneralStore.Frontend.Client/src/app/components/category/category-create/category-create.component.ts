import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-category-create',
  templateUrl: './category-create.component.html',
  styleUrls: ['./category-create.component.css']
})
export class CategoryCreateComponent {

  createCategory: any = {};
  
  constructor(private categoryService: CategoryService, public dialogRef: MatDialogRef<CategoryCreateComponent>) {
    
  }

  create() {
    this.categoryService.add(this.createCategory).subscribe({
      next: (resp: any) => {
        console.log(resp);
        this.createCategory = resp;
        this.dialogRef.close(); 
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

}
