/**
 * 
 * My Sudoku 3.1
 * By Joseph King
 * September 9, 2015
 * 
 * SudokuExtensionMethods.cs
 * 
 * This class defines extension methods used by the sudoku game objects.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MySudoku3_1
{
    #region ContainsAnySimilarElements
    public static class SudokuExtensions
    {
        /// <summary>
        /// Compares two lists to see if they have any similar elements
        /// </summary>
        /// <param name="aList">Invocation Integer List</param>
        /// <param name="bList">parameter Integer List</param>
        /// <returns>Returns true if the lists are similar</returns>
        public static bool ContainsAnySimilarElements(this IEnumerable<int> aList, IEnumerable<int> bList)
        {
            bool result = false;

            foreach (int a in aList)
            {
                if (bList.Contains(a))
                {
                    result = true;
                }
            }

            return result;
        }
    }
    #endregion

    #region TryParse
    public static class SudokuStringExtensions
    {
        /// <summary>
        /// Verifies if a string can be parsed to an int
        /// </summary>
        public static bool TryParse(this string source)
        {
            int number;
            bool result;

            bool test = Int32.TryParse(source, out number);

            if (test == true)
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }
    }
    #endregion

    #region IndexRange
    public static class SudokuLinqExtension
    {
        /// <summary>
        /// Returns a sublist of a list
        /// </summary>
        /// <typeparam name="TSource">Generic Type</typeparam>
        /// <param name="source">Generic Source</param>
        /// <param name="fromIndex">Starting Index</param>
        /// <param name="toIndex">Final Index</param>
        /// <returns>A Generic IEnumerable Type</returns>
        public static IEnumerable<TSource> ReturnSublist<TSource>(
            this IList<TSource> source,
            int fromIndex,
            int toIndex)
        {
            int currIndex = fromIndex;
            while (currIndex <= toIndex)
            {
                yield return source[currIndex];
                currIndex++;
            }
        }
    }
    #endregion

    #region IsNameValid
    public static class StringExtensions
    {
        /// <summary>
        /// Extension method to ensure the name meets the appropriate
        /// criteria.  The name must be between 4 and 12 characters long.
        /// This ensures the name can appear on the user screen.
        /// </summary>
        /// <param name="myString">This string represents the users name</param>
        /// <returns>True if the name is between 4 and 12 characters.</returns>
        public static bool IsNameValid(this string myString)
        {
            if (myString.Length >= 3 && myString.Length <= 12)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Extension method to ensure the string only contains
        /// alphanumeric or blank characters
        /// </summary>
        /// <param name="myString">Invocation string</param>
        /// <returns>Returns true if the string only contains alphanumeric or blank characters</returns>
        public static bool IsEachCharAlphaNumericOrBlank(this string myString)
        {
            string pattern = "^[0-9A-Za-z ]+$";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(myString))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    #endregion

}