import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {
  role: string = '';
  masterExpanded: boolean = false; // 🔥 for dropdown toggle

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.role = this.authService.getUserRole();
  }

  toggleMaster(): void {
    this.masterExpanded = !this.masterExpanded;
  }

  logout(): void {
    this.authService.logout();
  }
}
