using System;
using System.IO;

namespace RazorPress.Build
{
    public class FileSystemTest : IDisposable
    {
        protected readonly DirectoryInfo directory;

        public FileSystemTest()
        {
            this.directory = new DirectoryInfo(Path.Combine(Path.GetTempPath(), Path.GetRandomFileName()));
        }

        public void Dispose()
        {
            if (this.directory.Exists)
            {
                this.directory.Delete(true);
            }
        }
    }
}
