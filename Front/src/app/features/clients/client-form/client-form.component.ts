import { Component, inject, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ClientsService } from '../../../core/services/clients.service';
import { Client } from '../../../shared/models/client.models';

@Component({
  selector: 'app-client-form',
  templateUrl: './client-form.component.html',
  styleUrl: './client-form.component.scss'
})
export class ClientFormComponent {
  private fb = inject(FormBuilder);
  private clientsService = inject(ClientsService);
  private dialogRef = inject(MatDialogRef<ClientFormComponent>);

  form: FormGroup;
  isEditMode: boolean = false;

  constructor(@Inject(MAT_DIALOG_DATA) public data: Client | null) {
    this.isEditMode = !!data;
    this.form = this.fb.group({
      nombre: [data?.nombre || '', [Validators.required]],
      apellido: [data?.apellido || '', [Validators.required]],
      email: [data?.email || '', [Validators.required, Validators.email]],
      telefono: [data?.telefono || '', [Validators.pattern(/^\d{10}$/)]],
      direccion: [data?.direccion || ''],
      activo: [data?.activo ?? true] // Default to true if new
    });
  }

  save() {
    if (this.form.invalid) return;

    const clientData = this.form.value;

    if (this.isEditMode && this.data) {
      const updatedClient = { ...this.data, ...clientData };
      this.clientsService.updateClient(this.data.idCliente || 0, updatedClient).subscribe({
        next: (res) => this.dialogRef.close(res),
        error: (err) => console.error(err)
      });
    } else {
      this.clientsService.createClient(clientData).subscribe({
        next: (res) => this.dialogRef.close(res),
        error: (err) => console.error(err)
      });
    }
  }

  close() {
    this.dialogRef.close();
  }
}
