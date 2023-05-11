using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entitiy.Concrete.Dtos;
using Entity.Concrete;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public IDataResult<List<Category>> GetAll()
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.GetList().ToList());

        }

        public IDataResult<Category> GetById(int categoryId)
        {
            return new SuccessDataResult<Category>(_categoryDal.Get(c => c.CategoryId == categoryId));
        }

        public IResult Add(CategoryDetail categoryDetail)
        {
            Category category = new Category
            {
                CategoryName = categoryDetail.CategoryName
            };
            _categoryDal.Add(category);
            return new SuccessResult(Messages.CategoryAdded);

        }

        public IResult Update(Category category)
        {
            _categoryDal.Update(category);
            return new SuccessResult(Messages.CategoryUpdated);
        }

        public IResult Delete(int categoryId)
        {
            _categoryDal.Delete(_categoryDal.Get(c => c.CategoryId == categoryId));
            return new SuccessResult(Messages.CategoryDeleted);

        }
    }
}