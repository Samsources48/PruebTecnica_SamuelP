import { Component, inject, Inject, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { OrdersService } from '../../../core/services/orders.service';
import { ClientsService } from '../../../core/services/clients.service';
import { ProductsService } from '../../../core/services/products.service';
import { Order, OrderDetail } from '../../../shared/models/order.models';
import { Client } from '../../../shared/models/client.models';
import { Product } from '../../../shared/models/product.models';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-order-form',
  templateUrl: './order-form.component.html',
  styleUrl: './order-form.component.scss'
})
export class OrderFormComponent implements OnInit {
  private fb = inject(FormBuilder);
  private ordersService = inject(OrdersService);
  private clientsService = inject(ClientsService);
  private productsService = inject(ProductsService);
  private dialogRef = inject(MatDialogRef<OrderFormComponent>);

  form: FormGroup;
  isEditMode: boolean = false;

  clients$: Observable<Client[]> = this.clientsService.getClients();
  products$: Observable<Product[]> = this.productsService.getProducts();
  products: Product[] = []; // Local copy for price lookup

  constructor(@Inject(MAT_DIALOG_DATA) public data: Order | null) {
    this.isEditMode = !!data;
    this.form = this.fb.group({
      idCliente: [data?.idCliente || '', [Validators.required]],
      fechaPedido: [data?.fechaPedido || new Date().toISOString(), [Validators.required]],
      descripcion: [data?.descripcion || ''],
      estado: [data?.estado || 'PENDING', [Validators.required]],
      detalles: this.fb.array([])
    });

    if (data?.detalles) {
      data.detalles.forEach(d => this.addDetail(d));
    } else if (!this.isEditMode) {
        this.addDetail(); // Add one empty row by default
    }
  }

  ngOnInit() {
    this.products$.subscribe(products => this.products = products);
  }

  get detalles() {
    return this.form.get('detalles') as FormArray;
  }

  addDetail(detail?: OrderDetail) {
    const detailGroup = this.fb.group({
      idProducto: [detail?.idProducto || '', Validators.required],
      cantidad: [detail?.cantidad || 1, [Validators.required, Validators.min(1)]],
      precioUnitario: [{ value: detail?.precioUnitario || 0, disabled: true }], // Display only
      subtotal: [{ value: detail?.subtotal || 0, disabled: true }] // Display only
    });

    // Update price/subtotal when product or quantity changes
    detailGroup.valueChanges.subscribe(() => this.updateSubtotal(detailGroup));
    
    // Initial update if adding existing detail
    if (detail) {
        // We might not have products loaded yet, so this might need to wait or rely on form update
    }

    this.detalles.push(detailGroup);
  }

  removeDetail(index: number) {
    this.detalles.removeAt(index);
  }

  updateSubtotal(group: any) { // Type as any for quick access to controls
    const productId = group.get('idProducto')?.value;
    const quantity = group.get('cantidad')?.value;
    const product = this.products.find(p => p.idProducto === productId);

    if (product && quantity) {
      const price = product.precio;
      const subtotal = price * quantity;
      
      group.patchValue({
        precioUnitario: price,
        subtotal: subtotal
      }, { emitEvent: false });
    }
  }

  get total(): number {
    return this.detalles.controls.reduce((acc, control) => {
      return acc + (control.get('subtotal')?.value || 0);
    }, 0);
  }

  save() {
    if (this.form.invalid) return;

    // Prepare payload
    const formValue = this.form.value;
    const orderData: Order = {
      ...formValue,
      idPedido: this.data?.idPedido || 0,
      total: this.total,
      // Map details to include calculated values that are disabled in form
      detalles: this.detalles.controls.map(control => {
          const val = control.getRawValue();
          return {
              idProducto: val.idProducto,
              cantidad: val.cantidad,
              precioUnitario: val.precioUnitario,
              subtotal: val.subtotal,
              idDetallePedido: 0 // Backend usually handles this for new items
          };
      })
    };

    if (this.isEditMode && this.data) {
      this.ordersService.updateOrder(this.data.idPedido, orderData).subscribe({
        next: (res) => this.dialogRef.close(res),
        error: (err) => console.error(err)
      });
    } else {
      this.ordersService.createOrder(orderData).subscribe({
        next: (res) => this.dialogRef.close(res),
        error: (err) => console.error(err)
      });
    }
  }

  close() {
    this.dialogRef.close();
  }
}
