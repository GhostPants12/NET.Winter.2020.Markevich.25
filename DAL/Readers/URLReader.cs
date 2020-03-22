using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Types;

namespace UrlDAL
{
    public class URLReader : IReader
    {
        public IEnumerable<string> ReadInfo(string path)
        {
            string buf;
            using (StreamReader sr = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read)))
            {
                while (!sr.EndOfStream)
                {
                    buf = sr.ReadLine();
                    yield return buf;
                }
            }
        }
    }
}
