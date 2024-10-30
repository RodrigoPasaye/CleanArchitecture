namespace CleanArchitecture.Api.Controllers.Alquileres {
  public sealed record ReservarAlquilerRequest(Guid VehiculoId, Guid UserId, DateOnly StartDate, DateOnly EndDate);
}
