import { Component, AfterViewInit, inject, PLATFORM_ID, OnInit, signal, computed } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import Chart from 'chart.js/auto';
import { DashboardService, DashboardStats } from '../services/dashboard.service';

type TimePeriod = 'day' | 'week' | 'month' | '6months';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent implements OnInit, AfterViewInit {
  private platformId = inject(PLATFORM_ID);
  private dashboardService = inject(DashboardService);

  stats = signal<DashboardStats | null>(null);
  selectedPeriod = signal<TimePeriod>('6months');

  activityChart: Chart | null = null;
  statusChart: Chart | null = null;

  completedPercentage = computed(() => {
    const data = this.stats();
    if (!data || data.totalOrders === 0) return 0;
    return Math.round((data.completedOrders / data.totalOrders) * 100);
  });

  ngOnInit() {
    this.loadData();
  }

  ngAfterViewInit() {
    // Charts will be initialized in loadData when data arrives
  }

  loadData() {
    this.dashboardService.getStats().subscribe(data => {
      this.stats.set(data);
      if (isPlatformBrowser(this.platformId)) {
        setTimeout(() => {
          this.initActivityChart(data);
          this.initStatusChart(data);
        }, 100);
      }
    });
  }

  setTimePeriod(period: TimePeriod) {
    this.selectedPeriod.set(period);
    const data = this.stats();
    if (data && this.activityChart) {
      this.updateActivityChart(data, period);
    }
  }

  private initActivityChart(data: DashboardStats) {
    const ctx = document.getElementById('activityChart') as HTMLCanvasElement;
    if (!ctx) return;

    if (this.activityChart) {
      this.activityChart.destroy();
    }

    const filteredData = this.filterDataByPeriod(data.activityByDate, this.selectedPeriod());

    this.activityChart = new Chart(ctx, {
      type: 'line',
      data: {
        labels: filteredData.map(d => this.formatDateLabel(d.date)),
        datasets: [{
          label: 'Pedidos',
          data: filteredData.map(d => d.count),
          fill: true,
          backgroundColor: 'rgba(59, 130, 246, 0.1)',
          borderColor: '#3b82f6',
          borderWidth: 2,
          tension: 0.4,
          pointRadius: 4,
          pointBackgroundColor: '#3b82f6',
          pointBorderColor: '#ffffff',
          pointBorderWidth: 2,
          pointHoverRadius: 6
        }]
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
        plugins: {
          legend: {
            display: false
          },
          tooltip: {
            backgroundColor: '#1f2937',
            titleColor: '#ffffff',
            bodyColor: '#ffffff',
            cornerRadius: 8,
            padding: 12
          }
        },
        scales: {
          x: {
            grid: {
              display: false
            },
            ticks: {
              color: '#9ca3af',
              font: {
                size: 11
              }
            }
          },
          y: {
            grid: {
              color: '#f3f4f6'
            },
            ticks: {
              color: '#9ca3af',
              font: {
                size: 11
              }
            },
            beginAtZero: true
          }
        }
      }
    });
  }

  private initStatusChart(data: DashboardStats) {
    const ctx = document.getElementById('statusChart') as HTMLCanvasElement;
    if (!ctx) return;

    if (this.statusChart) {
      this.statusChart.destroy();
    }

    this.statusChart = new Chart(ctx, {
      type: 'doughnut',
      data: {
        labels: ['Completados', 'Pendientes'],
        datasets: [{
          data: [data.completedOrders, data.pendingOrders],
          backgroundColor: ['#3b82f6', '#e5e7eb'],
          borderWidth: 0,
          hoverOffset: 4
        }]
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
        cutout: '75%',
        plugins: {
          legend: {
            display: false
          },
          tooltip: {
            backgroundColor: '#1f2937',
            titleColor: '#ffffff',
            bodyColor: '#ffffff',
            cornerRadius: 8,
            padding: 12
          }
        }
      }
    });
  }

  private updateActivityChart(data: DashboardStats, period: TimePeriod) {
    if (!this.activityChart) return;

    const filteredData = this.filterDataByPeriod(data.activityByDate, period);

    this.activityChart.data.labels = filteredData.map(d => this.formatDateLabel(d.date));
    this.activityChart.data.datasets[0].data = filteredData.map(d => d.count);
    this.activityChart.update();
  }

  private filterDataByPeriod(activityData: { date: string; count: number }[], period: TimePeriod) {
    const now = new Date();
    let startDate: Date;

    switch (period) {
      case 'day':
        startDate = new Date(now.getFullYear(), now.getMonth(), now.getDate() - 1);
        break;
      case 'week':
        startDate = new Date(now.getFullYear(), now.getMonth(), now.getDate() - 7);
        break;
      case 'month':
        startDate = new Date(now.getFullYear(), now.getMonth() - 1, now.getDate());
        break;
      case '6months':
      default:
        startDate = new Date(now.getFullYear(), now.getMonth() - 6, now.getDate());
        break;
    }

    return activityData.filter(item => new Date(item.date) >= startDate);
  }

  private formatDateLabel(dateString: string): string {
    const date = new Date(dateString);
    const months = ['ENE', 'FEB', 'MAR', 'ABR', 'MAY', 'JUN', 'JUL', 'AGO', 'SEP', 'OCT', 'NOV', 'DIC'];
    return `${date.getDate()} ${months[date.getMonth()]}`;
  }
}
