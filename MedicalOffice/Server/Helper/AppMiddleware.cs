﻿namespace MedicalOffice.Server.Helper;

public static class AppMiddleware
{
    public static void AddAppCustom(this WebApplication app)
    {

        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }


        app.UseHttpsRedirection();
        app.UseCors("AllowSpecificOrigins");

        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();

        app.UseRouting();


        app.UseAuthentication();
        app.UseAuthorization();


        app.MapRazorPages();
        app.MapControllers();
        app.MapFallbackToFile("index.html");

    }
}
