using AudioSwitcherFace;
using MixerTypes;
using System.Data;
using System.Net;

var mixer = new Mixer();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy => {
        policy.AllowAnyOrigin();
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    });
});
builder.WebHost.ConfigureKestrel((context, serverOptions) =>
{
    serverOptions.Listen(IPAddress.Any, 5000, listenOptions => {});
});

var app = builder.Build();
app.UseCors();

app.MapGet("/sessions/", () => mixer.GetSessions());

app.MapPut("/sessions/{id}", async (string id, SessionObj sessionObject) =>
{
    try
    {
        CSessionUpdate SessionUpdateObj = new CSessionUpdate(
            sessionObject,
            mixer);
    }
    catch (InvalidOperationException)
    {
        // TODO: add some proper error types and messages
        return new Dictionary<string, string> { { "status", "error" } };
    }

    return new Dictionary<string, string> { { "status", "ok" } };
});

app.Run();
