using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace _1_uzduotis__progress_bar_
{
    public class search
    {
        public List<string> filesFound = new List<string>();
        public void ShowAllFoldersUnder(string path, string keyword)
        {
            try
            {
                foreach (string foundItem in System.IO.Directory.GetFileSystemEntries(path, keyword))
                {
                    filesFound.Add(foundItem);
                }
            }
            catch
            {
            }
            try
            {
                foreach (string path2 in System.IO.Directory.GetDirectories(path))
                {
                    ShowAllFoldersUnder(path2, keyword);
                }
            }
            catch
            {

            }
        }
    }
}
