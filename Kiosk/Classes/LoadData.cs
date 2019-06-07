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
            return di.GetFiles("*.jpg").Select(fi => $"{folder}\\{fi.Name}").ToList();
        }
    }
}
