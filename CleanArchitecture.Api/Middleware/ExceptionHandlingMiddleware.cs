﻿using CleanArchitecture.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Middleware {
  public class ExceptionHandlingMiddleware {

    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger) {
      _next = next;
      _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context) {
      try {
        await _next(context);
      } catch (Exception ex) {
        _logger.LogError(ex, "Ocurrio una excepcion: {Message}", ex.Message);
        var exceptionDetails = GetExceptionDetails(ex);
        var problemDetails = new ProblemDetails {
          Status = exceptionDetails.Status,
          Type = exceptionDetails.Type,
          Title = exceptionDetails.Title,
          Detail = exceptionDetails.Detail,
        };

        if (exceptionDetails.Errors is not null) {
          problemDetails.Extensions["errors"] = exceptionDetails.Errors;
        }

        context.Response.StatusCode = exceptionDetails.Status;

        await context.Response.WriteAsJsonAsync(problemDetails);
      }
    }

    private static ExceptionDetails GetExceptionDetails(Exception ex) {
      return ex switch {
        ValidationException validationException => new ExceptionDetails(
          StatusCodes.Status400BadRequest,
          "ValidationFailure",
          "Validacion de Error",
          "Han ocurrido uno o mas errores de validacion",
          validationException.Errors
        ),
        _ => new ExceptionDetails(
          StatusCodes.Status500InternalServerError,
          "ServerError",
          "Error de Servidor",
          "Ha ocurrido un inesperado error en la App",
          null
        ),
      };
    }

    internal record ExceptionDetails(int Status, string Type, string Title, string Detail, IEnumerable<object> Errors);
  }
}
