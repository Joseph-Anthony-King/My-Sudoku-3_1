/**
 * 
 * My Sudoku 3.1
 * By Joseph King
 * September 9, 2015
 * 
 * SudokuUtilities.cs
 * 
 * This class defines utility methods used by the game.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MySudoku3_1
{
    /// <summary>
    /// The utilities class for the game
    /// </summary>
    public class SudokuUtilities
    {
        #region Random Number Generator
        /// <summary>
        /// Initiate a static random number generator
        /// </summary>
        public static Random generateRandomNumber = new Random();
        #endregion

        #region Shuffle<T>()
        /// <summary>
        /// This method shuffles elements in a list
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="list">Generic Type List</param>
        public static void Shuffle<T>(List<T> list)
        {
            var _randomShuffle = generateRandomNumber;

            for (int i = list.Count; i > 1; i--)
            {
                // Pick a random element to swap
                int j = _randomShuffle.Next(9);
                int k = _randomShuffle.Next(9);
                // Swap
                T tmp = list[j];
                list[j] = list[k];
                list[k] = tmp;
            }
        }
        #endregion

        #region ConvertStringArrayToString()
        /// <summary>
        /// Convert string arrays to strings
        /// </summary>
        /// <param name="array">String Array</param>
        /// <returns>String</returns>
        public static string ConvertStringArrayToString(string[] array)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string s in array)
            {
                sb.Append(s);
            }

            return sb.ToString();
        }
        #endregion

        #region CellIsCertain()
        /// <summary>
        /// This method is used when setting up the game.  If the 
        /// element is not a hint it will be written in black, if
        /// not it will be written in read.
        /// </summary>
        /// <param name="o">The object is a textbox</param>
        /// <param name="certain">The hint</param>
        public static void CellIsCertain(ref object o, Boolean certain)
        {
            TextBox tb = (TextBox)o;
            if (tb.IsReadOnly == false)
            {
                if (certain == true)
                    tb.Foreground = Brushes.Black;
                else
                    tb.Foreground = Brushes.SlateGray;
            }
        }
        #endregion

        #region cellIsValid()
        /// <summary>
        /// This method checks the users entry to ensure it's an integer
        /// between 1 and 9.
        /// </summary>
        /// <param name="o">The textbox</param>
        public static void CellIsValid(ref object o)
        {
            TextBox tb = (TextBox)o;
            int number;

            bool result = Int32.TryParse(tb.Text, out number);

            if (result)
            {
                if (number < 1 || number > 9)
                {
                    MessageBox.Show("Please enter a number between 1 through 9.");
                    tb.Text = "";
                }
            }
            else
            {
                if (tb.Text == "")
                { }
                else
                {
                    MessageBox.Show("Please enter a number between 1 through 9.");
                    tb.Text = "";
                }
            }
        }
        #endregion
    }
}