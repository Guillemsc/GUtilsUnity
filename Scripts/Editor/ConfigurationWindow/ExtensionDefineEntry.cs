namespace GUtilsUnity.Packages
{
    public sealed class ExtensionDefineEntry
    {
        public string Define { get; }
        public string Name { get; }
        public string Description { get; }
        public bool Enabled { get; set; }

        public ExtensionDefineEntry(
            string name,
            string define,
            string description
        )
        {
            Name = name;
            Define = define;
            Description = description;
        }
    }
}
