/**
 * 
 * My Sudoku 3.1
 * By Joseph King
 * September 9, 2015
 * 
 * User.cs
 * 
 * This class defines the profiles for the users.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MySudoku3_1
{
    [Serializable()]
    public class User : ISerializable
    {
        #region Private Fields
        /// <summary>
        /// The private representation for the users name
        /// </summary>
        private String name;

        #endregion

        #region Properties

        /// <summary>
        /// The password for the user
        /// </summary>
        public String Password { get; set; }

        /// <summary>
        /// Property that returns the users logged in status
        /// </summary>
        public bool IsLoggedIn { get; set; }

        /// <summary>
        /// The user name must be from 4 to 12 characters
        /// to ensure it can be printed on the user screen
        /// </summary>
        public String UserName
        {
            get
            {
                return name;
            }

            set
            {
                // Uses the IsNameValid() extension to ensure
                // the name meets the criteria noted above
                if (value.IsNameValid())
                {
                    name = value;
                }
            }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// This constructor builds a new user
        /// </summary>
        /// <param name="userName">The users name</param>
        /// <param name="userPassword">The users password</param>
        public User (String userName, String userPassword)
        {
            UserName = userName;
            Password = userPassword;
            IsLoggedIn = false;
        }

        /// <summary>
        /// A constructor for building a user from memory
        /// </summary>
        public User (SerializationInfo info, StreamingContext ctxt)
        {
            this.UserName = (string)info.GetValue("Name", typeof(string));
            this.Password = (string)info.GetValue("Password", typeof(string));
            IsLoggedIn = false;
        }

        /// <summary>
        /// A parameterless constructor
        /// </summary>
        public User ()
        {
            UserName = "***";
            Password = null;
            IsLoggedIn = false;
        }

        #endregion

        #region ISerializable Members
        /// <summary>
        /// ISerializable members
        /// </summary>
        /// <param name="info">SerializationInfo info</param>
        /// <param name="context">StreamingContext context</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", this.UserName);
            info.AddValue("Password", this.Password);
        }
        #endregion

        #region Class Instance Methods
        /// <summary>
        /// This method is used to determine if the user is contained in a user list
        /// </summary>
        /// <param name="userList">The user list we are testing</param>
        /// <returns>Returns true if the user is contained in the list</returns>
        public bool IsUserContainedInUserList(List<User> userList)
        {
            foreach (User u in userList)
            {
                if (u.UserName == this.UserName)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// This method is used on the login in screen to determine if the 
        /// password entered matches the password saved in the user list
        /// </summary>
        /// <param name="userList">The user list we are testing</param>
        /// <returns>Returns true if the password is correct</returns>
        public bool IsUserPasswordCorrect(List<User> userList)
        {
            User userFromList = new User();

            if (IsUserContainedInUserList(userList))
            {
                userFromList = userList.FirstOrDefault(u => u.UserName == this.UserName);
            }

            if (userFromList.Password == this.Password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// This method logs in the user
        /// </summary>
        public void Login()
        {
            IsLoggedIn = true;
        }

        /// <summary>
        /// This method logs out the user
        /// </summary>
        public void Logout()
        {
            IsLoggedIn = false;
        }
        #endregion
    }
}
