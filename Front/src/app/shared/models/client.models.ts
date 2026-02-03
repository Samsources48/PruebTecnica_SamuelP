export interface Client {
  idCliente?: number;
  nombre: string;
  apellido: string;
  email: string;
  telefono?: string;
  direccion?: string;
  activo: boolean;
  fechaRegistro: string;
}
