export interface Product {
  idProducto: number;
  nombreProducto: string;
  descripcion?: string;
  precio: number;
  stock: number;
  activo?: boolean;
  fechaRegistro?: string;
}
