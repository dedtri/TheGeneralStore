import { Component, OnDestroy } from '@angular/core';
import { Subscription, interval } from 'rxjs';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnDestroy {

  categories: any = {};
  updateSubscription: Subscription | any;
  serverError: any = false;

  constructor(private categoryService: CategoryService) {

    this.initialize();

    this.updateSubscription = interval(2500).subscribe(() => {
      this.initialize();
    });

  }

  ngOnDestroy() {
    this.updateSubscription.unsubscribe();
  }

  initialize() {
    this.categoryService.getAll().subscribe({
      next: (res) => {
        this.categories = res;
        this.serverError = false;
      },
      error: (err) => {
        console.log(err)
        this.serverError = true;
        this.categories = {};
      }
    })
  }
}
