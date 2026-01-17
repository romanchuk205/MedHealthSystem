using Aspire.Hosting;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

// --- 1. БАЗИ ДАНИХ ---

var sql = builder.AddSqlServer("sql")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

// ВИКЛЮЧАЄМО ВСЮ СКЛАДНУ ІНІЦІАЛІЗАЦІЮ (WithVolumeMount/WithInitScript),
// щоб уникнути помилок синтаксису.
var catalogDb = sql.AddDatabase("catalog-db", "MedHealthCatalogDB_v2");
var appointmentsDb = sql.AddDatabase("appointments-db", "MedHealthAppointmentsDB"); 

var mongo = builder.AddMongoDB("mongo")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var feedbackDb = mongo.AddDatabase("feedback-db", "MedHealthFeedbackDB");

// --- 2. МІКРОСЕРВІСИ (Backend) ---

var catalogApi = builder.AddProject<Projects.MedHealth_Catalog_Api>("catalog-api")
    // 👇 ФІНАЛЬНЕ ВИПРАВЛЕННЯ: УСУВАЄМО Ambiguous invocation, використовуючи найпростіший WithReference
    .WithReference(catalogDb) 
    .WithHttpEndpoint(name: "catalog-http");

var appointmentsApi = builder.AddProject<Projects.MedHealth_Appointments_Api>("appointments-api")
    // 👇 ФІНАЛЬНЕ ВИПРАВЛЕННЯ: УСУВАЄМО Ambiguous invocation
    .WithReference(appointmentsDb) 
    .WithHttpEndpoint(name: "appointments-http");

var feedbackApi = builder.AddProject<Projects.MedHealth_Feedback_Api>("feedback-api")
    .WithReference(feedbackDb)
    .WithHttpEndpoint(name: "feedback-http");

// --- 3. AGGREGATOR ---

var aggregator = builder.AddProject<Projects.MedHealth_Aggregator>("aggregator")
    .WithReference(catalogApi)
    .WithReference(appointmentsApi)
    .WithReference(feedbackApi)
    .WithHttpEndpoint(name: "aggregator-http");

// --- 4. API GATEWAY ---

builder.AddProject<Projects.MedHealth_ApiGateway>("gateway")
    .WithReference(catalogApi)
    .WithReference(appointmentsApi)
    .WithReference(feedbackApi)
    .WithReference(aggregator)
    .WithHttpEndpoint(port: 5000, name: "gateway-http");

builder.Build().Run();