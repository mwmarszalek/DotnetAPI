
namespace DotnetAPI.Models
{
    public partial class Users
    {   
        // if having string type you cannot have a null value. if you do you will get an error. you can fix this by adding ? at the end eg. string? FirstName
        // however this is not a good practice. it is better to sort this out in a constructor (below)
        public int UserId {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string Email {get; set;}
        public string Gender {get; set;}
        public bool Active {get; set;}

        public Users()
        {
            if (FirstName == null)
            {
                FirstName = "";
            }
            if (LastName == null)
            {
                LastName = "";
            }
            if (Email == null)
            {
                Email = "";
            }
            if (Gender == null)
            {
                Gender = "";
            }
        }

    }
}