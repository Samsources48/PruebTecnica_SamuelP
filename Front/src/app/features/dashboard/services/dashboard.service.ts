import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { forkJoin, map, Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { Client } from '../../../shared/models/client.models';
import { Order } from '../../../shared/models/order.models';

export interface DashboardStats {
  totalOrders: number;
  completedOrders: number;
  pendingOrders: number;
  activeClients: number;
  activityByDate: { date: string; count: number }[];
}

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  private http = inject(HttpClient);
  private apiUrl = environment.apiUrl;

  getStats(): Observable<DashboardStats> {
    return forkJoin({
      clients: this.http.get<Client[]>(`${this.apiUrl}/Clientes`),
      orders: this.http.get<Order[]>(`${this.apiUrl}/Pedidos`)
    }).pipe(
      map(({ clients, orders }) => {
        const completedOrders = orders.filter(o => o.estado === 'Completado').length;
        const pendingOrders = orders.filter(o => o.estado === 'Pendiente').length; // Assuming 'Pendiente' is the status
        const activeClients = clients.filter(c => c.activo).length;

        // Group orders by date (YYYY-MM-DD)
        const activityMap = new Map<string, number>();
        orders.forEach(o => {
          const date = new Date(o.fechaPedido).toISOString().split('T')[0];
          activityMap.set(date, (activityMap.get(date) || 0) + 1);
        });

        const activityByDate = Array.from(activityMap.entries())
          .map(([date, count]) => ({ date, count }))
          .sort((a, b) => new Date(a.date).getTime() - new Date(b.date).getTime());

        return {
          totalOrders: orders.length,
          completedOrders,
          pendingOrders,
          activeClients,
          activityByDate
        };
      })
    );
  }
}
