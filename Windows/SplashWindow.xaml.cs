/**
 * 
 * My Sudoku 3.1
 * By Joseph King
 * September 9, 2015
 * 
 * SplashWindow.xaml.cs
 * 
 * This class is the entry point for the game.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MySudoku3_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SplashWindow : Window
    {
        /// <summary>
        /// The SudokuGameTheme music
        /// </summary>
        public SoundPlayer mySudokuTheme;

        /// <summary>
        /// The constructor
        /// </summary>
        public SplashWindow()
        {
            InitializeComponent();

            // Instantiate the SudokuGameTheme
            mySudokuTheme = new SoundPlayer();

            // Set the song location
            mySudokuTheme.Stream = Properties.Resources.Nine_to_Nine;

            // Play the MySudokuTheme
            mySudokuTheme.PlayLooping();
        }

        /// <summary>
        /// Closes the splash screen and shows the main window
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void imgClickHere_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (imgClickHere.Opacity == 1.0)
            {
                MainWindow mainWindow = new MainWindow(mySudokuTheme);
                this.Hide();
                mainWindow.Show();
            }
        }
    }
}