using System.Collections;

namespace Swappler.Models.Status
{
    public enum UserStatus
    {
        Unspecified,
        Added,
        Removed,
        Updated,
        Registered,
        Error,
        EmailAlreadyExist,
        UsernameAlreadyExist,
        EmailAndUsernameExist,
        EmailOrUsernameDoesNotExist,
        ValidCredentials,
        InvalidPassword
    }

    public static class UserStatusExtension
    {
        private static readonly Hashtable DescriptionTable = new Hashtable
        {
            {UserStatus.Unspecified, "Unspecified status!"},
            {UserStatus.Added, "Successfully added!"},
            {UserStatus.Removed, "Successfully removed!"},
            {UserStatus.Updated ,"Successfully updated!"},
            {UserStatus.Registered, "Successfully registered!"},
            {UserStatus.Error, "Error happend... Try again!"},
            {UserStatus.EmailAlreadyExist, "Email is already registered!"},
            {UserStatus.UsernameAlreadyExist, "Username is already taken!"},
            {UserStatus.EmailAndUsernameExist, "Specified email and username are already in use!"},
            {UserStatus.EmailOrUsernameDoesNotExist, "The email or username you've entered doesn't match any account!"},
            {UserStatus.InvalidPassword, "The password you've entered is incorrect!"},
            {UserStatus.ValidCredentials, "Entered credentials are valid!"}
        };

        public static string Description(this UserStatus userStatus)
        {
            return (string)DescriptionTable[userStatus];
        }
    }
    
}