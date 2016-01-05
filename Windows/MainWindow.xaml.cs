/**
 * 
 * My Sudoku 3.1
 * By Joseph King
 * September 9, 2015
 * 
 * MainWindow.xaml.cs
 * 
 * This class holds the game logic.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFCustomMessageBox;

namespace MySudoku3_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml, this contains the game logic.
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Music Enumeration
        /// <summary>
        /// This enumeration keeps trak of whether the music is off or on
        /// </summary>
        public enum Music { ON, OFF };
        #endregion

        #region Properties
        public SavedGameRepository SavedGameRepository { get; set; } // Declare the game repository
        public SavedUserRepository SavedUserRepository { get; set; } // Declare the user repository
        public List<SudokuGame> GameList { get; set; } // Delcare the game list
        public List<User> UserList { get; set; } // Declare the user list
        public GameSerializer GameSerializer { get; set; } // Declare an instance of the game serializer
        public UserSerializer UserSerializer { get; set; } // Declare an instance of the user serializer
        public SudokuGame Game { get; set; } // Declare the game variable
        public User User { get; set; } // Property to hold the user reference
        public List<TextBox> CellList { get; set; } // Declare the list of cells
        public Stopwatch GameStopWatch { get; set; } // Declare the gameStopWatch
        public Boolean CertainOfEntry { get; set; } // Declare the variable used to write black or slate gray
        public SoundPlayer MySudokuTheme { get; set; } // The game theme sound
        public Music GameMusicOnOrOff { get; set; } //Enumeration used to test if the music is on or off
        public List<Image> ListOfEvenNumberUserNameCharacters { get; set; } // Even number list of images
        public List<Image> ListOfOddNumberUserNameCharacters { get; set; } // Odd number list of images
        public List<Image> ListOfCreateGameUserNameCharacters { get; set; } // List of images to display user name when creating a game
        #endregion

        #region MainWindow(SoundPlayer theme)
        public MainWindow(SoundPlayer theme)
        {
            InitializeComponent();
            
            MySudokuTheme = theme;

            SavedGameRepository = new SavedGameRepository(); // Create an instance of the saved game repository
            SavedUserRepository = new SavedUserRepository(); // Create an instance of the saved user repository
            GameSerializer = new GameSerializer(); // Instantiate the game serializer
            UserSerializer = new UserSerializer(); // Instantiate the user serializer
            SavedGameRepository = GameSerializer.DeserializeRepository("SavedGames.dat"); // Download the saved games
            SavedUserRepository = UserSerializer.DeserializeRepository("SavedUsers.dat"); // Download the saved users
            GameList = SavedGameRepository.RepositorySavedGameList; // Load the saved games
            UserList = SavedUserRepository.RepositorySavedUserList; // Load the saved users

            CertainOfEntry = true; // Default the foreground color to black
            GameStopWatch = new Stopwatch(); // Initialize the gameStopWatch

            User = new User(); // Initialize the user

            ListOfEvenNumberUserNameCharacters = new List<Image>();

            ListOfEvenNumberUserNameCharacters.Add(imgEvenNumberUserName1);
            ListOfEvenNumberUserNameCharacters.Add(imgEvenNumberUserName2);
            ListOfEvenNumberUserNameCharacters.Add(imgEvenNumberUserName3);
            ListOfEvenNumberUserNameCharacters.Add(imgEvenNumberUserName4);
            ListOfEvenNumberUserNameCharacters.Add(imgEvenNumberUserName5);
            ListOfEvenNumberUserNameCharacters.Add(imgEvenNumberUserName6);
            ListOfEvenNumberUserNameCharacters.Add(imgEvenNumberUserName7);
            ListOfEvenNumberUserNameCharacters.Add(imgEvenNumberUserName8);
            ListOfEvenNumberUserNameCharacters.Add(imgEvenNumberUserName9);
            ListOfEvenNumberUserNameCharacters.Add(imgEvenNumberUserName10);
            ListOfEvenNumberUserNameCharacters.Add(imgEvenNumberUserName11);
            ListOfEvenNumberUserNameCharacters.Add(imgEvenNumberUserName12);

            ListOfOddNumberUserNameCharacters = new List<Image>();

            ListOfOddNumberUserNameCharacters.Add(imgOddNumberUserName1);
            ListOfOddNumberUserNameCharacters.Add(imgOddNumberUserName2);
            ListOfOddNumberUserNameCharacters.Add(imgOddNumberUserName3);
            ListOfOddNumberUserNameCharacters.Add(imgOddNumberUserName4);
            ListOfOddNumberUserNameCharacters.Add(imgOddNumberUserName5);
            ListOfOddNumberUserNameCharacters.Add(imgOddNumberUserName6);
            ListOfOddNumberUserNameCharacters.Add(imgOddNumberUserName7);
            ListOfOddNumberUserNameCharacters.Add(imgOddNumberUserName8);
            ListOfOddNumberUserNameCharacters.Add(imgOddNumberUserName9);
            ListOfOddNumberUserNameCharacters.Add(imgOddNumberUserName10);
            ListOfOddNumberUserNameCharacters.Add(imgOddNumberUserName11);

            ListOfCreateGameUserNameCharacters = new List<Image>();

            ListOfCreateGameUserNameCharacters.Add(imgCreateGameUserName1);
            ListOfCreateGameUserNameCharacters.Add(imgCreateGameUserName2);
            ListOfCreateGameUserNameCharacters.Add(imgCreateGameUserName3);
            ListOfCreateGameUserNameCharacters.Add(imgCreateGameUserName4);
            ListOfCreateGameUserNameCharacters.Add(imgCreateGameUserName5);
            ListOfCreateGameUserNameCharacters.Add(imgCreateGameUserName6);
            ListOfCreateGameUserNameCharacters.Add(imgCreateGameUserName7);
            ListOfCreateGameUserNameCharacters.Add(imgCreateGameUserName8);
            ListOfCreateGameUserNameCharacters.Add(imgCreateGameUserName9);
            ListOfCreateGameUserNameCharacters.Add(imgCreateGameUserName10);
            ListOfCreateGameUserNameCharacters.Add(imgCreateGameUserName11);
            ListOfCreateGameUserNameCharacters.Add(imgCreateGameUserName12);
            ListOfCreateGameUserNameCharacters.Add(imgCreateGameUserName13);

            GameMusicOnOrOff = Music.ON;
            User.Logout();
        }
        #endregion

        #region Main Menu Buttons

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void btnMMNewUser_Click(object sender, RoutedEventArgs e)
        {
            NewUserWindow newUserScreen = new NewUserWindow(User, UserList);
            newUserScreen.SuccessfullyCreatedUserEvent += new EventHandler<UserUpdateEventArgs>(HandleUserUpdateEvent);
            newUserScreen.ShowDialog();
        }

        /// <summary>
        /// Logs in the user
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void btnMMLoginUser_Click(object sender, RoutedEventArgs e)
        {
            LoginUserWindow loginUserScreen = new LoginUserWindow(User, UserList);
            loginUserScreen.SuccessfullyLoginUserEvent += new EventHandler<UserUpdateEventArgs>(HandleUserUpdateEvent);
            loginUserScreen.ShowDialog();
        }

        /// <summary>
        /// Logs out the user
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void btnUserLogoutUser_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = CustomMessageBox.ShowYesNo(User.UserName + ", are you sure you want to log out?", 
                "Are You Sure You Want to Logout?", "Yes, log me out", "No, keep me logged in", MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                User = new User { UserName = "", Password = "", IsLoggedIn = false };
                grdMainMenu.Visibility = Visibility.Visible;
                grdUserMenu.Visibility = Visibility.Hidden;
                stkMainMenu.Visibility = Visibility.Visible;
                stkUserMenu.Visibility = Visibility.Hidden;
                imgWelcomeBack.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Edits the user
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void btnUserEditUser_Click(object sender, RoutedEventArgs e)
        {
            EditUserWindow editUserScreen = new EditUserWindow(User, UserList);
            editUserScreen.SuccessfullyEditedUserEvent += new EventHandler<UserUpdateEventArgs>(HandleUserUpdateEvent);
            editUserScreen.ShowDialog();
        }
        
        /// <summary>
        /// Pressing the new game button from the main menu leads the user
        /// to the create new game screen.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void btnUserNewGame_Click(object sender, RoutedEventArgs e)
        {
            // Hide the Main Menu
            stkUserMenu.Visibility = Visibility.Hidden;
            grdUserMenu.Visibility = Visibility.Hidden;

            // Check LeapingLemur as the default difficulty level
            chkLeapingLemur.IsChecked = true;

            // Show the Create Game Screens
            stkSudokuGameScreenTitle.Visibility = Visibility.Visible;
            stkCreateGameMenu.Visibility = Visibility.Visible;
            grdCreateNewGame.Visibility = Visibility.Visible;

            FillCreateUserNameInCreateGameGrid();
        }

        /// <summary>
        /// Pressing the load game button from the main menu leads the user
        /// to the load saved game screen.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void btnUserLoadGame_Click(object sender, RoutedEventArgs e)
        {
            lstLoadSavedGame.ItemsSource = null;

            List<SudokuGame> UsersGameList = GameList.Where(g => g.UserName == User.UserName).OrderBy(g => g.UserName).ThenBy(g => g.DateCreated).ToList();

            lstLoadSavedGame.ItemsSource = UsersGameList;

            // Hide the Main Menu
            stkUserMenu.Visibility = Visibility.Hidden;
            grdUserMenu.Visibility = Visibility.Hidden;

            // Show the Load Game Screens
            stkSudokuGameScreenTitle.Visibility = Visibility.Visible;
            stkLoadGameMenu.Visibility = Visibility.Visible;
            grdLoadSavedGame.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Pressing the leaderboards button from the main menu allows the
        /// user to review the top scoring games.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void btnMMLeaderboards_Click(object sender, RoutedEventArgs e)
        {
            List<SudokuGame> TopTenGames = GameList.Where(n => n.Score != 0)
                .OrderByDescending(n => n.Score)
                .Take(10)
                .ToList();

            lstLeaderboards.ItemsSource = null;
            lstLeaderboards.ItemsSource = TopTenGames;

            // Set label to Top Ten Games
            stkTopTenGamesLabel.Visibility = Visibility.Visible;
            stkTopTenSteadySloths.Visibility = Visibility.Hidden;
            stkTopTenLeapingLemurs.Visibility = Visibility.Hidden;
            stkTopTenMountainlions.Visibility = Visibility.Hidden;

            // Show the Load Game Screens
            stkSudokuGameScreenTitle.Visibility = Visibility.Visible;
            stkLeaderboards.Visibility = Visibility.Visible;
            stkTopTenGamesLabel.Visibility = Visibility.Visible;
            grdLeaderboards.Visibility = Visibility.Visible;

            if (User.IsLoggedIn == false)
            {
                // Hide the Main Menu
                stkMainMenu.Visibility = Visibility.Hidden;
                grdMainMenu.Visibility = Visibility.Hidden;
            }
            else if (User.IsLoggedIn == true)
            {
                stkUserMenu.Visibility = Visibility.Hidden;
                grdUserMenu.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// The instructions screen is not implemented yet.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void btnMMInstructions_Click(object sender, RoutedEventArgs e)
        {
            // Hide the Main Menu
            stkMainMenu.Visibility = Visibility.Hidden;
            grdMainMenu.Visibility = Visibility.Hidden;

            // Start the instruction sequence
            grdInstructions.Visibility = Visibility.Visible;
            grdInstructions01.Visibility = Visibility.Visible;
            grdInstructions02.Visibility = Visibility.Hidden;
            grdInstructions03.Visibility = Visibility.Hidden;
            grdInstructions04.Visibility = Visibility.Hidden;
            grdInstructions05.Visibility = Visibility.Hidden;
            grdInstructions06.Visibility = Visibility.Hidden;
            grdInstructions07.Visibility = Visibility.Hidden;
            grdInstructions08.Visibility = Visibility.Hidden;
            grdInstructions09.Visibility = Visibility.Hidden;
            grdInstructions10.Visibility = Visibility.Hidden;
            grdInstructions11.Visibility = Visibility.Hidden;
            grdInstructions12.Visibility = Visibility.Hidden;
            grdInstructions13.Visibility = Visibility.Hidden;
            grdInstructions14.Visibility = Visibility.Hidden;
            grdInstructions15.Visibility = Visibility.Hidden;
            grdInstructions16.Visibility = Visibility.Hidden;
            grdInstructions17.Visibility = Visibility.Hidden;
            grdInstructions18.Visibility = Visibility.Hidden;
            grdInstructions19.Visibility = Visibility.Hidden;
            grdInstructions20.Visibility = Visibility.Hidden;
            grdInstructions21.Visibility = Visibility.Hidden;
            grdInstructions22.Visibility = Visibility.Hidden;
            grdInstructionButtons.Visibility = Visibility.Visible;

            // Set the content of the Back and Forward button.
            btnPrevious.Content = "Return";
            btnNext.Content = "Next";
        }

        /// <summary>
        /// If the music is on, turn it off.  If the music is off, turn it on.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void btnMMMusic_Click(object sender, RoutedEventArgs e)
        {
            if (GameMusicOnOrOff == Music.ON)
            {
                GameMusicOnOrOff = Music.OFF;
                MySudokuTheme.Stop();

                CustomMessageBox.ShowOK("Music is now off. \n\n:(", "Music is Now Off",
                    "Thanks, got it!", MessageBoxImage.Information);
            }
            else if (GameMusicOnOrOff == Music.OFF)
            {
                GameMusicOnOrOff = Music.ON;
                MySudokuTheme.PlayLooping();

                CustomMessageBox.ShowOK("Music is now on! \n\n:)", "Music is Now On!",
                    "Thanks, got it!", MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// To close the game from the main menu
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void btnMMExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = CustomMessageBox.ShowYesNo("Are you sure you want to exit My Sudoku?", "Exit My Sudoku?", 
                "Yes, exit My Sudoku", "No, I'm not done", MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
        #endregion

        #region Create Game Buttons
        /// <summary>
        /// The create game actions is not implemented yet.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void btnCGCreate_Click(object sender, RoutedEventArgs e)
        {

            // This enumeration is used to store the users difficulty level
            SudokuGame.Difficulty difficulty = new SudokuGame.Difficulty();

            // Set the difficulty level
            if (chkSteadySloth.IsChecked == true)
                difficulty = SudokuGame.Difficulty.EASY;
            else if (chkLeapingLemur.IsChecked == true)
                difficulty = SudokuGame.Difficulty.MEDIUM;
            else if (chkMightyMountainLion.IsChecked == true)
                difficulty = SudokuGame.Difficulty.HARD;
            else
                difficulty = SudokuGame.Difficulty.MEDIUM;

            // Create the new game.
            Game = new SudokuGame(User.UserName, difficulty);

            // instantiate and populate the cellList
            CellList = loadTextBoxesIntoList();

            // Clear the numbers is the cell list
            foreach (TextBox cell in CellList)
            {
                cell.Text = "";
            }

            // Set the values for the textBoxes in the cellList
            for (int i = 0; i < 81; i++)
            {
                if (Game.ElementList[i].DisplayHint == true)
                {
                    CellList[i].Text = Game.ElementList[i].Number.ToString();
                    CellList[i].IsReadOnly = true;
                    CellList[i].Foreground = Brushes.Red;
                }
                else
                {
                    CellList[i].IsReadOnly = false;
                    CellList[i].Foreground = Brushes.Black;
                }
            }

            // Hide the create game screens.
            stkCreateGameMenu.Visibility = Visibility.Hidden;
            grdCreateNewGame.Visibility = Visibility.Hidden;

            // Show the game screens.
            stkGameMenu.Visibility = Visibility.Visible;
            grdSudokuBoard.Visibility = Visibility.Visible;

            // Start the gameStopWatch
            GameStopWatch.Start();

            string user = Game.UserName;

            CustomMessageBox.ShowOK(User.UserName + ", your game is ready.  Good luck!",
                "Game Created!", "Thanks!");
        }

        /// <summary>
        /// The return button returns the user to the main menu.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void btnCGReturn_Click(object sender, RoutedEventArgs e)
        {
            // Hide the Create Game Screens
            stkSudokuGameScreenTitle.Visibility = Visibility.Hidden;
            stkCreateGameMenu.Visibility = Visibility.Hidden;
            grdCreateNewGame.Visibility = Visibility.Hidden;

            // Show the Main Menu
            grdUserMenu.Visibility = Visibility.Visible;
            stkUserMenu.Visibility = Visibility.Visible;
        }
        #endregion

        #region Load Game Buttons
        /// <summary>
        /// The load game action is not yet implemented.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void btnLGLoad_Click(object sender, RoutedEventArgs e)
        {
            // Load the selected item from gameList
            Game = (SudokuGame)lstLoadSavedGame.SelectedItem;

            if (Game != null)
            {
                MessageBoxResult result = CustomMessageBox.ShowYesNo(User.UserName + " are you sure you want to load this game?", 
                    "Load Saved Game?", "Yes, load my game", "No, don't load it", MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    if (Game.CompletedState == SudokuGame.Completed.No) // Only load a game if it is not completed
                    {
                        // instantiate and populate the cellList
                        CellList = loadTextBoxesIntoList();

                        // Clear the numbers is the cell list
                        foreach (TextBox cell in CellList)
                        {
                            cell.Text = "";
                        }

                        // Load the games cell list
                        for (int i = 0; i < CellList.Count; i++)
                        {
                            if (Game.ElementList[i].Number == 0)
                            {
                                CellList[i].Text = "";
                            }
                            else
                            {
                                CellList[i].Text = Game.ElementList[i].Number.ToString();
                            }

                            if (Game.ElementList[i].DisplayHint == true)
                            {
                                CellList[i].IsReadOnly = true;
                                CellList[i].Foreground = Brushes.Red;
                            }
                            else if (Game.ElementList[i].Certain == true)
                            {
                                CellList[i].IsReadOnly = false;
                                CellList[i].Foreground = Brushes.Black;
                            }
                            else if (Game.ElementList[i].Certain == false)
                            {
                                CellList[i].IsReadOnly = false;
                                CellList[i].Foreground = Brushes.SlateGray;
                            }
                        }

                        // Hide the Load Game Screens
                        stkLoadGameMenu.Visibility = Visibility.Hidden;
                        grdLoadSavedGame.Visibility = Visibility.Hidden;

                        // Show the game screens.
                        stkGameMenu.Visibility = Visibility.Visible;
                        grdSudokuBoard.Visibility = Visibility.Visible;

                        CertainOfEntry = true;

                        // Start the gameStopWatch
                        GameStopWatch.Start();

                        string user = Game.UserName;

                        CustomMessageBox.ShowOK(User.UserName + ", your game is ready.  Good luck!", "Game Loaded!", "Thanks!");
                    }
                    else
                    {
                        result = CustomMessageBox.ShowYesNo("This game is completed, would you like to see the solution?", "Load Saved Solution?", 
                            "Yes, let's see it!", "No, move on", MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            SolutionWindow solution = new SolutionWindow(Game);
                            solution.Show();
                        }
                    }
                }
            }
            else
            {
                CustomMessageBox.ShowOK("Sorry, no game is selected.", "No Game Selected!", "Damn, thanks!", MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// The delete game action is not yet implemented.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void btnLGDelete_Click(object sender, RoutedEventArgs e)
        {
            // Delete the selected item from gameList
            Game = (SudokuGame)lstLoadSavedGame.SelectedItem;

            if (Game != null)
            {
                // Confirm if the user wants to delete the game
                MessageBoxResult confirm = CustomMessageBox.ShowYesNo(User.UserName + ", are you sure you want to delete your game?", "Confirm Deletion", 
                    "Yes, delete my game", "No, don't delete my game", MessageBoxImage.Question);

                if (confirm == MessageBoxResult.Yes)
                {
                    // Load the selected item from gameList
                    Game = (SudokuGame)lstLoadSavedGame.SelectedItem;

                    // Delete the game from gameList
                    GameList.Remove(Game);

                    // Set savedGames equal to the gameList
                    SavedGameRepository.RepositorySavedGameList.Remove(Game);

                    // Save the game list to disk
                    GameSerializer.SerializeRepository("SavedGames.dat", SavedGameRepository);

                    // Clear thelstLoadSavedGame
                    lstLoadSavedGame.ItemsSource = null;

                    List<SudokuGame> UsersGameList = GameList.Where(g => g.UserName == User.UserName)
                        .OrderBy(g => g.UserName)
                        .ThenBy(g => g.DateCreated)
                        .ToList();

                    lstLoadSavedGame.ItemsSource = UsersGameList;

                    CustomMessageBox.ShowOK(User.UserName + ", your game has been deleted.", "Game Deleted!", "Thanks!");
                }
            }
            else
            {
                CustomMessageBox.ShowOK("Sorry, no game is selected.", "No Game Selected!", 
                    "Damn, thanks!", MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// The return button returns the user to the main menu.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void btnLGReturn_Click(object sender, RoutedEventArgs e)
        {
            // Hide the Load Game Screens
            stkSudokuGameScreenTitle.Visibility = Visibility.Hidden;
            stkLoadGameMenu.Visibility = Visibility.Hidden;
            grdLoadSavedGame.Visibility = Visibility.Hidden;

            // Show the Main Menu
            stkUserMenu.Visibility = Visibility.Visible;
            grdUserMenu.Visibility = Visibility.Visible;
        }
        #endregion

        #region Leaderboard Buttons
        /// <summary>
        /// The top ten list is not yet implemented.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void btnLBTopTen_Click(object sender, RoutedEventArgs e)
        {
            stkTopTenGamesLabel.Visibility = Visibility.Visible;
            stkTopTenSteadySloths.Visibility = Visibility.Hidden;
            stkTopTenLeapingLemurs.Visibility = Visibility.Hidden;
            stkTopTenMountainlions.Visibility = Visibility.Hidden;

            List<SudokuGame> TopTenGames = GameList.Where(n => n.CompletedState == SudokuGame.Completed.Yes)
                .OrderByDescending(n => n.Score)
                .Take(10)
                .ToList();

            lstLeaderboards.ItemsSource = null;
            lstLeaderboards.ItemsSource = TopTenGames;
        }

        /// <summary>
        /// The top ten sloths list is not yet implemented.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void btnLBSloths_Click(object sender, RoutedEventArgs e)
        {
            stkTopTenGamesLabel.Visibility = Visibility.Hidden;
            stkTopTenSteadySloths.Visibility = Visibility.Visible;
            stkTopTenLeapingLemurs.Visibility = Visibility.Hidden;
            stkTopTenMountainlions.Visibility = Visibility.Hidden;

            List<SudokuGame> TopTenSloths = GameList.Where(n => n.CompletedState == SudokuGame.Completed.Yes)
                .Where(n => n.GameDifficulty == SudokuGame.Difficulty.EASY)
                .OrderByDescending(n => n.Score)
                .Take(10)
                .ToList();

            lstLeaderboards.ItemsSource = null;
            lstLeaderboards.ItemsSource = TopTenSloths;
        }

        /// <summary>
        /// The top ten lemurs list is not yet implemented.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void btnLBTopLemurs_Click(object sender, RoutedEventArgs e)
        {
            stkTopTenGamesLabel.Visibility = Visibility.Hidden;
            stkTopTenSteadySloths.Visibility = Visibility.Hidden;
            stkTopTenLeapingLemurs.Visibility = Visibility.Visible;
            stkTopTenMountainlions.Visibility = Visibility.Hidden;

            List<SudokuGame> TopTenLemurs = GameList.Where(n => n.CompletedState == SudokuGame.Completed.Yes)
                .Where(n => n.GameDifficulty == SudokuGame.Difficulty.MEDIUM)
                .OrderByDescending(n => n.Score)
                .Take(10)
                .ToList();

            lstLeaderboards.ItemsSource = null;
            lstLeaderboards.ItemsSource = TopTenLemurs;
        }

        /// <summary>
        /// The top ten mountainlions list is not yet implemented.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void btnLBTopMountainlions_Click(object sender, RoutedEventArgs e)
        {
            stkTopTenGamesLabel.Visibility = Visibility.Hidden;
            stkTopTenSteadySloths.Visibility = Visibility.Hidden;
            stkTopTenLeapingLemurs.Visibility = Visibility.Hidden;
            stkTopTenMountainlions.Visibility = Visibility.Visible;

            List<SudokuGame> TopTenMountainLions = GameList.Where(n => n.CompletedState == SudokuGame.Completed.Yes)
                .Where(n => n.GameDifficulty == SudokuGame.Difficulty.HARD)
                .OrderByDescending(n => n.Score)
                .Take(10)
                .ToList();

            lstLeaderboards.ItemsSource = null;
            lstLeaderboards.ItemsSource = TopTenMountainLions;
        }

        /// <summary>
        /// The return button returns the user to the main menu.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void btnLBReturn_Click(object sender, RoutedEventArgs e)
        {
            // Hide the Load Game Screens
            stkSudokuGameScreenTitle.Visibility = Visibility.Hidden;
            stkLeaderboards.Visibility = Visibility.Hidden;
            stkTopTenGamesLabel.Visibility = Visibility.Hidden;
            grdLeaderboards.Visibility = Visibility.Hidden;

            if (User.IsLoggedIn == true)
            {
                // Show the user profile
                grdMainMenu.Visibility = Visibility.Hidden;
                stkMainMenu.Visibility = Visibility.Hidden;
                grdUserMenu.Visibility = Visibility.Visible;
                stkUserMenu.Visibility = Visibility.Visible;
                imgWelcome.Visibility = Visibility.Hidden;
                imgWelcomeBack.Visibility = Visibility.Visible;
            }
            else if (User.IsLoggedIn == false)
            {
                // Show the Main Menu
                stkMainMenu.Visibility = Visibility.Visible;
                grdMainMenu.Visibility = Visibility.Visible;
            }
        }
        #endregion

        #region Instruction Screen Buttons
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (grdInstructions01.Visibility == Visibility.Visible)
            {
                // Hide the instruction screen
                grdInstructions.Visibility = Visibility.Hidden;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Hidden;

                btnPrevious.Content = "Previous";

                // Show the Main Menu
                stkMainMenu.Visibility = Visibility.Visible;
                grdMainMenu.Visibility = Visibility.Visible;

            }

            else if (grdInstructions02.Visibility == Visibility.Visible)
            {
                // Go back to the previous instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Visible;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;

                // Set the content of the Back button to Return
                btnPrevious.Content = "Return";
            }

            else if (grdInstructions03.Visibility == Visibility.Visible)
            {
                // Go back to the previous instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Visible;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;
            }

            else if (grdInstructions04.Visibility == Visibility.Visible)
            {
                // Go back to the previous instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Visible;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;
            }

            else if (grdInstructions05.Visibility == Visibility.Visible)
            {
                // Go back to the previous instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Visible;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;
            }

            else if (grdInstructions06.Visibility == Visibility.Visible)
            {
                // Go back to the previous instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Visible;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;
            }

            else if (grdInstructions07.Visibility == Visibility.Visible)
            {
                // Go back to the previous instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Visible;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;
            }

            else if (grdInstructions08.Visibility == Visibility.Visible)
            {
                // Go back to the previous instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Visible;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;

            }

            else if (grdInstructions09.Visibility == Visibility.Visible)
            {
                // Go back to the previous instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Visible;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;
            }

            else if (grdInstructions10.Visibility == Visibility.Visible)
            {
                // Go back to the previous instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Visible;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;
            }

            else if (grdInstructions11.Visibility == Visibility.Visible)
            {
                // Go back to the previous instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Visible;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;

            }

            else if (grdInstructions12.Visibility == Visibility.Visible)
            {
                // Go back to the previous instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Visible;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;
            }

            else if (grdInstructions13.Visibility == Visibility.Visible)
            {
                // Go back to the previous instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Visible;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;
            }

            else if (grdInstructions14.Visibility == Visibility.Visible)
            {
                // Go back to the previous instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Visible;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;
            }

            else if (grdInstructions15.Visibility == Visibility.Visible)
            {
                // Go back to the previous instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Visible;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;
            }

            else if (grdInstructions16.Visibility == Visibility.Visible)
            {
                // Go back to the previous instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Visible;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;
            }

            else if (grdInstructions17.Visibility == Visibility.Visible)
            {
                // Go back to the previous instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Visible;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;
            }

            else if (grdInstructions18.Visibility == Visibility.Visible)
            {
                // Go back to the previous instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Visible;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;
            }

            else if (grdInstructions19.Visibility == Visibility.Visible)
            {
                // Go back to the previous instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Visible;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;
            }

            else if (grdInstructions20.Visibility == Visibility.Visible)
            {
                // Go back to the previous instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Visible;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;
            }

            else if (grdInstructions21.Visibility == Visibility.Visible)
            {
                // Go back to the previous instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Visible;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;
            }

            else if (grdInstructions22.Visibility == Visibility.Visible)
            {
                // Go back to the previous instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Visible;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;

                btnNext.Content = "Next";
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (grdInstructions01.Visibility == Visibility.Visible)
            {
                // Advance to the next instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Visible;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;

                // Set the content of the Back button to Return
                btnPrevious.Content = "Previous";
            }

            else if (grdInstructions02.Visibility == Visibility.Visible)
            {
                // Advance to the next instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Visible;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;

            }

            else if (grdInstructions03.Visibility == Visibility.Visible)
            {
                // Advance to the next instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Visible;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;
            }

            else if (grdInstructions04.Visibility == Visibility.Visible)
            {
                // Advance to the next instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Visible;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;
            }

            else if (grdInstructions05.Visibility == Visibility.Visible)
            {
                // Advance to the next instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Visible;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;

            }

            else if (grdInstructions06.Visibility == Visibility.Visible)
            {
                // Advance to the next instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Visible;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;
            }

            else if (grdInstructions07.Visibility == Visibility.Visible)
            {
                // Advance to the next instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Visible;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;
            }

            else if (grdInstructions08.Visibility == Visibility.Visible)
            {
                // Advance to the next instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Visible;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;
            }

            else if (grdInstructions09.Visibility == Visibility.Visible)
            {
                // Advance to the next instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Visible;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;
            }

            else if (grdInstructions10.Visibility == Visibility.Visible)
            {
                // Advance to the next instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Visible;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;
            }

            else if (grdInstructions11.Visibility == Visibility.Visible)
            {
                // Advance to the next instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Visible;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;
            }

            else if (grdInstructions12.Visibility == Visibility.Visible)
            {
                // Advance to the next instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Visible;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;
            }

            else if (grdInstructions13.Visibility == Visibility.Visible)
            {
                // Advance to the next instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Visible;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;
            }

            else if (grdInstructions14.Visibility == Visibility.Visible)
            {
                // Advance to the next instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Visible;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;
            }

            else if (grdInstructions15.Visibility == Visibility.Visible)
            {
                // Advance to the next instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Visible;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;
            }

            else if (grdInstructions16.Visibility == Visibility.Visible)
            {
                // Advance to the next instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Visible;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;
            }

            else if (grdInstructions17.Visibility == Visibility.Visible)
            {
                // Advance to the next instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Visible;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;
            }

            else if (grdInstructions18.Visibility == Visibility.Visible)
            {
                // Advance to the next instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Visible;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;
            }

            else if (grdInstructions19.Visibility == Visibility.Visible)
            {
                // Advance to the next instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Visible;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;
            }

            else if (grdInstructions20.Visibility == Visibility.Visible)
            {
                // Advance to the next instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Visible;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Visible;

            }

            else if (grdInstructions21.Visibility == Visibility.Visible)
            {
                // Advance to the next instruction screen
                grdInstructions.Visibility = Visibility.Visible;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Visible;
                grdInstructionButtons.Visibility = Visibility.Visible;

                btnNext.Content = "Return";
            }

            else if (grdInstructions22.Visibility == Visibility.Visible)
            {
                // Hide the instruction screen
                grdInstructions.Visibility = Visibility.Hidden;
                grdInstructions01.Visibility = Visibility.Hidden;
                grdInstructions02.Visibility = Visibility.Hidden;
                grdInstructions03.Visibility = Visibility.Hidden;
                grdInstructions04.Visibility = Visibility.Hidden;
                grdInstructions05.Visibility = Visibility.Hidden;
                grdInstructions06.Visibility = Visibility.Hidden;
                grdInstructions07.Visibility = Visibility.Hidden;
                grdInstructions08.Visibility = Visibility.Hidden;
                grdInstructions09.Visibility = Visibility.Hidden;
                grdInstructions10.Visibility = Visibility.Hidden;
                grdInstructions11.Visibility = Visibility.Hidden;
                grdInstructions12.Visibility = Visibility.Hidden;
                grdInstructions13.Visibility = Visibility.Hidden;
                grdInstructions14.Visibility = Visibility.Hidden;
                grdInstructions15.Visibility = Visibility.Hidden;
                grdInstructions16.Visibility = Visibility.Hidden;
                grdInstructions17.Visibility = Visibility.Hidden;
                grdInstructions18.Visibility = Visibility.Hidden;
                grdInstructions19.Visibility = Visibility.Hidden;
                grdInstructions20.Visibility = Visibility.Hidden;
                grdInstructions21.Visibility = Visibility.Hidden;
                grdInstructions22.Visibility = Visibility.Hidden;
                grdInstructionButtons.Visibility = Visibility.Hidden;

                btnNext.Content = "Next";

                // Show the Main Menu
                stkMainMenu.Visibility = Visibility.Visible;
                grdMainMenu.Visibility = Visibility.Visible;

            }

        }
        #endregion

        #region Difficulty Level Checkboxes from the Create Game Screen
        /// <summary>
        /// Checking the SteadySloth check box ensures LeapingLemur
        /// and MightyMountainLion are unchecked.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void chkSteadySloth_Checked(object sender, RoutedEventArgs e)
        {
            chkLeapingLemur.IsChecked = false;
            chkMightyMountainLion.IsChecked = false;
        }

        /// <summary>
        /// Click on the SteadySlothText to check steady sloth.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void imgSteadySlothText_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chkSteadySloth.IsChecked = true;
        }

        /// <summary>
        /// Click on the SteadySlothImage to check steady sloth.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void imgSteadySloth_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chkSteadySloth.IsChecked = true;
        }

        /// <summary>
        /// Checking the LeapingLemur check box ensures SteadySloth
        /// and MightyMountainLion are unchecked
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void chkLeapingLemur_Checked(object sender, RoutedEventArgs e)
        {
            chkSteadySloth.IsChecked = false;
            chkMightyMountainLion.IsChecked = false;
        }

        /// <summary>
        /// Click on the LeapingLemurText to check leaping lemur.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void imgLeapingLemurText_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chkLeapingLemur.IsChecked = true;
        }

        /// <summary>
        /// Click on the LeapingLemurImage to check leaping lemur.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void imgLeapingLemur_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chkLeapingLemur.IsChecked = true;
        }

        /// <summary>
        /// Checking the MightyMountainLion check box ensures SteadySloth
        /// and LeapingLemur are unchecked.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void chkMightyMountainLion_Checked(object sender, RoutedEventArgs e)
        {
            chkSteadySloth.IsChecked = false;
            chkLeapingLemur.IsChecked = false;
        }

        /// <summary>
        /// Click on the MightyMountainLionText to check mighty mountainlion.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void imgMightyMountainLionText_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chkMightyMountainLion.IsChecked = true;
        }

        /// <summary>
        /// Click on the MightyMountainLionImage to check mighty mountainlion.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void imgMightyMountainLion_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chkMightyMountainLion.IsChecked = true;
        }
        #endregion

        #region Game Screen Buttons
        /// <summary>
        /// The certain button sets the certain boolean to true, thus setting
        /// the cellList textbox foreground color to black.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void btnGSCertain_Click(object sender, RoutedEventArgs e)
        {
            CertainOfEntry = true;
        }

        /// <summary>
        /// The uncertain button sets the certain boolean to false, thus setting
        /// the cellList textbox foreground color to slate gray.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void btnGSUncertain_Click(object sender, RoutedEventArgs e)
        {
            CertainOfEntry = false;
        }

        /// <summary>
        /// The clear all button removes the users entries, thus resetting
        /// the board.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void btnGSClearAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (TextBox cell in CellList)
            {
                if (cell.IsReadOnly == false)
                {
                    cell.Text = "";
                }
            }
        }

        /// <summary>
        /// The check button determines is the player wins or not.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void btnGSCheck_Click(object sender, RoutedEventArgs e)
        {
            // Stop the gameStopWatch
            GameStopWatch.Stop();

            // Calculate the number of Ticks
            Game.Ticks = Game.Ticks + GameStopWatch.Elapsed.Seconds;

            // Reset the gameStopWatch
            GameStopWatch.Reset();

            // Reset the game.ElementSB in order to evaluate the users entries
            Game.UserEntriesSB.Clear();

            // Build the UserEntriesSB to evaluate the claimant's responses
            foreach (TextBox element in CellList)
            {
                if (element.Text == "")
                {
                    Game.UserEntriesSB.Append(" ");
                }
                else
                {
                    Game.UserEntriesSB.Append(element.Text);
                }
            }

            // Make a record of the user's confidence in their answers.
            for (int i = 0; i < Game.UserEntriesSB.Length; i++)
            {
                if (CellList[i].Foreground == Brushes.SlateGray)
                {
                    if (CellList[i].Text != "")
                    {
                        int cellNumber = Int32.Parse(CellList[i].Text);
                        cellNumber = Game.ElementList[i].Number;
                        Game.ElementList[i].Certain = false;
                    }
                    else
                    {
                        Game.ElementList[i].Number = 0;
                        Game.ElementList[i].Certain = true;
                    }
                }
                else
                {
                    if (CellList[i].Text != "")
                    {
                        int cellNumber = Int32.Parse(CellList[i].Text);
                        cellNumber = Game.ElementList[i].Number;
                        Game.ElementList[i].Certain = true;
                    }
                    else
                    {
                        Game.ElementList[i].Number = 0;
                        Game.ElementList[i].Certain = true;
                    }
                }
            }

            if (Game.SolutionString.Equals(Game.UserEntriesSB.ToString())) // If the player wins...
            {
                // Calculate the score
                Game.Score = Game.CalculateSolution();

                // Complete the game
                Game.CompletedState = SudokuGame.Completed.Yes;

                // Set the date completed
                Game.DateCompleted = DateTime.Now;

                // Determine if the game was previously saved
                if (SavedGameRepository.RepositorySavedGameList.Contains(Game))
                {
                    // If so, remove the old copy
                    SavedGameRepository.RepositorySavedGameList.Remove(Game);
                }

                // Save the completed game
                SavedGameRepository.RepositorySavedGameList.Add(Game);

                // Update the game list
                GameList = SavedGameRepository.RepositorySavedGameList;

                // Save the game list to disk
                GameSerializer.SerializeRepository("SavedGames.dat", SavedGameRepository);

                // Stop mySudokuTheme
                MySudokuTheme.Stop();

                // Initiate and show the victory screen
                VictoryWindow victory = new VictoryWindow(Game);
                victory.Show();

                // Close the main window
                this.Close();
            }
            else // if the player does not win...
            {
                if (Game.GameDifficulty == SudokuGame.Difficulty.EASY || Game.GameDifficulty == SudokuGame.Difficulty.MEDIUM)
                {
                    CustomMessageBox.ShowOK("Close but no cigar, I'll show you where you're right!", 
                        "Time for a Hint", "Cool, let's see it!");

                    if (Game.GameDifficulty == SudokuGame.Difficulty.EASY)
                    {
                        // Turns the user's solution to a string
                        string _assistanceString = Game.UserEntriesSB.ToString();

                        // Turns the solution to a char array
                        char[] _solutionArray = Game.SolutionString.ToCharArray();

                        // Turns the user's solution to a char array
                        char[] _assistanceArray = _assistanceString.ToCharArray();

                        // If the value in the user's char array is incorrect its converted to ""
                        for (int i = 0; i < _assistanceString.Length; i++)
                        {
                            if (_solutionArray[i] != _assistanceArray[i])
                            {
                                if (CellList[i].IsReadOnly == false)
                                {
                                    CellList[i].Text = "";
                                }
                            }
                        }

                        // Add 200 Demerits
                        Game.Demerits = Game.Demerits + 200;
                    }
                    else if (Game.GameDifficulty == SudokuGame.Difficulty.MEDIUM)
                    {
                        int _correctAnswers = 0;

                        for (int i = 0; i < Game.SolutionString.Length; i++)
                        {
                            if (Game.SolutionString.ElementAt(i) == Game.UserEntriesSB.ToString().ElementAt(i))
                            {
                                if (CellList[i].IsReadOnly == false)
                                {
                                    _correctAnswers++;
                                }
                            }
                        }

                        string correctSring = _correctAnswers + " of your answers are correct!";

                        CustomMessageBox.ShowOK(correctSring, _correctAnswers + " Answers", "Cool, thanks!");

                        // Add 100 Demerits
                        Game.Demerits = Game.Demerits + 100;
                    }
                }
                else if (Game.GameDifficulty == SudokuGame.Difficulty.HARD)
                {
                    CustomMessageBox.ShowOK("Close but no cigar, try again!",
                        "Try again!", "Alright, got it");
                }

                GameStopWatch.Start();
            }
        }

        /// <summary>
        /// The save button saves the game.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void btnGSSave_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = CustomMessageBox.ShowYesNo(User.UserName + " are you sure you want to save this game?", "Save Game?", 
                "Yes, save it", "No, don't save it", MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Stop the gameStopWatch
                GameStopWatch.Stop();

                // Record the number of ticks
                Game.Ticks = Game.Ticks + GameStopWatch.Elapsed.Seconds;

                // Build the UserEntriesSB to evaluate the claimant's responses
                foreach (TextBox element in CellList)
                {
                    if (element.Text == "")
                    {
                        Game.UserEntriesSB.Append(" ");
                    }
                    else
                    {
                        Game.UserEntriesSB.Append(element.Text);
                    }
                }

                // Make a record of the user's confidence in their answers.
                for (int i = 0; i < CellList.Count; i++)
                {
                    if (CellList[i].Foreground == Brushes.SlateGray)
                    {
                        if (CellList[i].Text == "")
                        {
                            Game.ElementList[i].Number = 0;
                            Game.ElementList[i].Certain = true;
                        }
                        else
                        {
                            int cellNumber = Int32.Parse(CellList[i].Text);
                            Game.ElementList[i].Number = cellNumber;
                            Game.ElementList[i].Certain = false;
                        }
                    }
                    else
                    {
                        if (CellList[i].Text != "")
                        {
                            int cellNumber = Int32.Parse(CellList[i].Text);
                            Game.ElementList[i].Number = cellNumber;
                            Game.ElementList[i].Certain = true;
                        }
                        else
                        {
                            Game.ElementList[i].Number = 0;
                            Game.ElementList[i].Certain = true;
                        }
                    }
                }

                // Remove the game if previously saved
                if (SavedGameRepository.RepositorySavedGameList.Contains(Game))
                {
                    SavedGameRepository.RepositorySavedGameList.Remove(Game);
                }

                // Save the game
                SavedGameRepository.RepositorySavedGameList.Add(Game);

                // Add the saved game to SavedGames.dat
                GameSerializer.SerializeRepository("SavedGames.dat", SavedGameRepository);

                // Update the game list
                GameList = SavedGameRepository.RepositorySavedGameList;

                string user = User.UserName + ", your game has been saved!";

                // Reset the gameStopWatch
                GameStopWatch.Reset();

                // Restart the gameStopWatch
                GameStopWatch.Start();

                CustomMessageBox.ShowOK(user, "Game Saved!", "Thanks!");
            }
        }

        /// <summary>
        /// If the music is on, turn it off.  If the music is off, turn it on.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void btnGSMusic_Click(object sender, RoutedEventArgs e)
        {
            if (GameMusicOnOrOff == Music.ON)
            {
                GameMusicOnOrOff = Music.OFF;
                MySudokuTheme.Stop();

                CustomMessageBox.ShowOK("Music is now off. \n\n:(", "Music is Now Off",
                    "Thanks, got it!", MessageBoxImage.Information);
            }
            else if (GameMusicOnOrOff == Music.OFF)
            {
                GameMusicOnOrOff = Music.ON;
                MySudokuTheme.PlayLooping();

                CustomMessageBox.ShowOK("Music is now on! \n\n:)", "Music is Now On!",
                    "Thanks, got it!", MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Returns the user to the main menu.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void btnGSReturn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = CustomMessageBox.ShowYesNo(User.UserName + " all unsaved changes will be lost, do you wish to proceed?", 
                "Return to Main Menu?", "Yes, return to main menu", "No, let's keep playing", MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Stop the gameStopWatch
                GameStopWatch.Stop();

                // Reset the gameStopWatch
                GameStopWatch.Reset();

                // Hide the game screens
                stkSudokuGameScreenTitle.Visibility = Visibility.Hidden;
                stkGameMenu.Visibility = Visibility.Hidden;
                grdSudokuBoard.Visibility = Visibility.Hidden;

                // Show the main menu screens
                stkUserMenu.Visibility = Visibility.Visible;
                grdUserMenu.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Allows the user to exit the game.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void btnGSExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = CustomMessageBox.ShowYesNo(User.UserName + " all unsaved changes will be lost, do you want to exit My Sudoku?", 
                "Exit My Sudoku?", "Yes, exit My Sudoku", "No, let's keep playing", MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Stop the gameStopWatch
                GameStopWatch.Stop();

                // Reset the gameStopWatch
                GameStopWatch.Reset();

                Application.Current.Shutdown();
            }
        }
        #endregion

        #region loadTextBoxesIntoList()
        /// <summary>
        /// This method is used to load the TextBoxes into the cellList
        /// </summary>
        /// <returns>List of TextBoxes</TextBoxes></returns>
        private List<TextBox> loadTextBoxesIntoList()
        {
            List<TextBox> textBoxList = new List<TextBox>();

            textBoxList.Add(txtCell0);
            textBoxList.Add(txtCell1);
            textBoxList.Add(txtCell2);
            textBoxList.Add(txtCell3);
            textBoxList.Add(txtCell4);
            textBoxList.Add(txtCell5);
            textBoxList.Add(txtCell6);
            textBoxList.Add(txtCell7);
            textBoxList.Add(txtCell8);
            textBoxList.Add(txtCell9);
            textBoxList.Add(txtCell10);
            textBoxList.Add(txtCell11);
            textBoxList.Add(txtCell12);
            textBoxList.Add(txtCell13);
            textBoxList.Add(txtCell14);
            textBoxList.Add(txtCell15);
            textBoxList.Add(txtCell16);
            textBoxList.Add(txtCell17);
            textBoxList.Add(txtCell18);
            textBoxList.Add(txtCell19);
            textBoxList.Add(txtCell20);
            textBoxList.Add(txtCell21);
            textBoxList.Add(txtCell22);
            textBoxList.Add(txtCell23);
            textBoxList.Add(txtCell24);
            textBoxList.Add(txtCell25);
            textBoxList.Add(txtCell26);
            textBoxList.Add(txtCell27);
            textBoxList.Add(txtCell28);
            textBoxList.Add(txtCell29);
            textBoxList.Add(txtCell30);
            textBoxList.Add(txtCell31);
            textBoxList.Add(txtCell32);
            textBoxList.Add(txtCell33);
            textBoxList.Add(txtCell34);
            textBoxList.Add(txtCell35);
            textBoxList.Add(txtCell36);
            textBoxList.Add(txtCell37);
            textBoxList.Add(txtCell38);
            textBoxList.Add(txtCell39);
            textBoxList.Add(txtCell40);
            textBoxList.Add(txtCell41);
            textBoxList.Add(txtCell42);
            textBoxList.Add(txtCell43);
            textBoxList.Add(txtCell44);
            textBoxList.Add(txtCell45);
            textBoxList.Add(txtCell46);
            textBoxList.Add(txtCell47);
            textBoxList.Add(txtCell48);
            textBoxList.Add(txtCell49);
            textBoxList.Add(txtCell50);
            textBoxList.Add(txtCell51);
            textBoxList.Add(txtCell52);
            textBoxList.Add(txtCell53);
            textBoxList.Add(txtCell54);
            textBoxList.Add(txtCell55);
            textBoxList.Add(txtCell56);
            textBoxList.Add(txtCell57);
            textBoxList.Add(txtCell58);
            textBoxList.Add(txtCell59);
            textBoxList.Add(txtCell60);
            textBoxList.Add(txtCell61);
            textBoxList.Add(txtCell62);
            textBoxList.Add(txtCell63);
            textBoxList.Add(txtCell64);
            textBoxList.Add(txtCell65);
            textBoxList.Add(txtCell66);
            textBoxList.Add(txtCell67);
            textBoxList.Add(txtCell68);
            textBoxList.Add(txtCell69);
            textBoxList.Add(txtCell70);
            textBoxList.Add(txtCell71);
            textBoxList.Add(txtCell72);
            textBoxList.Add(txtCell73);
            textBoxList.Add(txtCell74);
            textBoxList.Add(txtCell75);
            textBoxList.Add(txtCell76);
            textBoxList.Add(txtCell77);
            textBoxList.Add(txtCell78);
            textBoxList.Add(txtCell79);
            textBoxList.Add(txtCell80);

            return textBoxList;
        }
        #endregion

        #region Game Screen Events
        /// <summary>
        /// When the certain field indicates true the foreground color is set to black.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void txtCell_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            SudokuUtilities.CellIsCertain(ref sender, CertainOfEntry);
        }

        /// <summary>
        /// When the certain field indicates false the foreground color is set to slate gray.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void txtCell_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            SudokuUtilities.CellIsValid(ref sender);
        }
        #endregion

        #region HandleUserUpdateEvent(object sender, UserUpdateEventArgs e)        
        /// <summary>
        /// This event handler updates the user when invoked
        /// </summary>
        /// <param name="sender">The object sender</param>
        /// <param name="e">The user update event args</param>
        private void HandleUserUpdateEvent(object sender, UserUpdateEventArgs e)
        {
            if (e.UserAction == UserAction.LoginUser) // Login User
            {
                User = e.User;
                User.Login();
                grdMainMenu.Visibility = Visibility.Hidden;
                grdUserMenu.Visibility = Visibility.Visible;
                stkMainMenu.Visibility = Visibility.Hidden;
                stkUserMenu.Visibility = Visibility.Visible;
                imgWelcome.Visibility = Visibility.Hidden;
                imgWelcomeBack.Visibility = Visibility.Visible;

                FillUserNameStackInUserGrid();
            }
            else if (e.UserAction == UserAction.CreateUser) // Create User
            {
                User = e.User;
                User.Login();
                UserList.Add(User);
                SavedUserRepository.RepositorySavedUserList = UserList;
                UserSerializer.SerializeRepository("SavedUsers.dat", SavedUserRepository);
                grdMainMenu.Visibility = Visibility.Hidden;
                grdUserMenu.Visibility = Visibility.Visible;
                stkMainMenu.Visibility = Visibility.Hidden;
                stkUserMenu.Visibility = Visibility.Visible;
                imgWelcome.Visibility = Visibility.Visible;
                imgWelcomeBack.Visibility = Visibility.Hidden;

                FillUserNameStackInUserGrid();
            }
            else if (e.UserAction == UserAction.EditUser) // Edit User
            {
                if (GameList.Count > 0)
                {
                    foreach (SudokuGame game in GameList)
                    {
                        if (game.UserName == e.User.UserName)
                        {
                            game.UserName = e.User.UserName;
                        }
                    }
                }

                SavedGameRepository.RepositorySavedGameList = GameList;
                GameSerializer.SerializeRepository("SavedGames.dat", SavedGameRepository);

                if (SavedUserRepository.RepositorySavedUserList.Contains(User))
                {
                    SavedUserRepository.RepositorySavedUserList.Remove(User);
                }

                User = e.User;
                UserList.Add(User);
                SavedUserRepository.RepositorySavedUserList = UserList;
                UserSerializer.SerializeRepository("SavedUsers.dat", SavedUserRepository);
                grdMainMenu.Visibility = Visibility.Hidden;
                grdUserMenu.Visibility = Visibility.Visible;
                stkMainMenu.Visibility = Visibility.Hidden;
                stkUserMenu.Visibility = Visibility.Visible;
                imgWelcome.Visibility = Visibility.Visible;

                imgWelcome.Visibility = Visibility.Hidden;
                imgWelcomeBack.Visibility = Visibility.Visible;

                FillUserNameStackInUserGrid();
            }
            else if (e.UserAction == UserAction.DeleteUser) // Delete User
            {
                if (GameList.Count > 0)
                {
                    foreach (SudokuGame game in GameList.ToList())
                    {
                        if (game.UserName == e.User.UserName)
                        {
                            GameList.Remove(game);
                        }
                    }
                }

                SavedGameRepository.RepositorySavedGameList = GameList;
                GameSerializer.SerializeRepository("SavedGames.dat", SavedGameRepository);

                UserList.Remove(User);
                SavedUserRepository.RepositorySavedUserList = UserList;
                UserSerializer.SerializeRepository("SavedUsers.dat", SavedUserRepository);

                User = new User();

                imgWelcome.Visibility = Visibility.Hidden;
                imgWelcomeBack.Visibility = Visibility.Hidden;

                grdMainMenu.Visibility = Visibility.Visible;
                grdUserMenu.Visibility = Visibility.Hidden;
                stkMainMenu.Visibility = Visibility.Visible;
                stkUserMenu.Visibility = Visibility.Hidden;
            }
        }

        #endregion

        #region FillUserNameStackInUserGrid()
        /// <summary>
        /// Fills in the user name in the user grid with 
        /// images based on the user name
        /// </summary>
        private void FillUserNameStackInUserGrid()
        {
            foreach (Image image in ListOfEvenNumberUserNameCharacters)
            {
                image.Source = (ImageSource)FindResource("blank");
            }

            foreach (Image image in ListOfOddNumberUserNameCharacters)
            {
                image.Source = (ImageSource)FindResource("blank");
            }

            string userName = User.UserName.ToUpper();
            
            int i = 0;
            
            if ( User.UserName.Length % 2 == 0 )
            {
                stkOddNumberUserName.Visibility = Visibility.Hidden;

                if (User.UserName.Length == 4)
                {
                    i = 4;
                }
                else if (User.UserName.Length == 6)
                {
                    i = 3;
                }
                else if (User.UserName.Length == 8)
                {
                    i = 2;
                }
                else if (User.UserName.Length == 10)
                {
                    i = 1;
                }
                else if (User.UserName.Length == 12)
                {
                    i = 0;
                }

                foreach (char c in userName)
                {
                    string s = c.ToString();

                    if (s == " ")
                        s = "blank";
                    else if (s == ",")
                        s = "comma";
                    else if (s == "0")
                        s = "zero";
                    else if (s == "1")
                        s = "one";
                    else if (s == "2")
                        s = "two";
                    else if (s == "3")
                        s = "three";
                    else if (s == "4")
                        s = "four";
                    else if (s == "5")
                        s = "five";
                    else if (s == "6")
                        s = "six";
                    else if (s == "7")
                        s = "seven";
                    else if (s == "8")
                        s = "eight";
                    else if (s == "9")
                        s = "nine";

                    ListOfEvenNumberUserNameCharacters[i].Source = (ImageSource)FindResource(s);

                    i++;
                }
                
                stkEvenNumberUserName.Visibility = Visibility.Visible;
            }
            else
            {
                stkEvenNumberUserName.Visibility = Visibility.Hidden;

                if (User.UserName.Length == 3)
                {
                    i = 4;
                }
                else if (User.UserName.Length == 5)
                {
                    i = 3;
                }
                else if (User.UserName.Length == 7)
                {
                    i = 2;
                }
                else if (User.UserName.Length == 9)
                {
                    i = 1;
                }
                else if (User.UserName.Length == 11)
                {
                    i = 0;
                }

                foreach (char c in userName)
                {
                    string s = c.ToString();

                    if (s == " ")
                        s = "blank";
                    else if (s == ",")
                        s = "comma";
                    else if (s == "0")
                        s = "zero";
                    else if (s == "1")
                        s = "one";
                    else if (s == "2")
                        s = "two";
                    else if (s == "3")
                        s = "three";
                    else if (s == "4")
                        s = "four";
                    else if (s == "5")
                        s = "five";
                    else if (s == "6")
                        s = "six";
                    else if (s == "7")
                        s = "seven";
                    else if (s == "8")
                        s = "eight";
                    else if (s == "9")
                        s = "nine";

                    ListOfOddNumberUserNameCharacters[i].Source = (ImageSource)FindResource(s);

                    i++;
                }
                
                stkOddNumberUserName.Visibility = Visibility.Visible;
            }
        }

        #endregion

        #region FillCreateUserNameInCreateGameGrid()
        /// <summary>
        /// Fills in the user name in the create game 
        /// grid with images based on the user name
        /// </summary>
        private void FillCreateUserNameInCreateGameGrid()
        {
            foreach (Image image in ListOfCreateGameUserNameCharacters)
            {
                image.Source = (ImageSource)FindResource("blank");
            }

            string userName = User.UserName.ToUpper();

            int i = 0;

            foreach (char c in userName)
            {
                string s = c.ToString();

                if (s == " ")
                    s = "blank";
                else if (s == ",")
                    s = "comma";
                else if (s == "0")
                    s = "zero";
                else if (s == "1")
                    s = "one";
                else if (s == "2")
                    s = "two";
                else if (s == "3")
                    s = "three";
                else if (s == "4")
                    s = "four";
                else if (s == "5")
                    s = "five";
                else if (s == "6")
                    s = "six";
                else if (s == "7")
                    s = "seven";
                else if (s == "8")
                    s = "eight";
                else if (s == "9")
                    s = "nine";

                ListOfCreateGameUserNameCharacters[i].Source = (ImageSource)FindResource(s);

                i++;
            }
                        
            stkEvenNumberUserName.Visibility = Visibility.Visible;

        }

        #endregion

    }
}