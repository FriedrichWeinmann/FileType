using System;

namespace FileType
{
    /// <summary>
    /// A mapping object, that maps a header to the correct mimetype as well as the usual file extension for that type.
    /// </summary>
    [Serializable]
    public class FileType
    {
        #region public Properties
        /// <summary>
        /// The header bytes an object of this type would have
        /// </summary>
        public byte?[] Header { get; set; }

        /// <summary>
        /// The size of the header
        /// </summary>
        public int HeaderOffset { get; set; }

        /// <summary>
        /// The extension associated with this type
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// The actual mime name
        /// </summary>
        public string Mime { get; set; }

        /// <summary>
        /// A description or comment of the filetype
        /// </summary>
        public string Description { get; set; }
        #endregion public Properties

        #region Constructors
        /// <summary>
        /// Initializes an empty instance of the <see cref="FileType"/> class.
        /// </summary>
        public FileType()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileType"/> struct.
        /// Takes the details of offset for the header
        /// </summary>
        /// <param name="Header">Byte array with header.</param>
        /// <param name="Offset">The header offset - how far into the file we need to read the header</param>
        /// <param name="Extension">String with extension.</param>
        /// <param name="Mime">The description of MIME.</param>
        /// <param name="Description">Describes the filetype or comments on its peculiarities.</param>
        public FileType(byte?[] Header, string Extension, string Mime, int Offset = 0, string Description = "")
        {
            this.Header = Header;
            HeaderOffset = Offset;
            this.Extension = Extension;
            this.Mime = Mime;
            this.Description = Description;
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Determines whether the specified header data matches a file of this type.
        /// </summary>
        /// <param name="HeaderData">The bytes to compare</param>
        /// <returns>Whether this type matches the data or not</returns>
        public bool IsType(byte[] HeaderData)
        {
            if (HeaderData.Length < HeaderOffset + Header.Length)
                return false;

            for (int index = 0; index < Header.Length; index++)
            {
                if (Header[index] == null)
                    continue;
                if (Header[index] != HeaderData[HeaderOffset + index])
                    return false;
            }

            return true;
        }
        #endregion Methods

        #region Overrides
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            if (!(other is FileType)) return false;

            FileType otherType = (FileType)other;

            if (this.Extension == otherType.Extension && this.Mime == otherType.Mime) return true;

            return base.Equals(other);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Extension}->{Mime}";
        }
        #endregion Overrides
    }
}