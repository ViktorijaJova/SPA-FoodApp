import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { RestaurantService } from 'src/app/services/restaurant.service';

@Component({
  selector: 'app-restaurant-menu',
  templateUrl: './restaurant-menu.component.html',
  styleUrls: ['./restaurant-menu.component.css']
})
export class RestaurantMenuComponent implements OnInit {

  restaurantId: string
  restaurantMenu: any

  starters: any
  salads: any
  mainDish: any
  deserts: any
  drinks: any

  constructor(private activatedRoute: ActivatedRoute,
              private restaurantService: RestaurantService) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((params: any) => {
      this.restaurantId = params.id
    })

    this.getMenu()
  }

  getMenu() {
    this.restaurantService.getRestaurantMenu(this.restaurantId).subscribe({
      next: data => this.restaurantMenu = data,
      error: err => console.warn(err.error),
      complete: () => {
        this.splitMenu(this.restaurantMenu)
      }
    })
  }

  splitMenu(menu: any) {
    this.starters = menu.filter(menuItem => menuItem.mealType === 1)
    this.salads = menu.filter(menuItem => menuItem.mealType === 2)
    this.mainDish = menu.filter(menuItem => menuItem.mealType === 3)
    this.deserts = menu.filter(menuItem => menuItem.mealType === 4)
    this.drinks = menu.filter(menuItem => menuItem.mealType === 5)
  }

}
