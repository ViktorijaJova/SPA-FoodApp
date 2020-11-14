import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'mapMunicipality'
})
export class MapMunicipalityPipe implements PipeTransform {
  transform(value: any) {
    switch(value) {
      case 1: 
        return "Karposh"
        break;
      case 2: 
        return "Centar"
        break;
      case 3:
        return "Aerodrom"
        break;
    }
  }
}
