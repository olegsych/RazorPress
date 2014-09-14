using System;
using System.IO;

namespace RazorPress.Build
{
    public class FileSystemTest : IDisposable
    {
        private readonly DirectoryInfo directory;

        public FileSystemTest()
        {
            this.directory = new DirectoryInfo(Path.Combine(Path.GetTempPath(), Path.GetRandomFileName()));
        }

        protected DirectoryInfo Directory
        {
            get { return this.directory; }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.directory.Exists)
                {
                    this.directory.Delete(true);
                }
            }
        }
    }
}
