using ezUpdater;
using ezUpdater.Services;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Console.WriteLine("*** ezUpdater started ***");

try
{
    var argService = new ArgumentService(args);
    var a = argService.Args;

    Console.WriteLine($"AppDir   : {a.AppDir}");
    Console.WriteLine($"ZipUrl   : {a.ZipUrl}");
    Console.WriteLine($"ExeName  : {a.ExeName}");

    var downloader = new DownloadService();
    var extractor = new ZipExtractService();
    var restarter = new RestartService();

    Console.WriteLine("- Downloading zip...");
    var zipPath = await downloader.DownloadDetailAsync(a.ZipUrl);
    Console.WriteLine($"Downloaded to: {zipPath}");

    Console.WriteLine("- Waiting for app to exit...");
    new ProcessWaitService().WaitForExit(a.ParentPid);

    Console.WriteLine("- Extracting...");
    extractor.Extract(zipPath, a.AppDir);
    Console.WriteLine("Extract done");

    Console.WriteLine("- Restarting app...");
    restarter.Restart(a.AppDir, a.ExeName);
    Console.WriteLine("- Done");
}
catch (Exception ex)
{
    Console.WriteLine("❌ ERROR:");
    Console.WriteLine(ex.ToString());
}

Console.WriteLine("Press any key to exit...");
Console.ReadKey();
