using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Domain.Alquileres {
  public class PrecioService {
    public PrecioDetalle CalcularPrecio(Vehiculo vehiculo, DateRange periodo) {

      var tipoMoneda = vehiculo.Precio!.TipoMoneda;

      var precioPeriodo = new Moneda(periodo.CantidadDias * vehiculo.Precio.Monto, tipoMoneda);

      decimal porcentaje = 0;

      foreach (var accesorio in vehiculo.Accesorios) {
        porcentaje += accesorio switch {
          Accesorio.AppleCar or Accesorio.Androidcar => 0.05m,
          Accesorio.AireAcondicionado => 0.01m,
          Accesorio.Mapas => 0.01m,
          _ => 0
        };
      }

      var accesorios = Moneda.Zero(tipoMoneda);

      if (porcentaje > 0) {
        accesorios = new Moneda(precioPeriodo.Monto * porcentaje, tipoMoneda);
      }

      var precioTotal = Moneda.Zero();
      precioTotal += precioPeriodo;

      if (!vehiculo.Mantenimiento.IsZero()) {
        precioTotal += vehiculo.Mantenimiento;
      }

      precioTotal += accesorios;

      return new PrecioDetalle(precioPeriodo, vehiculo.Mantenimiento, accesorios, precioTotal);
    }
  }
}
