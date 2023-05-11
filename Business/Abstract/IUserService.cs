using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Entitiy.Concrete;

namespace Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> getClaims (User user);
        void Add (User user);
        void Update (User user);
        void Delete (User user);
        User GetByEmail (string email);

    }
}
