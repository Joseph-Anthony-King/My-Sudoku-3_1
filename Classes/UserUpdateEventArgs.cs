/**
 * 
 * My Sudoku 3.1
 * By Joseph King
 * September 9, 2015
 * 
 * UserUpdateEventArgs.cs
 * 
 * This class custom event arguements for the user 
 * update event.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySudoku3_1
{
    /// <summary>
    /// An emumeration indicating the action to be performed on the user
    /// </summary>
    public enum UserAction
    {
        CreateUser,
        LoginUser,
        EditUser,
        DeleteUser
    };
    
    /// <summary>
    /// The custom event arguments for the user update event: the action
    /// requested and the user to be updated
    /// </summary>
    public class UserUpdateEventArgs : EventArgs
    {
        public UserAction UserAction { get; set; }
        public User User { get; set; }

        public UserUpdateEventArgs(UserAction type, User user)
        {
            UserAction = type;
            User = user;
        }
    }
}
