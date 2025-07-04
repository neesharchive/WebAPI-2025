import { Component } from '@angular/core';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent {
  sidebarOpened = true;

  // globally accessible static toggle
  static toggleSidebar: () => void;
  ngOnInit() {
  MainComponent.toggleSidebar = () => {
    this.sidebarOpened = !this.sidebarOpened;
  };
}

}