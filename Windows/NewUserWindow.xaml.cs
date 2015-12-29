/**
 * 
 * My Sudoku 3.1
 * By Joseph King
 * September 9, 2015
 * 
 * NewUserWindow.xaml.cs
 * 
 * The code behind for NewUserWindow.xaml.
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
    /// Interaction logic for NewUserWindow.xaml
    /// </summary>
    public partial class NewUserWindow : Window
    {
        /// <summary>
        /// This event is invoked when the user is successfuly created
        /// </summary>
        public event EventHandler<UserUpdateEventArgs> SuccessfullyCreatedUserEvent;

        /// <summary>
        /// This method invokes the SuccessfullyCreatedUserEvent
        /// </summary>
        /// <param name="e">User update event args</param>
        protected virtual void OnSuccessfulCreationOfUser(UserUpdateEventArgs e)
        {
            if (SuccessfullyCreatedUserEvent != null)
                SuccessfullyCreatedUserEvent(this, e);
        }

        public User user; // A reference to the game user
        public List<User> userList; // A reference to the saved user list
        public UserAction createUser; // The type of action passed to the new window through event args.

        /// <summary>
        /// The construtor for the new user window
        /// </summary>
        /// <param name="inComingUser">The user to be created</param>
        /// <param name="inComingUserList">The user list</param>
        public NewUserWindow(User inComingUser,  List<User> inComingUserList)
        {
            InitializeComponent();

            user = inComingUser; // Accept the user for processing            
            userList = inComingUserList; // Accept the user list for processing
        }

        /// <summary>
        /// Creates the user
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void btnCreateUser_Click(object sender, RoutedEventArgs e)
        {
            if (txtNewUserName.Text.IsNameValid())
            {
                User testUser = new User { UserName = txtNewUserName.Text, Password = pwNewUserPassword.Password, IsLoggedIn = false };

                if (testUser.IsUserContainedInUserList(userList) == false)
                {
                    if (pwNewUserPassword.Password == pwConfirmNewUserPassword.Password && pwNewUserPassword.Password.Length > 0)
                    {
                        if (txtNewUserName.Text.IsEachCharAlphaNumericOrBlank())
                        {
                            user = new User(txtNewUserName.Text, pwNewUserPassword.Password);
                            createUser = UserAction.CreateUser;
                            OnSuccessfulCreationOfUser(new UserUpdateEventArgs(createUser, user)); // Raise the successful creation of a user event
                            this.Close();
                        }
                        else
                        {
                            CustomMessageBox.ShowOK("The user name can only contain alphanumeric characters or blank spaces.  Please enter a valid name.", 
                                "String Must Be Alphanumeric or Blank Space", "Thanks, got it!", MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        if (pwNewUserPassword.Password.Length > 0)
                        {
                            CustomMessageBox.ShowOK("The password and the confirmation do not match.",
                                "Password is Not Confirmed", "Thanks, got it!", MessageBoxImage.Information);
                        }
                        else
                        {
                            CustomMessageBox.ShowOK("The password cannot be blank.", "Password Cannot Be Blank",
                                "Thanks, got it!", MessageBoxImage.Information);
                        }
                    }
                }
                else
                {
                    CustomMessageBox.ShowOK("There is already a user using that name, please pick a unique name.", 
                        "User name must be unique", "Thanks, got it!", MessageBoxImage.Information);
                }
            }
            else
            {
                CustomMessageBox.ShowOK("User name must be between 3 and 12 characters.  Please try again.", 
                    "User Name is Invalid", "Thanks, got it!", MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Closes the new user form
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void btnNewUserCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
