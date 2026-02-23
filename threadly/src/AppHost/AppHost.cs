var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
	.WithDataVolume();

var database = postgres.AddDatabase("defaultconnection");

var api = builder.AddProject<Projects.API>("api")
	.WithReference(database)
	.WaitFor(database);

builder.AddNpmApp("webapp", "../WebApp", "dev")
	.WithReference(api)
	.WaitFor(api)
	.WithEnvironment("BROWSER", "none")
	.WithEnvironment("PORT", "7608")
	.WithHttpEndpoint(port: 7608, env: "PORT", isProxied: false);

builder.Build().Run();
