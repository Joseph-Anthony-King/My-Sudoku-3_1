/**
 * 
 * My Sudoku 3.1
 * By Joseph King
 * September 9, 2015
 * 
 * LoginUserWindow.xaml.cs
 * 
 * The code behind for LoginUserWindow.xaml. 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for LoginUserWindow.xaml
    /// </summary>
    public partial class LoginUserWindow : Window
    {
        /// <summary>
        /// This event is invoked when the user is successfuly logged in
        /// </summary>
        public event EventHandler<UserUpdateEventArgs> SuccessfullyLoginUserEvent;

        /// <summary>
        /// This method invokes the SuccessfullyLoginUserEvent
        /// </summary>
        /// <param name="e">User update event args</param>
        protected virtual void OnSuccessfulLoginOfUser(UserUpdateEventArgs e)
        {
            if (SuccessfullyLoginUserEvent != null)
                SuccessfullyLoginUserEvent(this, e);
        }

        public User user; // A reference to the game user
        public List<User> userList; // A reference to the user list
        public UserAction loginUser; // The type of window passed to the user event.

        /// <summary>
        /// The construtor for the login user window
        /// </summary>
        /// <param name="inComingUser">The user to be logged in</param>
        /// <param name="inComingUserList">The user list</param>
        public LoginUserWindow(User inComingUser, List<User> inComingUserList)
        {
            InitializeComponent();

            user = inComingUser;
            userList = inComingUserList;
        }

        /// <summary>
        /// Logs in the user
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void btnLoginUser_Click(object sender, RoutedEventArgs e)
        {
            if (txtLoginUserName.Text.Length > 0)
            {
                if (pwLoginUserPassword.Password.Length > 0)
                {
                    User testUser = new User { UserName = txtLoginUserName.Text, Password = pwLoginUserPassword.Password };

                    if (testUser.IsUserContainedInUserList(userList))
                    {
                        if (testUser.IsUserPasswordCorrect(userList))
                        {
                            user = userList.FirstOrDefault(u => u.UserName == testUser.UserName);
                            loginUser = UserAction.LoginUser;
                            OnSuccessfulLoginOfUser(new UserUpdateEventArgs(loginUser, user));
                            this.Close();
                        }
                        else
                        {
                            CustomMessageBox.ShowOK("The password you entered is incorrect.  Please try again.",
                                "Password was Incorrect", "Thanks, got it!", MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        MessageBoxResult result = CustomMessageBox.ShowYesNo("There is no user with that name saved to the user list." +
                            "  Would you like to create one?", "Create a New User?", "Yes, let's create one", 
                            "No, let me try again", MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            this.Close();
                        }
                        else
                        {

                        }
                    }
                }
                else 
                {
                    CustomMessageBox.ShowOK("Password cannot be blank.", "Password Cannot be Blank",
                        "Thanks, got it!", MessageBoxImage.Information);
                }
            }
            else
            {
                CustomMessageBox.ShowOK("User Name cannot be blank.", "User Name Cannot be Blank",
                    "Thanks, got it!", MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Closes the login form
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void btnLoginCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
