import { Component, inject } from '@angular/core';
import { ProductsService } from '../../../core/services/products.service';
import { Product } from '../../../shared/models/product.models';
import { ProductFormComponent } from '../product-form/product-form.component';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.scss'
})
export class ProductListComponent {
  private productsService = inject(ProductsService);
  private dialog = inject(MatDialog);

  displayedColumns: string[] = ['name', 'price', 'stock', 'actions'];
  products$: Observable<Product[]> = this.productsService.getProducts();

  openDialog(product?: Product) {
    const dialogRef = this.dialog.open(ProductFormComponent, {
      width: '500px',
      data: product || null
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.products$ = this.productsService.getProducts();
      }
    });
  }

  deleteProduct(id: number) {
    if (confirm('¿Estás seguro de eliminar este producto?')) {
      this.productsService.deleteProduct(id).subscribe({
        next: () => {
          this.products$ = this.productsService.getProducts(); // Refresh
        },
        error: (err) => console.error('Error deleting product', err)
      });
    }
  }
}
