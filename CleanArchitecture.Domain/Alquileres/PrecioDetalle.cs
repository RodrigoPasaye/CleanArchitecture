using CleanArchitecture.Domain.Shared;

namespace CleanArchitecture.Domain.Alquileres {
  public record PrecioDetalle(
    Moneda PrecioPeriodo,
    Moneda Mantenimiento,
    Moneda Accesorios,
    Moneda PrecioTotal
  );
}
