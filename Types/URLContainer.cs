using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Types
{
    public class URLContainer
    {
        public string Scheme { get; private set; } 
        public string Host { get; private set; }
        private string[] path;
        private Dictionary<string, string> parameters;

        public URLContainer(string scheme, string host, string[] path = null, Dictionary<string, string> parameters = null)
        {
            this.Scheme = scheme;
            this.Host = host;
            this.path = path;
            this.parameters = parameters;
        }

        public ReadOnlyCollection<string> GetPath()
        {
            if (path is null)
            {
                return null;
            }

            return new ReadOnlyCollection<string>(path);
        }

        public ReadOnlyDictionary<string, string> GetParameters()
        {
            if (parameters is null)
            {
                return null;
            }

            return new ReadOnlyDictionary<string, string>(this.parameters);
        }
    }
}
