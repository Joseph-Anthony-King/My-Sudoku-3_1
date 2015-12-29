/**
 * 
 * My Sudoku 3.1
 * By Joseph King
 * September 9, 2015
 * 
 * SavedGameRepository.cs
 * 
 * This class defines the saved game repository.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MySudoku3_1
{
    /// <summary>
    /// This holds a reference to the game repository to be serialized
    /// </summary>
    [Serializable()]
    public class SavedGameRepository : ISerializable
    {
        #region Properties
        /// <summary>
        /// A public property to hold the game list
        /// </summary>
        public List<SudokuGame> RepositorySavedGameList { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// A constructor used to build the game repository from memory
        /// </summary>
        /// <param name="info">The info parameter</param>
        /// <param name="context">The context parameter</param>
        public SavedGameRepository(SerializationInfo info, StreamingContext context)
        {
            this.RepositorySavedGameList = (List<SudokuGame>)info.GetValue("SavedGames.dat", typeof(List<SudokuGame>));
        }

        /// <summary>
        /// A parameterless constructor
        /// </summary>
        public SavedGameRepository()
        {
            RepositorySavedGameList = new List<SudokuGame>();
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
            info.AddValue("SavedGames.dat", this.RepositorySavedGameList);
        }
        #endregion
    }
}