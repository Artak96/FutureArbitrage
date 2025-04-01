IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("futurearbitrage-db")
    .WithDataVolume()
    .WithPgAdmin();

builder.AddProject<Projects.FutureArbitrage_Api>("futurearbitrage-api")
    .WithReference(postgres);

builder.Build().Run();
