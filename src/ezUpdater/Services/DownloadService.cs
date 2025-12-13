using System.Diagnostics;

namespace ezUpdater.Services
{
    public class DownloadService
    {
        private readonly HttpClient _http = new();

        public async Task<string> DownloadAsync(string url)
        {
            var tempZip = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".zip");

            var bytes = await _http.GetByteArrayAsync(url);
            await File.WriteAllBytesAsync(tempZip, bytes);

            return tempZip;
        }

        public async Task<string> DownloadDetailAsync(string url)
        {
            var tempZip = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".zip");

            using var response = await _http.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            var totalBytes = response.Content.Headers.ContentLength;
            using var stream = await response.Content.ReadAsStreamAsync();
            using var file = File.Create(tempZip);

            var buffer = new byte[81920];
            long totalRead = 0;
            int read;

            var sw = Stopwatch.StartNew();

            while ((read = await stream.ReadAsync(buffer)) > 0)
            {
                await file.WriteAsync(buffer.AsMemory(0, read));
                totalRead += read;

                if (totalBytes.HasValue)
                {
                    var percent = (int)(totalRead * 100 / totalBytes.Value);
                    var speed = totalRead / 1024d / 1024d / sw.Elapsed.TotalSeconds;

                    Console.Write(
                        $"\rDownloading: {percent}% | " +
                        $"{totalRead / 1024d / 1024d:0.0} MB / " +
                        $"{totalBytes.Value / 1024d / 1024d:0.0} MB | " +
                        $"{speed:0.0} MB/s   ");
                }
            }

            Console.WriteLine("\nDownload completed.");
            return tempZip;
        }
    }
}
