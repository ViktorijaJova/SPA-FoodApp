import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'mapMealType'
})
export class MapMealTypePipe implements PipeTransform {
  transform(value: any) {
    switch (value) {
        case 1:
          return "Starters";
          break;
        case 2:
          return "Salads";
          break;
        case 3:
          return "MainDish"
          break;
        case 4:
          return "Deserts"
          break;
        case 5:
          return "Drinks"
          break;
    }
  }
}