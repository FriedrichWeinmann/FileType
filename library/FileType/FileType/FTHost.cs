using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileType
{
    /// <summary>
    /// Provides basic 
    /// </summary>
    public static class FTHost
    {
        /// <summary>
        /// List of filetypes that have been registered
        /// </summary>
        public static List<FileType> FileTypes = new List<FileType>();

        /// <summary>
        /// The maximum size possibly needed to compare a files content with the registered filetypes
        /// </summary>
        public static int MaxHeader = 0;

        /// <summary>
        /// Checks, whether the data is of the specified file type.
        /// </summary>
        /// <param name="Content">The leading bytes of the file data, used to identify the file type</param>
        /// <param name="TypeName">The presumed typename</param>
        /// <param name="IgnoreUnknownTypes">Whether unknown types should return true or false</param>
        /// <returns>Whether the file data is of the expected type</returns>
        public static bool IsType(byte[] Content, string TypeName, bool IgnoreUnknownTypes)
        {
            string localTypeName = TypeName.Trim('.');
            bool foundType = false;
            foreach (FileType typeItem in FileTypes.Where(typeItem => String.Equals(typeItem.Extension, localTypeName, StringComparison.InvariantCultureIgnoreCase)))
            {
                if (typeItem.IsType(Content))
                    return true;
                foundType = true;
            }
            if (!foundType && IgnoreUnknownTypes)
                return true;
            return false;
        }

        /// <summary>
        /// Checks, whether the specified file is of the specified file type
        /// </summary>
        /// <param name="FullName">The full path to the file</param>
        /// <param name="TypeName">The presumed typename</param>
        /// <param name="IgnoreUnknownTypes">Whether unknown types should return true or false</param>
        /// <returns>Whether the file is of the expected type</returns>
        public static bool IsType(string FullName, string TypeName, bool IgnoreUnknownTypes)
        {
            if (!File.Exists(FullName))
                throw new ArgumentException($"File not found: {FullName}");

            byte[] headerContent = new byte[MaxHeader];
            using (FileStream fStream = new FileStream(FullName, FileMode.Open, FileAccess.Read))
            {
                fStream.Read(headerContent, 0, MaxHeader);
            }
            return IsType(headerContent, TypeName, IgnoreUnknownTypes);
        }

        /// <summary>
        /// Checks, whether the specified file is of the specified file type
        /// </summary>
        /// <param name="File">The file object to check</param>
        /// <param name="TypeName">The presumed typename</param>
        /// <param name="IgnoreUnknownTypes">Whether unknown types should return true or false</param>
        /// <returns>Whether the file is of the expected type</returns>
        public static bool IsType(FileInfo File, string TypeName, bool IgnoreUnknownTypes)
        {
            return IsType(File.FullName, TypeName, IgnoreUnknownTypes);
        }

        /// <summary>
        /// Checks, whether the specified file's extension matches its content.
        /// </summary>
        /// <param name="File">THe file to verify</param>
        /// <param name="IgnoreUnknownTypes">Whether unknown types should return true or false</param>
        /// <returns>whether the specified file's extension matches its content.</returns>
        public static bool IsValidType(FileInfo File, bool IgnoreUnknownTypes)
        {
            if (String.IsNullOrEmpty(File.Extension))
                return true;
            return IsType(File.FullName, File.Extension, IgnoreUnknownTypes);
        }

        /// <summary>
        /// Takes a file information object and returns the types it could be of.
        /// </summary>
        /// <param name="File">The file to resolve the type of</param>
        /// <returns>The types the input file could be part of</returns>
        public static List<FileType> ResolveType(FileInfo File)
        {
            List<FileType> fileTypes = new List<FileType>();

            byte[] headerContent = new byte[MaxHeader];
            using (FileStream fStream = new FileStream(File.FullName, FileMode.Open, FileAccess.Read))
            {
                fStream.Read(headerContent, 0, MaxHeader);
            }
            // Only return matching types that are NOT the default "No Extension" Type
            foreach (FileType typeItem in FileTypes.Where(type => type.IsType(headerContent) && (!String.IsNullOrEmpty(type.Mime) || !String.IsNullOrEmpty(type.Mime))))
                fileTypes.Add(typeItem);

            return fileTypes;
        }
    }
}
