using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using DAL.Interfaces;
using Types;

namespace UrlDAL
{
    public class URLReader : IReader<string>
    {
        /// <summary>Reads the information from the specified path file.</summary>
        /// <param name="path">The path.</param>
        /// <returns>Sequence of strings that was read from the file.</returns>
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
