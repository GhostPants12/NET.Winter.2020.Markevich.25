using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using BLL.Interfaces;
using Types;

namespace UrlBLL
{
    public class URLTransformer : ITransformer<URLContainer>
    {
        private Regex pattern;
        private string splitPattern;

        public URLTransformer()
        {
            this.pattern = new Regex(@"(\S*)://(\S*)/(\S*)");
            this.splitPattern = @"(://)|(/)|(\?)";
        }

        public URLContainer Transform(string url)
        {
            if (!pattern.IsMatch(url))
            {
                throw new ArgumentException($" {url} is invalid URL address.");
            }

            string[] splitUrl = Regex.Split(url, splitPattern);
            splitUrl = splitUrl.Where((s => !String.IsNullOrEmpty(s))).ToArray();
            int index = splitUrl.Select((s => s.Equals("?") ? 1 : 0)).ToList().IndexOf(1);
            if (splitUrl.Contains("/"))
            {
                string[] path;
                int pathIndex = 0;
                if (index >= 0)
                {
                    Dictionary<string, string> parameters = new Dictionary<string, string>();
                    path = new string[(index-3)/2];
                    for (int i = 3; i < index; i++)
                    {
                        if (!splitUrl[i].Equals("/"))
                        {
                            path[pathIndex] = splitUrl[i];
                            pathIndex++;
                        }
                    }

                    for (int i = index; i < splitUrl.Length; i++)
                    {
                        if (!splitUrl[i].Equals("?"))
                        {
                            parameters.Add(splitUrl[i].Split('=')[0], splitUrl[i].Split('=')[1]);
                        }
                    }

                    return new URLContainer(splitUrl[0], splitUrl[2], path, parameters);
                }

                path = new string[(splitUrl.Length-3)/2];
                for (int i = 3; i < splitUrl.Length; i++)
                {
                    if (!splitUrl[i].Equals("/"))
                    {
                        path[pathIndex] = splitUrl[i];
                        pathIndex++;
                    }
                }

                return new URLContainer(splitUrl[0], splitUrl[2], path);
            }
            return new URLContainer(splitUrl[0], splitUrl[2]);
        }
    }
}
