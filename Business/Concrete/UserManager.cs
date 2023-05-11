using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entitiy.Concrete;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public List<OperationClaim> getClaims(User user)
        {
            return _userDal.GetClaims(user);

        }

        public void Add(User user)
        {
            _userDal.Add(user);

        }

        public void Update(User user)
        {
            _userDal.Update(user);
        }

        public void Delete(User user)
        {
            _userDal.Delete(user);
        }

        public User GetByEmail(string email)
        {
            return _userDal.Get(user => user.Email == email);
        }
    }
}
