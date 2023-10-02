import { Location } from '@angular/common';
import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-myorder',
  templateUrl: './myorder.component.html',
  styleUrls: ['./myorder.component.css']
})
export class MyorderComponent {

  queryResult: any = [];
  query: any = {
    pageSize: 5,
    customerId: 0,
  };

  constructor(private orderService: OrderService, private route: ActivatedRoute, private location: Location) {

    this.initialize();

  }

  initialize() {

    this.route.paramMap.subscribe({
      next: (params) => {
        const id = params.get('id');
        this.query.customerId = id;
      }
    })

    this.orderService.getAll(this.query).subscribe({
      next: (resp) => {
        this.queryResult = resp.entities.filter((order: any) => order.orderProducts.length > 0);
        console.log(this.queryResult);
      },
      error: (err) => {
        console.log(err);
      }
    })
  }

  goBack() {
    this.location.back();
  }
}
