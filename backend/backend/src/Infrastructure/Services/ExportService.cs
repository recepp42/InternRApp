using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.Extensions.Hosting;

namespace backend.Infrastructure.Services;
public class ExportService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        string environmentVar = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        string exportDirectory;

        if (environmentVar == "Development")
        {
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            exportDirectory = Path.Combine(sCurrentDirectory, $@"..\..\..\wwwroot\lib\ExportFiles\");
        }
        else
        {
            exportDirectory = @"/home/site/wwwroot/wwwroot/lib/ExportFiles/";
        }

        while (!stoppingToken.IsCancellationRequested)
        {
            DirectoryInfo di = new DirectoryInfo(exportDirectory);
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            await Task.Delay(86400000, stoppingToken); //after 1 day
        }


    }
}
