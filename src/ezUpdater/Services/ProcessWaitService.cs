using System.Diagnostics;

namespace ezUpdater.Services
{
    public class ProcessWaitService
    {
        public void WaitForExit(int pid, int timeoutSeconds = 30)
        {
            try
            {
                var process = Process.GetProcessById(pid);
                if (!process.WaitForExit(timeoutSeconds * 1000))
                    throw new TimeoutException("App vẫn đang chạy, không thể update.");
            }
            catch (ArgumentException)
            {
                // process đã exit
            }
        }
    }
}
