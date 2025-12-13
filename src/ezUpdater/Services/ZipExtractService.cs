using System.IO.Compression;

namespace ezUpdater.Services
{

    public class ZipExtractService
    {
        public void Extract(string zipPath, string targetDir)
        {
            using var archive = ZipFile.OpenRead(zipPath);

            foreach (var entry in archive.Entries)
            {
                var filePath = Path.Combine(targetDir, entry.FullName);

                Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

                if (string.IsNullOrEmpty(entry.Name))
                    continue;

                entry.ExtractToFile(filePath, overwrite: true);
            }
        }
    }
}
