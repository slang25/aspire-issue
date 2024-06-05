var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.WebApplication1>("myapp");

builder.Build().Run();
