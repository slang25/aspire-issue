var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject("myapp", new Projects.WebApplication1().ProjectPath);

builder.Build().Run();
