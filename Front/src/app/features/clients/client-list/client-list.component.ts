import { Component, inject } from '@angular/core';
import { ClientsService } from '../../../core/services/clients.service';
import { Client } from '../../../shared/models/client.models';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { ClientFormComponent } from '../client-form/client-form.component';

@Component({
  selector: 'app-client-list',
  templateUrl: './client-list.component.html',
  styleUrl: './client-list.component.scss'
})
export class ClientListComponent {
  private clientsService = inject(ClientsService);
  private dialog = inject(MatDialog);

  displayedColumns: string[] = ['name', 'email', 'phone', 'actions'];
  clients$: Observable<Client[]> = this.clientsService.getClients();

  openDialog(client?: Client) {
    const dialogRef = this.dialog.open(ClientFormComponent, {
      width: '500px',
      data: client || null
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.clients$ = this.clientsService.getClients();
      }
    });
  }

  deleteClient(id: number) {
    if (confirm('¿Estás seguro de eliminar este cliente?')) {
      this.clientsService.deleteClient(id).subscribe({
        next: () => {
          this.clients$ = this.clientsService.getClients(); // Refresh list
        },
        error: (err) => console.error('Error deleting client', err)
      });
    }
  }
}
