using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Alquileres {
  public static class AlquilerErrors {
    public static Error NotFound = new Error(
      "Alquiler.NotFound",
      "El alquiler con el Id especificado no fue encontrado"
    );

    public static Error Overlap = new Error(
      "Alquiler.Overlap",
      "El alquiler esta siendo tomado por 2 o mas clientes al mismo tiempo"
    );

    public static Error NotReserved = new Error("Alquiler.Notreservado", "El alquiler no esta reservado");

    public static Error NotConfirmed = new Error("Alquiler.NotConfirmed", "El alquiler no esta confirmado");

    public static Error AlreadyStarted = new Error("Alquiler.AlreadyStarted", "El alquiler ya ha comenzado");
  }
}
