using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Kiosk.Classes
{
    public class LoadData
    {
        public static List<string> GetFiles(string folder)
        {
            var di = new DirectoryInfo(folder);
            return di.GetFiles("*.jpg").Select(fi => fi.FullName).ToList();
        }

        public static List<string> GetFolders(string folder)
        {
            var di = new DirectoryInfo(folder);
            return di.GetDirectories().Select(dir => dir.FullName).ToList();
        }
    }
}
