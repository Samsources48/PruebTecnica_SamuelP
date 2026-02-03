import { Component, inject, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ProductsService } from '../../../core/services/products.service';
import { Product } from '../../../shared/models/product.models';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrl: './product-form.component.scss'
})
export class ProductFormComponent {
  private fb = inject(FormBuilder);
  private productsService = inject(ProductsService);
  private dialogRef = inject(MatDialogRef<ProductFormComponent>);

  form: FormGroup;
  isEditMode: boolean = false;

  constructor(@Inject(MAT_DIALOG_DATA) public data: Product | null) {
    this.isEditMode = !!data;
    this.form = this.fb.group({
      nombreProducto: [data?.nombreProducto || '', [Validators.required]],
      precio: [data?.precio || 0, [Validators.required, Validators.min(0)]],
      stock: [data?.stock || 0, [Validators.required, Validators.min(0)]],
      descripcion: [data?.descripcion || '']
    });
  }

  save() {
    if (this.form.invalid) return;

    const productData = this.form.value;

    if (this.isEditMode && this.data) {
      const updatedProduct = { ...this.data, ...productData };
      this.productsService.updateProduct(this.data.idProducto, updatedProduct).subscribe({
        next: (res) => this.dialogRef.close(res),
        error: (err) => console.error(err)
      });
    } else {
      this.productsService.createProduct(productData).subscribe({
        next: (res) => this.dialogRef.close(res),
        error: (err) => console.error(err)
      });
    }
  }

  close() {
    this.dialogRef.close();
  }
}
