namespace UIShell.PresentationCore
{
    /// <summary>
    /// Constants for command names.
    /// </summary>
    public class CommandNames
    {
        // File commands
        public const string Open = "Open";
        public const string Save = "Save";
        public const string SaveAs = "SaveAs";
        public const string NewDocument = "NewDocument";

        // Edit commands
        public const string Undo = "Undo";
        public const string Redo = "Redo";
        public const string Cut = "Cut";
        public const string Copy = "Copy";
        public const string Paste = "Paste";
        public const string Delete = "Delete";
        public const string SelectAll = "SelectAll";
    }

    public class ExtensionPointNames
    {
        public const string ToolStrip = "ToolBar";
        public const string MainMenu = "MainMenu";
        public const string FileFilters = "FileFilters";
    }
}