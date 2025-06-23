import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'GHBA';
  isLoading = true;

  ngOnInit(): void {
    // Show loader briefly on app start
    setTimeout(() => {
      this.isLoading = false;
    }, 1000); // Smooth out feel without forcing delays
  }
}
