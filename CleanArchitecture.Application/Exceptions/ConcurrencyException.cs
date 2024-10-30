namespace CleanArchitecture.Application.Exceptions {
  public sealed class ConcurrencyException : Exception {
    public ConcurrencyException(string message, Exception innerexception) : base(message, innerexception) { }
  }
}
