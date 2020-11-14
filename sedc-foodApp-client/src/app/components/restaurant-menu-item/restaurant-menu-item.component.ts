import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-restaurant-menu-item',
  templateUrl: './restaurant-menu-item.component.html',
  styleUrls: ['./restaurant-menu-item.component.css']
})
export class RestaurantMenuItemComponent {

  @Input() menuItems: any
  @Input() mealTypeName: any

  constructor() { }
}
