// this model is DATA TRANSFER OBJECT. it's the same as user model except that it's missing userId which we don't need when 
// inserting new data to table. It is used in our PUT


namespace DotnetAPI
{
    public partial class UserTOAddDto
    {   

        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string Email {get; set;}
        public string Gender {get; set;}
        public bool Active {get; set;}

        public UserDto()
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