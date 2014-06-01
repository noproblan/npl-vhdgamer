using System.IO;

namespace VhdGamer.Gaming
{
    public class Vhd
    {
        public FileInfo Location;
        private VirtualDisk disk;
        private bool isMounted;
        public bool IsMounted { get { return isMounted; } }

        public Vhd(FileInfo location)
        {
            this.Location = location;
            this.disk = new VirtualDisk(this.Location.FullName);
            this.isMounted = false;
        }

        ~Vhd()
        {
            if (IsMounted) 
            {
                Umount();
            }
        }

        public void Mount()
        {
            this.disk.Open();
            this.disk.Attach(VirtualDiskAttachOptions.None);
            this.isMounted = true;
        }

        public void Umount()
        {
            this.disk.Detach();
            this.isMounted = false;
        }

        public DirectoryInfo GetMountLocation()
        {
            return new DirectoryInfo(this.disk.GetDriveLetter());
        }
    }
}
