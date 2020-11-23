import { Component, Input, OnInit } from '@angular/core';
import { AuthService } from 'src/app/auth/auth.service';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-restaurant-menu-item',
  templateUrl: './restaurant-menu-item.component.html',
  styleUrls: ['./restaurant-menu-item.component.css']
})
export class RestaurantMenuItemComponent {

  @Input() menuItems: any
  @Input() mealTypeName: any

  constructor(private orderService: OrderService,
              private authService: AuthService) { }

  updateOrder(menuItem) {
    let userId = this.authService.getUserId()

    let request = {
      UserId: userId,
      MenuItem: menuItem
    }

    this.orderService.updateOrder(request).subscribe({
      error: err => console.warn(err.error)
    })
  }
}
