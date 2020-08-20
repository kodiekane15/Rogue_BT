using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Rogue_BT.Helper
{
    public class FileStamp
    {
        public static string MakeUnique(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            fileName = Path.GetFileNameWithoutExtension(fileName);
            fileName = StringUtilities.URLFriendly(fileName);
            fileName = $"{fileName}{DateTime.Now.Ticks}{extension}";
            return fileName;
        }
    }
}