
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
app.MapPost("/webhook", async context =>
{
    if (!context.Request.Headers.ContainsKey("Authorization"))
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return;
    }
    var apiKey = context.Request.Headers["Authorization"];
    if(apiKey!="APIKey")
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return;
    }
    var requestBody = await context.Request.ReadFromJsonAsync<WebHookPayload>();
    Console.WriteLine($"Header :{requestBody?.Header},Body:{requestBody?.Body}");
    context.Response.StatusCode = 200;
    await context.Response.WriteAsync("Webhook Act !");
});

app.Run();

//public record WebHookPayload(string Header,string Body);
public record WebHookPayload(string Header,string Body);
/**
 In this case:
public record WebHookPayload(string Header, string Body);
is equivalent to defining this:
            ||
            \/
public class WebHookPayload
{
    public string Header { get; init; }
    public string Body { get; init; }

    public WebHookPayload(string header, string body)
    {
        Header = header;
        Body = body;
    }

    public override string ToString() => $"WebHookPayload {{ Header = {Header}, Body = {Body} }}";

    public override bool Equals(object obj)
    {
        if (obj is WebHookPayload other)
        {
            return Header == other.Header && Body == other.Body;
        }
        return false;
    }

    public override int GetHashCode() => HashCode.Combine(Header, Body);
}In this case:

csharp
Copy code
public record WebHookPayload(string Header, string Body);
is equivalent to defining this:

csharp
Copy code
public class WebHookPayload
{
    public string Header { get; init; }
    public string Body { get; init; }

    public WebHookPayload(string header, string body)
    {
        Header = header;
        Body = body;
    }

    public override string ToString() => $"WebHookPayload {{ Header = {Header}, Body = {Body} }}";

    public override bool Equals(object obj)
    {
        if (obj is WebHookPayload other)
        {
            return Header == other.Header && Body == other.Body;
        }
        return false;
    }

    public override int GetHashCode() => HashCode.Combine(Header, Body);
}
 */
