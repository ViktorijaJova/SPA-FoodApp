import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'isVegePipe'
})
export class IsVegePipe implements PipeTransform {
  transform(value: any) {
    return value ? "Yes" : "No"
  }
}