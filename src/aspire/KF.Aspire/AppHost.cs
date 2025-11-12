var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("Postgres").WithDataVolume();

var keyForgeHost = builder.AddProject("key-forge-api", "../../backend/Host/Host.csproj")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(postgres).WaitFor(postgres)
    .WithReplicas(2);

builder.Build().Run();