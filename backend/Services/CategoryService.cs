namespace BlogApp.Services;

using System.Numerics;
using BlogApp.DTOs;
using BlogApp.Interfaces;
using BlogApp.Mappers;

public class CategoryService(ICategoryRepository repository) : ICategoryService
{
    public RespDTO GetAllCategories()
    {
        var categories = repository.GetAllCategories();

        return new CategoryListRespDTO(categories.ToDTOList(), 200, "");
    }

    public RespDTO GetTop5Categories()
    {
        var categories = repository.GetTop5Categories();
        

        return new CategoryListRespDTO(categories.ToDTOList(), 200, "");
    }

    public CategoryDTO? GetCategoryById(string name, ref string errCode)
    {
        var category = repository.GetCategoryByName(name);

        if (category == null)
        {
            errCode = "E3001"; // Category not found
            return null;
        }

        return category.ToDTO();
    }

    public bool AddCategory(CategoryDTO categoryDTO, ref string errCode)
    {
        bool result = false;
        var category = repository.GetCategoryByName(categoryDTO.Name);

        if(category != null) 
        {
            errCode = "E3002"; // Category already exists
            return false;
        }

        result = repository.AddCategory(categoryDTO.ToModel());

        if (!result)
        {
            errCode = "E3003"; // Failed to add category
        }

        return result;
    }

    public bool UpdateCategory(CategoryDTO categoryDTO, ref string errCode)
    {
        bool result = false;
        var category = repository.GetCategoryByName(categoryDTO.Name);

        if(category == null) 
        {
            errCode = "E3001"; // Category not found
            return false;
        }

        result = repository.UpdateCategory(categoryDTO.ToModel());

        if (!result)
        {
            errCode = "E3004"; // Failed to update category
        }

        return result;
    }

    public bool DeleteCategory(string name, ref string errCode)
    {
        bool result = false;
        var category = repository.GetCategoryByName(name);

        if(category == null) 
        {
            errCode = "E3001"; // Category not found
            return false;
        }

        result = repository.DeleteCategory(category);

        if (!result)
        {
            errCode = "E3005"; // Failed to delete category
        }

        return result;
    }
}