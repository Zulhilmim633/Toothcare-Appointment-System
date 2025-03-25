public class RoleMiddleware
{
    private readonly RequestDelegate _next;

    public RoleMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var userRole = context.Session.GetString("UserRole"); // Get role from session

        if (!string.IsNullOrEmpty(userRole))
        {
            if (context.Request.Path.StartsWithSegments("/Doctors") && userRole != "Doctor")
            {
                context.Response.Redirect("/staff");
                return;
            }

            if (context.Request.Path.StartsWithSegments("/staff") && userRole == "Doctor")
            {
                context.Response.Redirect("/Doctors");
                return;
            }
        }

        await _next(context);
    }
}
