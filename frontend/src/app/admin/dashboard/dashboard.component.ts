import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { DashboardService, DashboardStats, BedStats } from '../../services/dashboard.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  currentDate = new Date().toLocaleDateString('en-US', {
    year: 'numeric',
    month: 'short',
    day: 'numeric'
  });

  stats = [
    { label: 'Available Rooms', value: 0, icon: 'home' },
    { label: 'Reservations', value: 0, icon: 'event' },
    { label: 'Pending Requests', value: 0, icon: 'pending_actions' },
    { label: 'Check-ins', value: 0, icon: 'login' },
    { label: 'Check-outs', value: 0, icon: 'logout' }
  ];

  bedStatsForm: FormGroup;
  bedStats: BedStats = {
    availableBeds: 0,
    occupiedBeds: 0,
    checkIns: 0,
    totalNights: 0
  };
  bedStatsDisplayLabel: string = ''; // for range label near title

  constructor(private dashboardService: DashboardService, private fb: FormBuilder) {
    this.bedStatsForm = this.fb.group({
      range: this.fb.group({
        start: [null],
        end: [null]
      })
    });
  }

  ngOnInit(): void {
    this.loadDashboardStats();
    this.setTodayAsDefaultAndFetch();
    this.watchDateChanges();
  }

  get bedStatsRangeGroup(): FormGroup {
    return this.bedStatsForm.get('range') as FormGroup;
  }

  loadDashboardStats(): void {
    this.dashboardService.getDashboardStats().subscribe({
      next: (response) => {
        const data: DashboardStats = response.data;
        this.stats[0].value = data.availableRooms;
        this.stats[1].value = data.reservations;
        this.stats[2].value = data.pendingRequests;
        this.stats[3].value = data.checkIns;
        this.stats[4].value = data.checkOuts;
      },
      error: (err) => {
        console.error('Failed to fetch dashboard stats:', err);
      }
    });
  }

  fetchBedStats(): void {
    const range = this.bedStatsRangeGroup.value;
    const start = range.start;
    const end = range.end;

    if (!start || !end) return;

    const formattedStart = start.toISOString().split('T')[0];
    const formattedEnd = end.toISOString().split('T')[0];

    this.dashboardService.getBedStats(formattedStart, formattedEnd).subscribe({
      next: (response) => {
        this.bedStats = response.data;
        this.bedStatsDisplayLabel = `${start.toLocaleDateString('en-US')} to ${end.toLocaleDateString('en-US')}`;
      },
      error: (err) => {
        console.error('Failed to fetch bed stats:', err);
      }
    });
  }

  watchDateChanges(): void {
    this.bedStatsRangeGroup.valueChanges.subscribe(val => {
      if (val.start && val.end) {
        this.fetchBedStats();
      }
    });
  }

  setTodayAsDefaultAndFetch(): void {
    const today = new Date();
    const startDate = new Date('2000-01-01'); // any safely early date
    const formattedStart = startDate.toISOString().split('T')[0];
    const formattedEnd = today.toISOString().split('T')[0];

    // Clear inputs
    this.bedStatsRangeGroup.patchValue({ start: null, end: null }, { emitEvent: false });
    this.bedStatsDisplayLabel = ''; // Show "Today (currentDate)" in UI

    this.dashboardService.getBedStats(formattedStart, formattedEnd).subscribe({
      next: (response) => {
        this.bedStats = response.data;
      },
      error: (err) => {
        console.error('Failed to fetch default bed stats:', err);
      }
    });
  }

  resetDateRange(): void {
    this.setTodayAsDefaultAndFetch();
  }

  toggleSidebar(event: any): void {
    const sidenav = document.querySelector('mat-sidenav') as any;
    if (sidenav?.toggle) {
      sidenav.toggle();
    }
  }
}
