/**
 * 
 * My Sudoku 3.1
 * By Joseph King
 * September 9, 2015
 * 
 * SudokuGame.cs
 * 
 * This class defines the sudoku game objects.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MySudoku3_1
{
    /// <summary>
    /// This class holds the game class instance properties and methods
    /// </summary>
    [Serializable()]
    public class SudokuGame : ISerializable, IComparer<SudokuGame>
    {
        #region Difficulty Enumeration
        /// <summary>
        /// An enumeration of the difficulty level
        /// </summary>
        public enum Difficulty { EASY, MEDIUM, HARD };
        #endregion

        #region Completed Enumeration
        /// <summary>
        /// An enumeration of the completed status
        /// </summary>
        public enum Completed { Yes, No };
        #endregion

        #region Properties
        /// <summary>
        /// User Name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Difficulty level
        /// </summary>
        public Difficulty GameDifficulty { get; set; }

        /// <summary>
        /// Completion status
        /// </summary>
        public Completed CompletedState { get; set; }

        /// <summary>
        /// Date created
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Date Completed
        /// </summary>
        public DateTime DateCompleted { get; set; }

        /// <summary>
        /// Game Name
        /// </summary>
        public string GameName { get; set; }

        /// <summary>
        /// The sudoku matrix
        /// </summary>
        public List<int> SudokuSolution { get; set; }

        /// <summary>
        /// String that holds the list of integers
        /// </summary>
        public string SolutionString { get; set; }

        /// <summary>
        /// List of element objects
        /// </summary>
        public List<Element> ElementList { get; set; }

        /// <summary>
        /// StringBuilder to hold the element list for processing
        /// </summary>
        public StringBuilder UserEntriesSB { get; set; }

        /// <summary>
        /// Integer that determines the amount of elements that will be displayed
        /// </summary>
        public int Display { get; set; }

        /// <summary>
        /// The user's score
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// The ticks that have elapsed since the game started
        /// </summary>
        public int Ticks { get; set; }

        /// <summary>
        /// Negative points assessed against the user
        /// </summary>
        public int Demerits { get; set; }

        /// <summary>
        /// String to display of the difficulty level
        /// </summary>
        public string DisplayDifficultyLevel { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// This constructor builds a new game
        /// </summary>
        /// <param name="level">Enumeration of the difficulty level</param>
        /// <param name="user">String that represents the users name</param>
        public SudokuGame(string user, Difficulty level)
        {
            UserName = user;
            GameDifficulty = level;
            DateCreated = DateTime.Now;
            DateCompleted = DateCreated;
            CompletedState = Completed.No;
            SudokuSolution = SodukuSolutionGenerator.SolutionGenerator();
            ElementList = new List<Element>();
            UserEntriesSB = new StringBuilder();

            // Initialize SolutionString
            SolutionString = "";

            // Load the solution string which will be used to test the users solution
            foreach (int _number in SudokuSolution)
            {
                SolutionString = SolutionString + _number;
            }

            // Load the element list with elements based on the solution string
            foreach (int _element in SudokuSolution)
            {
                Element element = new Element(_element);
                ElementList.Add(element);
            }

            // Determine the amount of elements which will be displayed
            if (GameDifficulty == Difficulty.EASY)
                Display = 36;
            else if (GameDifficulty == Difficulty.MEDIUM)
                Display = 32;
            else if (GameDifficulty == Difficulty.HARD)
                Display = 27;

            // Set the amount of hints the user will receive
            for (int i = 0; i < Display; )
            {
                int j = SudokuUtilities.generateRandomNumber.Next(0, ElementList.Count - 1);

                if (ElementList[j].DisplayHint == false)
                {
                    ElementList[j].DisplayHint = true;
                    i++;
                }
            }

            // Initialize score to 0
            Score = 0;

            // Initialize the ticks to 0
            Ticks = 0;

            // Initialize demerits to 0
            Demerits = 0;

            // Set DisplayDifficultyLevel string
            if (GameDifficulty == Difficulty.EASY)
            {
                DisplayDifficultyLevel = "Steady Sloth";
            }
            else if (GameDifficulty == Difficulty.MEDIUM)
            {
                DisplayDifficultyLevel = "Leaping Lemur";
            }
            else if (GameDifficulty == Difficulty.HARD)
            {
                DisplayDifficultyLevel = "Mighty Mountain Lion";
            }
        }

        /// <summary>
        /// A constructor for building a game from memory
        /// </summary>
        public SudokuGame(SerializationInfo info, StreamingContext ctxt)
        {
            this.GameName = (string)info.GetValue("GameName", typeof(string));
            this.UserName = (string)info.GetValue("UserName", typeof(string));
            this.GameDifficulty = (Difficulty)info.GetValue("GameDifficulty", typeof(Difficulty));
            this.CompletedState = (Completed)info.GetValue("Completed", typeof(Completed));
            this.DateCreated = (DateTime)info.GetValue("DateCreated", typeof(DateTime));
            this.DateCompleted = (DateTime)info.GetValue("DateCompleted", typeof(DateTime));
            this.SudokuSolution = (List<int>)info.GetValue("SudokuSolution", typeof(List<int>));
            this.SolutionString = (string)info.GetValue("SolutionString", typeof(string));
            this.ElementList = (List<Element>)info.GetValue("ElementList", typeof(List<Element>));
            this.UserEntriesSB = (StringBuilder)info.GetValue("ElementSB", typeof(StringBuilder));
            this.Display = (int)info.GetValue("Display", typeof(int));
            this.Score = (int)info.GetValue("Score", typeof(int));
            this.Ticks = (int)info.GetValue("Ticks", typeof(int));
            this.Demerits = (int)info.GetValue("Demerits", typeof(int));
            this.DisplayDifficultyLevel = (string)info.GetValue("DisplayDifficultyLevel", typeof(string));
        }

        /// <summary>
        /// A parameterless constructor
        /// </summary>
        public SudokuGame()
        {
            UserName = "Player";
            GameDifficulty = Difficulty.MEDIUM;
            DateCreated = DateTime.Now;
            DateCompleted = DateCreated;
            CompletedState = Completed.No;
            SudokuSolution = SodukuSolutionGenerator.SolutionGenerator();
            ElementList = new List<Element>();
            UserEntriesSB = new StringBuilder();

            // Load the solution string which will be used to test the users solution
            foreach (int _number in SudokuSolution)
            {
                SolutionString = SolutionString + _number + " ";
            }

            // Load the element list with elements based on the solution string
            foreach (int _element in SudokuSolution)
            {
                Element element = new Element(_element);
                ElementList.Add(element);
            }

            // Determine the amount of elements which will be displayed
            if (GameDifficulty == Difficulty.EASY)
                Display = 36;
            else if (GameDifficulty == Difficulty.MEDIUM)
                Display = 32;
            else if (GameDifficulty == Difficulty.HARD)
                Display = 27;

            // Set the amount of hints the user will receive
            for (int i = 0; i < Display; )
            {
                int j = SudokuUtilities.generateRandomNumber.Next(0, ElementList.Count - 1);

                if (ElementList[j].DisplayHint == false)
                {
                    ElementList[j].DisplayHint = true;
                    i++;
                }
            }

            // Convert the element list into a StringBuilder
            // This StringBuilder is passed to the diplay to build the game board
            // This also records the users entries
            foreach (Element element in ElementList)
            {
                if (element.DisplayHint == true)
                {
                    UserEntriesSB.Append(element.ToString() + " ");
                }
                else
                {
                    UserEntriesSB.Append("_ ");
                }
            }

            // Initialize score to 0
            Score = 0;

            // Initialize the ticks to 0
            Ticks = 0;

            // Initialize demerits to 0
            Demerits = 0;

            // Set DisplayDifficultyLevel
            if (GameDifficulty == Difficulty.EASY)
            {
                DisplayDifficultyLevel = "Steady Sloth";
            }
            else if (GameDifficulty == Difficulty.MEDIUM)
            {
                DisplayDifficultyLevel = "Leaping Lemur";
            }
            else if (GameDifficulty == Difficulty.HARD)
            {
                DisplayDifficultyLevel = "Mighty Mountain Lion";
            }
        }
        #endregion

        #region Iseriablizable Members
        /// <summary>
        /// Implementation of the ISeriablizable Members
        /// </summary>
        /// <param name="info">SerializationInfo info</param>
        /// <param name="context">StreamingContext context</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("GameName", this.GameName);
            info.AddValue("UserName", this.UserName);
            info.AddValue("GameDifficulty", this.GameDifficulty);
            info.AddValue("Completed", this.CompletedState);
            info.AddValue("DateCreated", this.DateCreated);
            info.AddValue("DateCompleted", this.DateCompleted);
            info.AddValue("SudokuSolution", this.SudokuSolution);
            info.AddValue("SolutionString", this.SolutionString);
            info.AddValue("ElementList", this.ElementList);
            info.AddValue("ElementSB", this.UserEntriesSB);
            info.AddValue("Display", this.Display);
            info.AddValue("Score", this.Score);
            info.AddValue("Ticks", this.Ticks);
            info.AddValue("Demerits", this.Demerits);
            info.AddValue("DisplayDifficultyLevel", this.DisplayDifficultyLevel);
        }
        #endregion

        #region CalculateSolution()
        /// <summary>
        /// This method calculates the solution
        /// </summary>
        /// <returns>The Score property</returns>
        public int CalculateSolution()
        {
            int maxScore = 0;
            int result = 1;

            if (this.GameDifficulty == Difficulty.EASY)
                maxScore = 7200;
            else if (this.GameDifficulty == Difficulty.MEDIUM)
                maxScore = 14400;
            else if (this.GameDifficulty == Difficulty.HARD)
                maxScore = 28800;

            if ((Ticks + Demerits) > maxScore)
                result = 1;
            else
                result = maxScore - (Ticks + Demerits);

            return result;
        }
        #endregion

        #region ToString()
        public override string ToString()
        {
            string scoreString = "";
            string difficulty = "";
            string result;

            if (GameDifficulty == Difficulty.EASY)
            {
                difficulty = "Steady Sloth";
            }
            else if (GameDifficulty == Difficulty.MEDIUM)
            {
                difficulty = "Leaping Lemur";
            }
            else if (GameDifficulty == Difficulty.HARD)
            {
                difficulty = "Mighty Mountain Lion";
            }

            if (Score == 0)
            {
                scoreString = "In Progress";
            }
            else
            {
                scoreString = "Completed";
            }

            result = UserName + " | " + DateCreated + " | " + difficulty + " | " + scoreString;

            return result;
        }
        #endregion

        #region IComparer Members
        /// <summary>
        /// A comparison method that compares games by the dates created
        /// </summary>
        /// <param name="x">SudokuConsoleGame one</param>
        /// <param name="y">SudokuConsoleGame two</param>
        /// <returns></returns>
        public int Compare(SudokuGame x, SudokuGame y)
        {
            return DateTime.Compare(x.DateCreated, y.DateCreated);
        }
        #endregion
    }
}