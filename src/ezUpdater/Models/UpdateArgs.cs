namespace ezUpdater.Models
{
    public class UpdateArgs
    {
        public string AppDir { get; set; } = "";
        public string ZipUrl { get; set; } = "";
        public string ExeName { get; set; } = "";
        public int ParentPid { get; set; }
    }
}
