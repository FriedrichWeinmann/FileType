using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileType
{
    /// <summary>
    /// Result of a file type validation
    /// </summary>
    [Serializable]
    public class TestResult
    {
        /// <summary>
        /// Does the file content match its extension?
        /// </summary>
        public bool IsValid;

        /// <summary>
        /// Was testing possible?
        /// </summary>
        public Nullable<bool> Success;

        /// <summary>
        /// Full name / path to the file tested.
        /// </summary>
        public string FullName;

        /// <summary>
        /// The base path the search was based off from
        /// </summary>
        public string BasePath;

        /// <summary>
        /// Path relative to the searchbase stored in BasePath
        /// </summary>
        public string RelativePath
        {
            get { return FullName.Replace(BasePath, ""); }
            set { } // Included to have the default PSF Serializer be happy.
        }

        /// <summary>
        /// Create an empty FileType testresult.
        /// </summary>
        public TestResult()
        {

        }

        /// <summary>
        /// Create a filled out FIleType testresult
        /// </summary>
        /// <param name="IsValid">Does the file content match its extension?</param>
        /// <param name="Success">Was testing possible?</param>
        /// <param name="FullName">Full name / path to the file tested.</param>
        public TestResult(bool IsValid, Nullable<bool> Success, string FullName)
        {
            this.IsValid = IsValid;
            this.Success = Success;
            this.FullName = FullName;
        }
    }
}
