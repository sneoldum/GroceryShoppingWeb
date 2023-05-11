using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entitiy.Concrete.Dtos;
using Entity.Concrete;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        IDataResult<List<Category>> GetAll();
        IDataResult<Category> GetById(int categoryId);
        IResult Add(CategoryDetail categoryDetail);
        IResult Update(Category category);
        IResult Delete(int categoryId);
    }
}
