import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { MainComponent } from 'src/app/core/main/main.component'; // if toggleSidebar() is static

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  username: string = '';

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
  const role = localStorage.getItem('userRole');
  if (role === 'Admin') {
    this.username = 'AdminNishant';
  } else {
    this.username = 'UserNishant'; // or any default name
  }
}


  toggleSidebar() {
    MainComponent.toggleSidebar();
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
