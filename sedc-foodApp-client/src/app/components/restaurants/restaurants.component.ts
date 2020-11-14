import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { cwd } from 'process';
import { RestaurantService } from 'src/app/services/restaurant.service';
import { environment } from 'src/environments/environment'

@Component({
  selector: 'app-restaurants',
  templateUrl: './restaurants.component.html',
  styleUrls: ['./restaurants.component.css']
})
export class RestaurantsComponent implements OnInit {

  restaurants: any

  constructor(private restaurantService: RestaurantService) { }

  ngOnInit(): void {
    this.getAllRestaurants()
  }
 
  getAllRestaurants() {
    this.restaurantService.getRestaurants().subscribe({
      next: data => this.restaurants = data,
      error: err => console.warn(err.error)
    })
  }

}
