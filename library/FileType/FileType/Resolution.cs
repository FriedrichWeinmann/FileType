using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileType
{
    /// <summary>
    /// File Type resolution report.
    /// </summary>
    public class Resolution
    {
        /// <summary>
        /// Path to the file resolved.
        /// </summary>
        public string FullName;

        /// <summary>
        /// The types this file has
        /// </summary>
        public FileType[] FileTypes;
    }
}
