/**
 * 
 * My Sudoku 3.1
 * By Joseph King
 * September 9, 2015
 * 
 * EditUserWindow.xaml.cs
 * 
 * The code behind for EditUserWindow.xaml. 
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
    /// Interaction logic for EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window
    {
        /// <summary>
        /// This event is invoked when the user is successfuly updated
        /// </summary>
        public event EventHandler<UserUpdateEventArgs> SuccessfullyEditedUserEvent;

        /// <summary>
        /// This method invokes the SuccessfullyEditedUserEvent
        /// </summary>
        /// <param name="e">User update event args</param>
        protected virtual void OnSuccessfulCreationOfUser(UserUpdateEventArgs e)
        {
            if (SuccessfullyEditedUserEvent != null)
                SuccessfullyEditedUserEvent(this, e);
        }

        public User user; // A reference to the game user
        public List<User> userList; // A reference to the saved user list
        public UserAction editUser; // The type of action passed to the new window through event args.

        /// <summary>
        /// The constructor for the edit user window
        /// </summary>
        /// <param name="inComingUser">The user to be edited</param>
        /// <param name="inComingUserList">The user list</param>
        public EditUserWindow(User inComingUser, List<User> inComingUserList)
        {
            InitializeComponent();

            user = inComingUser; // Accept the user for processing            
            userList = inComingUserList; // Accept the user list for processing

            txtEditUserName.Text = user.UserName;
            pwEditUserPassword.Password = user.Password;
            pwConfirmNewUserPassword.Password = user.Password;
        }

        /// <summary>
        /// Edits the user
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void btnEditUser_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = CustomMessageBox.ShowYesNo(user.UserName + 
                ", are you sure you want to change your profile?", "Are You Sure You Want to Change Your Profile?", 
                "Yes, change my profile", "No, keep as is", MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                editUser = UserAction.EditUser;

                ValidateUserInputAndEditUser();

                this.Close();
            }
        }

        /// <summary>
        /// Deletes the user
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = CustomMessageBox.ShowYesNo(user.UserName + 
                ", are you sure you want to delete your profile?", "Are You Sure You Want to Delete Your Profile?", 
                "Yes, delete my profile", "No, let's keep it", MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                editUser = UserAction.DeleteUser;

                ValidateUserInputAndEditUser();

                this.Close();
            }
        }

        /// <summary>
        /// Closes the edit form
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void btnEditUserCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Validates user input and edits the user
        /// </summary>
        private void ValidateUserInputAndEditUser()
        {
            if (txtEditUserName.Text.IsNameValid())
            {
                if (user.IsUserContainedInUserList(userList))
                {
                    if (pwEditUserPassword.Password == pwConfirmNewUserPassword.Password && pwEditUserPassword.Password.Length > 0)
                    {
                        if (txtEditUserName.Text.IsEachCharAlphaNumericOrBlank())
                        {
                            user = new User(txtEditUserName.Text, pwEditUserPassword.Password);
                            OnSuccessfulCreationOfUser(new UserUpdateEventArgs(editUser, user)); // Raise the successful creation of a user event
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
                        if (pwEditUserPassword.Password.Length > 0)
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
    }
}
