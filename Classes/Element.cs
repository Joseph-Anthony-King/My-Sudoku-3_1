/**
 * 
 * My Sudoku 3.1
 * By Joseph King
 * September 9, 2015
 * 
 * Element.cs
 * 
 * The element class defines the elements in the sudoku matrix. 
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
    /// This class defines the elements of the sudoku matrix.
    /// </summary>
    [Serializable()]
    public class Element : ISerializable
    {
        #region Properties
        /// <summary>
        /// This property gets and sets the element's integer value
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// This property gets and sets the element's display hint as a boolean
        /// </summary>
        public bool DisplayHint { get; set; }

        /// <summary>
        /// This property represents the user's certainity of their entry as a boolean
        /// </summary>
        public bool Certain { get; set; }
        #endregion

        #region Constructors 
        /// <summary>
        /// An element constructor that takes the value the element holds
        /// and a bool that indicates if it is a hint or not
        /// </summary>
        /// <param name="x">The value of the element</param>
        /// <param name="hint">A boolean value that indicates if its a hint or not</param>
        public Element(int x, bool hint)
        {
            Number = x;
            DisplayHint = hint;
            Certain = false;
        }

        /// <summary>
        /// An element constructor that takes the value the element holds
        /// </summary>
        /// <param name="x">The value of the element</param>
        public Element(int x)
        {
            Number = x;
            DisplayHint = false;
            Certain = false;
        }

        /// <summary>
        /// A default constructor for the element class
        /// </summary>
        public Element()
        {
            Number = 0;
            DisplayHint = false;
            Certain = false;
        }

        // ISeriazable Constructor
        public Element(SerializationInfo info, StreamingContext context)
        {
            this.Number = (int)info.GetValue("Number", typeof(int));
            this.DisplayHint = (bool)info.GetValue("DisplayHint", typeof(bool));
            this.Certain = (bool)info.GetValue("Certain", typeof(bool));
        }
        #endregion

        #region Element Methods
        /// <summary>
        /// An override for the ToString() method
        /// </summary>
        /// <returns>A string representation for the value held by the element</returns>
        public override string ToString()
        {
            string s = Number.ToString();
            return s;
        }
        #endregion

        #region ISerializable Members
        /// <summary>
        /// The ISerializable member
        /// </summary>
        /// <param name="info">The info parameter</param>
        /// <param name="context">The context parameter</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Number", this.Number);
            info.AddValue("DisplayHint", this.DisplayHint);
            info.AddValue("Certain", this.Certain);
        }
        #endregion
    }
}