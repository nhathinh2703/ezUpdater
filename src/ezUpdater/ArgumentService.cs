using ezUpdater.Models;

namespace ezUpdater
{
    public class ArgumentService
    {
        public UpdateArgs Args { get; } = new();

        public ArgumentService(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "--app-dir":
                        Args.AppDir = args[++i];
                        break;
                    case "--zip-url":
                        Args.ZipUrl = args[++i];
                        break;
                    case "--exe-name":
                        Args.ExeName = args[++i];
                        break;
                    case "--parent-pid":
                        Args.ParentPid = int.Parse(args[++i]);
                        break;
                }
            }

            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Args.AppDir))
                throw new ArgumentException("Missing --app-dir");

            if (string.IsNullOrWhiteSpace(Args.ZipUrl))
                throw new ArgumentException("Missing --zip-url");

            if (string.IsNullOrWhiteSpace(Args.ExeName))
                throw new ArgumentException("Missing --exe-name");

            if (Args.ParentPid <= 0)
                throw new ArgumentException("Missing --parent-pid");

        }
    }
}
