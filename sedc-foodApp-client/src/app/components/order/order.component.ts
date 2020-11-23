import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/auth/auth.service';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {

  order: any;
  price: any

  constructor(private orderService: OrderService,
              private authService: AuthService) { }

  ngOnInit(): void {
    this.getUserOrder()
  }

  getUserOrder() {
    let userId = this.authService.getUserId()

    this.orderService.getUserOrder(userId).subscribe({
      next: res => this.order = res,
      error: err => console.warn(err.error),
      complete: () => {
        this.calculatePrice()
      }
    })
  }

  calculatePrice() {
    let price = 0;
    this.order.menuItems.forEach(item => {
      price += parseInt(item.price)
    });
    this.price = price
  }

}
