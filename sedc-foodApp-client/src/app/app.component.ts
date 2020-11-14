import { Component } from '@angular/core';
import { AliveService } from './services/alive.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  constructor(private aliveService: AliveService) {}

  pingIsAlive() {
    this.aliveService.ping().subscribe({
      next: res => console.log(res.message),
      error: err => console.warn(err.error),
      complete: () => {
        console.log("complete")
      }
    })
  }

}
