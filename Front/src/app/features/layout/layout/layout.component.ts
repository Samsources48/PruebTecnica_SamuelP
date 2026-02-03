import { Component, inject, OnInit } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay, filter } from 'rxjs/operators';
import { AuthStore } from '../../../core/auth/auth.store';
import { Router, NavigationEnd } from '@angular/router';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.scss'
})
export class LayoutComponent implements OnInit {
  private breakpointObserver = inject(BreakpointObserver);
  private authStore = inject(AuthStore);
  private router = inject(Router);

  user = this.authStore.user;
  pageTitle = 'Dashboard Overview';

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );

  ngOnInit() {
    this.updatePageTitle(this.router.url);
    this.router.events.pipe(
      filter((event): event is NavigationEnd => event instanceof NavigationEnd)
    ).subscribe((event) => {
      this.updatePageTitle(event.urlAfterRedirects);
    });
  }

  private updatePageTitle(url: string) {
    if (url.includes('/dashboard')) {
      this.pageTitle = 'Dashboard';
    } else if (url.includes('/clients')) {
      this.pageTitle = 'Gestión de Clientes';
    } else if (url.includes('/products')) {
      this.pageTitle = 'Catálogo de Productos';
    } else if (url.includes('/orders')) {
      this.pageTitle = 'Gestión de Pedidos';
    } else {
      this.pageTitle = 'Sistema de Gestión';
    }
  }

  logout() {
    this.authStore.logout();
    this.router.navigate(['/auth/login']);
  }
}
