using AudioSwitcherFace;
using MixerTypes;
using System.Data;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var mixer = new Mixer();

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
