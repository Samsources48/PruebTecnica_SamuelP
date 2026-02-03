import { Component, inject } from '@angular/core';
import { OrdersService } from '../../../core/services/orders.service';
import { Order } from '../../../shared/models/order.models';
import { OrderFormComponent } from '../order-form/order-form.component';
import { MatDialog } from '@angular/material/dialog';
import { Observable, BehaviorSubject, combineLatest, map, switchMap } from 'rxjs';

@Component({
  selector: 'app-order-list',
  templateUrl: './order-list.component.html',
  styleUrl: './order-list.component.scss'
})
export class OrderListComponent {
  private ordersService = inject(OrdersService);
  private dialog = inject(MatDialog);
  
  displayedColumns: string[] = ['id', 'client', 'date', 'status', 'total', 'actions'];
  
  private statusFilter$ = new BehaviorSubject<string>('');
  private loadTrigger$ = new BehaviorSubject<void>(undefined);

  ordersFiltered$: Observable<Order[]>;

  constructor() {
    const orders$ = this.loadTrigger$.pipe(
      switchMap(() => this.ordersService.getOrders())
    );

    this.ordersFiltered$ = combineLatest([
      orders$,
      this.statusFilter$
    ]).pipe(
      map(([orders, status]) => {
        if (!status) return orders;
        return orders.filter(o => o.estado === status);
      })
    );
  }

  openDialog(order?: Order) {
    const dialogRef = this.dialog.open(OrderFormComponent, {
      width: '700px',
      data: order || null
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.reload();
      }
    });
  }

  reload() {
    this.loadTrigger$.next();
  }

  onStatusChange(status: string) {
    this.statusFilter$.next(status);
  }

  getStatusColor(status: string) {
    switch (status) {
      case 'COMPLETED': return '#4caf50';
      case 'PENDING': return '#ff9800';
      case 'CANCELLED': return '#f44336';
      default: return '#9e9e9e';
    }
  }

  updateStatus(order: Order, status: string) {
    const updatedOrder = { ...order, estado: status };
    this.ordersService.updateOrder(order.idPedido, updatedOrder).subscribe({
        next: () => this.reload(),
        error: (err) => console.error(err)
    });
  }

  deleteOrder(id: number) {
    if (confirm('¿Eliminar pedido?')) {
      this.ordersService.deleteOrder(id).subscribe({
          next: () => this.reload(),
          error: (err) => console.error(err)
      });
    }
  }
}
