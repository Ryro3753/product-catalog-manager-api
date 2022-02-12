using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace ProductTest.Integration;

public class WebApplication : WebApplicationFactory<Program>
{
  protected override IHost CreateHost(IHostBuilder builder)
  {
    return base.CreateHost(builder);
  }
}

public class BadRequestMessage
{
  public string Message { get; set; }
}