/**
 * 
 * My Sudoku 3.1
 * By Joseph King
 * September 9, 2015
 * 
 * SavedGameRepository.cs
 * 
 * This class defines the saved user repository.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MySudoku3_1
{
    /// <summary>
    /// This holds a reference to the user repository to be serialized
    /// </summary>
    [Serializable()]
    public class SavedUserRepository : ISerializable
    {
        #region Properties
        /// <summary>
        /// A public property to hold the user list
        /// </summary>
        public List<User> RepositorySavedUserList { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// A constructor used to build the user repository from memory
        /// </summary>
        /// <param name="info">The info parameter</param>
        /// <param name="context">The context parameter</param>
        public SavedUserRepository (SerializationInfo info, StreamingContext context)
        {
            this.RepositorySavedUserList = (List<User>)info.GetValue("SavedUsers.dat", typeof(List<User>));
        }

        /// <summary>
        /// A parameterless constructor
        /// </summary>
        public SavedUserRepository()
        {
            RepositorySavedUserList = new List<User>();
        }

        #endregion

        #region ISerializable Member
        /// <summary>
        /// The ISerializable Member
        /// </summary>
        /// <param name="info">The info parameter</param>
        /// <param name="context">The context parameter</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("SavedUsers.dat", this.RepositorySavedUserList);
        }
        #endregion
    }
}
