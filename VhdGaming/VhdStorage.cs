using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace VhdGamer.Gaming
{
    // Auf GamesLibrary auf Filesystem Basis
    public class VhdStorage
    {
        public readonly DirectoryInfo Location;

        public VhdStorage(DirectoryInfo location)
        {
            this.Location = location;
            if (!this.Location.Exists)
            {
                throw new FileNotFoundException("Not in LAN Mode. You must fake " + location.FullName + " for yourself.");
            }
        }

        public Vhd GetVhd(String filename)
        {
            return new Vhd(new FileInfo(System.IO.Path.Combine(GetPathName(), filename)));
        }

        public String GetPathName()
        {
            return Location.FullName;
        }

        public ICollection<String> GetVhdNames()
        {
            ICollection<String> result = new List<String>();
            foreach (FileInfo f in Location.GetFiles("*.vhd"))
            {
                result.Add(f.Name);
            }
            return result;
        }

        public long GetSize()
        {
            long result = 0;
            foreach (FileInfo f in Location.GetFiles("*.vhd"))
            {
                result += f.Length;
            }
            return result;
        }

        // Synchronize vhds from Storage to this storage. Use filter to specify specific files to be synched.
        public void Syncronize(VhdStorage from, ICollection<String> filter = null)
        {
            // Create Filelist Diff >-----------------------------------------------------------
            ICollection<String> localVhdNames = GetVhdNames(); // cache for better performance
            ICollection<String> serverVhdNames = from.GetVhdNames();
            ICollection<String> diff = new List<String>();
            // if there is no filter, we take it all
            if (filter == null) 
            {
                filter = serverVhdNames;
            }
            foreach (String fileName in serverVhdNames)
            {
                if (filter.Contains(fileName) && !localVhdNames.Contains(fileName))
                {
                    diff.Add(fileName);
                }
            }

            // Sum Up Source Filesizes (Bytes) >------------------------------------------------
            long totalBytesToReceive = 0;
            foreach (String filename in diff)
            {
                FileInfo f = new FileInfo(from.GetPathName() + @"\" + filename);
                totalBytesToReceive += f.Length;
            }
            Debug.WriteLine(Location.Name + ": Bytes for Transfer: " + totalBytesToReceive);

            foreach (String fileName in diff)
            {
                String sourceFilePath = Path.Combine(from.GetPathName(), fileName);
                String targetFilePath = Path.Combine(this.GetPathName(), fileName);
                FileSystem.CopyFile(sourceFilePath, targetFilePath, UIOption.AllDialogs);
            }
        }
    }
}
