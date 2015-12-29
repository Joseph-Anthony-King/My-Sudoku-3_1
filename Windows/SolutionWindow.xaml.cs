/**
 * 
 * My Sudoku 3.1
 * By Joseph King
 * September 9, 2015
 * 
 * SolutionWindow.xaml.cs
 * 
 * This class allows the user to see the solution from their saved games.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MySudoku3_1
{
    /// <summary>
    /// Interaction logic for SolutionScreen.xaml
    /// </summary>
    public partial class SolutionWindow : Window
    {
        /// <summary>
        /// The constructor for the solution window
        /// </summary>
        /// <param name="game">The game with solution to be depicted</param>
        public SolutionWindow(SudokuGame game)
        {
            InitializeComponent();

            string user = game.UserName;

            user = user + ", You're Solution Was!";

            Title = user;

            // Create a list to hold the images.
            List<Image> lstVictorySolution = new List<Image>();

            #region Load the images into the list.
            lstVictorySolution.Add(imgVictorySolution01);
            lstVictorySolution.Add(imgVictorySolution02);
            lstVictorySolution.Add(imgVictorySolution03);
            lstVictorySolution.Add(imgVictorySolution04);
            lstVictorySolution.Add(imgVictorySolution05);
            lstVictorySolution.Add(imgVictorySolution06);
            lstVictorySolution.Add(imgVictorySolution07);
            lstVictorySolution.Add(imgVictorySolution08);
            lstVictorySolution.Add(imgVictorySolution09);
            lstVictorySolution.Add(imgVictorySolution10);
            lstVictorySolution.Add(imgVictorySolution11);
            lstVictorySolution.Add(imgVictorySolution12);
            lstVictorySolution.Add(imgVictorySolution13);
            lstVictorySolution.Add(imgVictorySolution14);
            lstVictorySolution.Add(imgVictorySolution15);
            lstVictorySolution.Add(imgVictorySolution16);
            lstVictorySolution.Add(imgVictorySolution17);
            lstVictorySolution.Add(imgVictorySolution18);
            lstVictorySolution.Add(imgVictorySolution19);
            lstVictorySolution.Add(imgVictorySolution20);
            lstVictorySolution.Add(imgVictorySolution21);
            lstVictorySolution.Add(imgVictorySolution22);
            lstVictorySolution.Add(imgVictorySolution23);
            lstVictorySolution.Add(imgVictorySolution24);
            lstVictorySolution.Add(imgVictorySolution25);
            lstVictorySolution.Add(imgVictorySolution26);
            lstVictorySolution.Add(imgVictorySolution27);
            lstVictorySolution.Add(imgVictorySolution28);
            lstVictorySolution.Add(imgVictorySolution29);
            lstVictorySolution.Add(imgVictorySolution30);
            lstVictorySolution.Add(imgVictorySolution31);
            lstVictorySolution.Add(imgVictorySolution32);
            lstVictorySolution.Add(imgVictorySolution33);
            lstVictorySolution.Add(imgVictorySolution34);
            lstVictorySolution.Add(imgVictorySolution35);
            lstVictorySolution.Add(imgVictorySolution36);
            lstVictorySolution.Add(imgVictorySolution37);
            lstVictorySolution.Add(imgVictorySolution38);
            lstVictorySolution.Add(imgVictorySolution39);
            lstVictorySolution.Add(imgVictorySolution40);
            lstVictorySolution.Add(imgVictorySolution41);
            lstVictorySolution.Add(imgVictorySolution42);
            lstVictorySolution.Add(imgVictorySolution43);
            lstVictorySolution.Add(imgVictorySolution44);
            lstVictorySolution.Add(imgVictorySolution45);
            lstVictorySolution.Add(imgVictorySolution46);
            lstVictorySolution.Add(imgVictorySolution47);
            lstVictorySolution.Add(imgVictorySolution48);
            lstVictorySolution.Add(imgVictorySolution49);
            lstVictorySolution.Add(imgVictorySolution50);
            lstVictorySolution.Add(imgVictorySolution51);
            lstVictorySolution.Add(imgVictorySolution52);
            lstVictorySolution.Add(imgVictorySolution53);
            lstVictorySolution.Add(imgVictorySolution54);
            lstVictorySolution.Add(imgVictorySolution55);
            lstVictorySolution.Add(imgVictorySolution56);
            lstVictorySolution.Add(imgVictorySolution57);
            lstVictorySolution.Add(imgVictorySolution58);
            lstVictorySolution.Add(imgVictorySolution59);
            lstVictorySolution.Add(imgVictorySolution60);
            lstVictorySolution.Add(imgVictorySolution61);
            lstVictorySolution.Add(imgVictorySolution62);
            lstVictorySolution.Add(imgVictorySolution63);
            lstVictorySolution.Add(imgVictorySolution64);
            lstVictorySolution.Add(imgVictorySolution65);
            lstVictorySolution.Add(imgVictorySolution66);
            lstVictorySolution.Add(imgVictorySolution67);
            lstVictorySolution.Add(imgVictorySolution68);
            lstVictorySolution.Add(imgVictorySolution69);
            lstVictorySolution.Add(imgVictorySolution70);
            lstVictorySolution.Add(imgVictorySolution71);
            lstVictorySolution.Add(imgVictorySolution72);
            lstVictorySolution.Add(imgVictorySolution73);
            lstVictorySolution.Add(imgVictorySolution74);
            lstVictorySolution.Add(imgVictorySolution75);
            lstVictorySolution.Add(imgVictorySolution76);
            lstVictorySolution.Add(imgVictorySolution77);
            lstVictorySolution.Add(imgVictorySolution78);
            lstVictorySolution.Add(imgVictorySolution79);
            lstVictorySolution.Add(imgVictorySolution80);
            lstVictorySolution.Add(imgVictorySolution81);
            #endregion

            // Load the images of 1 to 9.
            BitmapImage imgOne = new BitmapImage(new Uri("/MySudoku3_1;component/Images/1.gif", UriKind.Relative));
            BitmapImage imgTwo = new BitmapImage(new Uri("/MySudoku3_1;component/Images/2.gif", UriKind.Relative));
            BitmapImage imgThree = new BitmapImage(new Uri("/MySudoku3_1;component/Images/3.gif", UriKind.Relative));
            BitmapImage imgFour = new BitmapImage(new Uri("/MySudoku3_1;component/Images/4.gif", UriKind.Relative));
            BitmapImage imgFive = new BitmapImage(new Uri("/MySudoku3_1;component/Images/5.gif", UriKind.Relative));
            BitmapImage imgSix = new BitmapImage(new Uri("/MySudoku3_1;component/Images/6.gif", UriKind.Relative));
            BitmapImage imgSeven = new BitmapImage(new Uri("/MySudoku3_1;component/Images/7.gif", UriKind.Relative));
            BitmapImage imgEight = new BitmapImage(new Uri("/MySudoku3_1;component/Images/8.gif", UriKind.Relative));
            BitmapImage imgNine = new BitmapImage(new Uri("/MySudoku3_1;component/Images/9.gif", UriKind.Relative));
            BitmapImage imgOnePlatinum = new BitmapImage(new Uri("/MySudoku3_1;component/Images/1platinum.gif", UriKind.Relative));
            BitmapImage imgTwoPlatinum = new BitmapImage(new Uri("/MySudoku3_1;component/Images/2platinum.gif", UriKind.Relative));
            BitmapImage imgThreePlatinum = new BitmapImage(new Uri("/MySudoku3_1;component/Images/3platinum.gif", UriKind.Relative));
            BitmapImage imgFourPlatinum = new BitmapImage(new Uri("/MySudoku3_1;component/Images/4platinum.gif", UriKind.Relative));
            BitmapImage imgFivePlatinum = new BitmapImage(new Uri("/MySudoku3_1;component/Images/5platinum.gif", UriKind.Relative));
            BitmapImage imgSixPlatinum = new BitmapImage(new Uri("/MySudoku3_1;component/Images/6platinum.gif", UriKind.Relative));
            BitmapImage imgSevenPlatinum = new BitmapImage(new Uri("/MySudoku3_1;component/Images/7platinum.gif", UriKind.Relative));
            BitmapImage imgEightPlatinum = new BitmapImage(new Uri("/MySudoku3_1;component/Images/8platinum.gif", UriKind.Relative));
            BitmapImage imgNinePlatinum = new BitmapImage(new Uri("/MySudoku3_1;component/Images/9platinum.gif", UriKind.Relative));

            // Cycle through the victory image list and based on the solution string
            // the appropriate image will be chosen to display the solution matrix.
            for (int i = 0; i < 81; i++)
            {
                if (game.SolutionString[i].Equals('1'))
                {
                    if (game.ElementList[i].DisplayHint == true)
                    {
                        lstVictorySolution[i].Source = imgOnePlatinum;
                    }
                    else
                    {
                        lstVictorySolution[i].Source = imgOne;
                    }
                }
                else if (game.SolutionString[i].Equals('2'))
                {
                    if (game.ElementList[i].DisplayHint == true)
                    {
                        lstVictorySolution[i].Source = imgTwoPlatinum;
                    }
                    else
                    {
                        lstVictorySolution[i].Source = imgTwo;
                    }
                }
                else if (game.SolutionString[i].Equals('3'))
                {
                    if (game.ElementList[i].DisplayHint == true)
                    {
                        lstVictorySolution[i].Source = imgThreePlatinum;
                    }
                    else
                    {
                        lstVictorySolution[i].Source = imgThree;
                    }
                }
                else if (game.SolutionString[i].Equals('4'))
                {
                    if (game.ElementList[i].DisplayHint == true)
                    {
                        lstVictorySolution[i].Source = imgFourPlatinum;
                    }
                    else
                    {
                        lstVictorySolution[i].Source = imgFour;
                    }
                }
                else if (game.SolutionString[i].Equals('5'))
                {
                    if (game.ElementList[i].DisplayHint == true)
                    {
                        lstVictorySolution[i].Source = imgFivePlatinum;
                    }
                    else
                    {
                        lstVictorySolution[i].Source = imgFive;
                    }
                }
                else if (game.SolutionString[i].Equals('6'))
                {
                    if (game.ElementList[i].DisplayHint == true)
                    {
                        lstVictorySolution[i].Source = imgSixPlatinum;
                    }
                    else
                    {
                        lstVictorySolution[i].Source = imgSix;
                    }
                }
                else if (game.SolutionString[i].Equals('7'))
                {
                    if (game.ElementList[i].DisplayHint == true)
                    {
                        lstVictorySolution[i].Source = imgSevenPlatinum;
                    }
                    else
                    {
                        lstVictorySolution[i].Source = imgSeven;
                    }
                }
                else if (game.SolutionString[i].Equals('8'))
                {
                    if (game.ElementList[i].DisplayHint == true)
                    {
                        lstVictorySolution[i].Source = imgEightPlatinum;
                    }
                    else
                    {
                        lstVictorySolution[i].Source = imgEight;
                    }
                }
                else if (game.SolutionString[i].Equals('9'))
                {
                    if (game.ElementList[i].DisplayHint == true)
                    {
                        lstVictorySolution[i].Source = imgNinePlatinum;
                    }
                    else
                    {
                        lstVictorySolution[i].Source = imgNine;
                    }
                }
            }
        }

        /// <summary>
        /// Closes the solution window
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void btnSolutionReturn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
