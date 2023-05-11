using Core.Entities.Concrete;
using Entity.Concrete;
using System.ComponentModel;
using Entitiy.Concrete.Dtos;

namespace ShoppingWebUI.Models
{
    public class LoginViewModel
    {
        public UserDetail UserDetail { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
