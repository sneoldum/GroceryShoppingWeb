using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;

namespace Business.Constants
{
    public static class Messages
    {
        //Product messages
        public static string ProductAdded = "Product successfully added !";
        public static string ProductDeleted = "Product successfully deleted !";
        public static string ProductUpdated = "Product successfully updated !";
        //Category messages
        public static string CategoryAdded = "Category successfully added !";
        public static string CategoryDeleted = "Category successfully deleted !";
        public static string CategoryUpdated = "Category successfully updated !";
        //Cart messages
        public static string CartSet = "Cart successfully set !";
        public static string CartAdded = "Product successfully added to cart!";
        public static string CartDeleted = "Product successfully deleted from cart!";
        public static string CartUpdated = "Cart successfully updated !";
        public static string CartCleared = "Cart successfully cleared !";
        //User messages
        public static string UserNotFound = "User not found !";
        public static string PasswordError = "Invalid Password !";
        public static string SuccessfulLogin = "Logged successfully !";
        public static string UserAlreadyExists = "User already exists !";
        public static string UserRegistered = "User registered successfully !";
        public static string AccessTokenCreated="Access token created successfully";
    }
}
