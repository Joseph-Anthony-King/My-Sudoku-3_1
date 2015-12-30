/**
 * 
 * My Sudoku 3.1
 * By Joseph King
 * September 9, 2015
 * 
 * UserSerializer.cs
 * 
 * This class defines the serializer object which is used to save
 * and load the user list objects.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MySudoku3_1
{
    public class UserSerializer
    {
        #region Constructor
        /// <summary>
        /// A constructor used to access the class
        /// </summary>
        public UserSerializer()
        {

        }
        #endregion

        #region SerializeRepository(string filename, SavedUserRepository savedUserRepository)
        /// <summary>
        /// A method to serialize the game repository
        /// </summary>
        /// <param name="filename">A string representation of the output file name</param>
        /// <param name="savedGameRepository">The saved game repository</param>
        public void SerializeRepository(string filename, SavedUserRepository savedUserRepository)
        {
            string filePath = ObtainPathToDatFile(filename);

            using (Stream stream = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (DeflateStream compressedStream = new DeflateStream(stream, CompressionMode.Compress))
                {
                    BinaryFormatter bFormatter = new BinaryFormatter();
                    bFormatter.Serialize(compressedStream, savedUserRepository);
                }
            }
        }
        #endregion

        #region DeserializeRepository(string filename)
        /// <summary>
        /// A method to deserialize the game repository
        /// </summary>
        /// <param name="filename">A string representation of the input file name</param>
        /// <returns>A SavedGameRepository object</returns>
        public SavedUserRepository DeserializeRepository(string filename)
        {
            SavedUserRepository savedUserRepository = new SavedUserRepository();

            string filePath = ObtainPathToDatFile(filename);

            using (Stream stream = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (DeflateStream decompressStream = new DeflateStream(stream, CompressionMode.Decompress))
                {
                    BinaryFormatter bFormatter = new BinaryFormatter();
                    if (stream.Length > 0)
                    {
                        savedUserRepository = (SavedUserRepository)bFormatter.Deserialize(decompressStream);
                    }
                }
            }

            return savedUserRepository;
        }
        #endregion

        #region ObtainPathToDatFile(string fileName)
        /// <summary>
        /// Private method to obtain the path to the local SaveGames file
        /// </summary>
        /// <param name="fileName">The name of the saved games file</param>
        /// <returns>The path to the saved games.</returns>
        private static string ObtainPathToDatFile(string fileName)
        {
            string localPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            string appPath = Path.Combine(localPath, "MySudoku3_0\\");

            if (!Directory.Exists(appPath))
            {
                Directory.CreateDirectory(appPath);
            }

            string filePath = Path.Combine(appPath, fileName);
            return filePath;
        }
        #endregion
    }
}
