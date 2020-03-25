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
        private readonly Regex pattern = new Regex(@"(\w+)://(\w+)");
        private readonly string splitPattern = @"(://)|(/)|(\?)";

        /// <summary>Initializes a new instance of the <see cref="URLTransformer"/> class.</summary>
        public URLTransformer()
        {
        }

        /// <summary>Transforms the specified string value to URLContainer object.</summary>
        /// <param name="url">The URL's string representation.</param>
        /// <returns>URLContainer object.</returns>
        /// <exception cref="ArgumentException">The string value is not a valid URL address.</exception>
        public virtual URLContainer Transform(string url)
        {
            if (!this.pattern.IsMatch(url))
            {
                throw new ArgumentException($" {url} is invalid URL address.");
            }

            string[] splitUrl = Regex.Split(url, this.splitPattern);
            splitUrl = splitUrl.Where(s => !string.IsNullOrEmpty(s)).ToArray();
            int index = splitUrl.Select(s => s.Equals("?", StringComparison.InvariantCultureIgnoreCase) ? 1 : 0).ToList().IndexOf(1);
            if (splitUrl.Contains("/"))
            {
                string[] path;
                int pathIndex = 0;
                if (index >= 0)
                {
                    Dictionary<string, string> parameters = new Dictionary<string, string>();
                    path = new string[(index - 3) / 2];
                    for (int i = 3; i < index; i++)
                    {
                        if (!splitUrl[i].Equals("/", StringComparison.InvariantCultureIgnoreCase))
                        {
                            path[pathIndex] = splitUrl[i];
                            pathIndex++;
                        }
                    }

                    for (int i = index; i < splitUrl.Length; i++)
                    {
                        if (!splitUrl[i].Equals("?", StringComparison.InvariantCultureIgnoreCase))
                        {
                            parameters.Add(splitUrl[i].Split('=')[0], splitUrl[i].Split('=')[1]);
                        }
                    }

                    return new URLContainer(splitUrl[0], splitUrl[2], path, parameters);
                }

                path = new string[(splitUrl.Length - 3) / 2];
                for (int i = 3; i < splitUrl.Length; i++)
                {
                    if (!splitUrl[i].Equals("/", StringComparison.InvariantCultureIgnoreCase))
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
