using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Types
{
    /// <summary>Class-container for the URL address.</summary>
    public class URLContainer
    {
        private readonly string[] path;
        private readonly Dictionary<string, string> parameters;

        /// <summary>Initializes a new instance of the <see cref="URLContainer"/> class.</summary>
        /// <param name="scheme">The scheme.</param>
        /// <param name="host">The host.</param>
        /// <param name="path">The path.</param>
        /// <param name="parameters">The parameters.</param>
        public URLContainer(string scheme, string host, string[] path = null, Dictionary<string, string> parameters = null)
        {
            this.Scheme = scheme;
            this.Host = host;
            this.path = path;
            this.parameters = parameters;
        }

        /// <summary>Gets the scheme of URL address.</summary>
        /// <value>The scheme.</value>
        public string Scheme { get; private set; }

        /// <summary>Gets the host of the URL address.</summary>
        /// <value>The host.</value>
        public string Host { get; private set; }

        /// <summary>Gets the path of the URL address.</summary>
        /// <returns>Collection of all elements in the path of the URL address.</returns>
        public ReadOnlyCollection<string> GetPath()
        {
            if (this.path is null)
            {
                return null;
            }

            return new ReadOnlyCollection<string>(path);
        }

        /// <summary>Gets the parameters of URL address.</summary>
        /// <returns>Dictionary of parameters and their values.</returns>
        public ReadOnlyDictionary<string, string> GetParameters()
        {
            if (this.parameters is null)
            {
                return null;
            }

            return new ReadOnlyDictionary<string, string>(this.parameters);
        }
    }
}
