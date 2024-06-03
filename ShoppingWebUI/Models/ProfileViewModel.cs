namespace ShoppingWebUI.Models
{
    public class ProfileViewModel
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Balance { get; set; }
        public List<string> PlayedBets { get; set; }
        public List<string> ComplatedBets { get; set; }



    }
}
