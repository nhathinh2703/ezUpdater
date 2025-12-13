using System.Diagnostics;

namespace ezUpdater.Services
{
    public class RestartService
    {
        public void Restart(string appDir, string exeName)
        {
            var exePath = Path.Combine(appDir, exeName);

            if (!File.Exists(exePath))
                return;

            Process.Start(new ProcessStartInfo
            {
                FileName = exePath,
                WorkingDirectory = appDir,
                UseShellExecute = true
            });
        }
    }
}
