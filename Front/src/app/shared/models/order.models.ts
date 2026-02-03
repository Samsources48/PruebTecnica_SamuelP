export interface OrderDetail {
  idDetallePedido: number;
  idProducto: number;
  cantidad: number;
  precioUnitario: number;
  subtotal: number;
}

export interface Order {
  idPedido: number;
  descripcion?: string;
  fechaPedido: string;
  total: number;
  estado: string;
  activo?: boolean;
  idCliente: number;
  detalles: OrderDetail[];
}
