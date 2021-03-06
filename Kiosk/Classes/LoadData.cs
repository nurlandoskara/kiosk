﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using Kiosk.Models;

namespace Kiosk.Classes
{
    public class LoadData
    {
        public static List<FileItem> GetFiles(string folder)
        {
            var di = new DirectoryInfo(folder);
            return di.GetFiles("*.jpg").Select(fi => new FileItem {FullPath = fi.FullName, Name = fi.Name}).ToList();
        }

        public static List<string> GetFolders(string folder)
        {
            var di = new DirectoryInfo(folder);
            return di.GetDirectories().Select(dir => dir.Name).ToList();
        }
    }
}
