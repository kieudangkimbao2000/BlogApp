namespace BlogApp.Controllers;

using BlogApp.Common;
using BlogApp.DTOs;
using BlogApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/categ")]
[Authorize]
public class CategoryController(ICategoryService service) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<CategoryDTO>> GetAllCategories()
    {
        return Ok(service.GetAllCategories());
    }

    [HttpGet("{id}")]
    public ActionResult<CategoryDTO?> GetCategoryById(string id)
    {
        string errCode = "";
        var category = service.GetCategoryById(id, ref errCode);

        if (category == null)
        {
            return NotFound(AppMessages.GetMessage(errCode));
        }

        return Ok(category);
    }

    [HttpPost]
    public ActionResult AddCategory([FromBody] CategoryDTO categoryDTO)
    {
        string errCode = "";
        bool result = service.AddCategory(categoryDTO, ref errCode);

        if (!result)
        {
            if (errCode == "E3002")
            {
                return BadRequest(AppMessages.GetMessage(errCode));
            }
            else
            {
                return StatusCode(500,AppMessages.GetMessage(errCode));
            }
        }

        return Ok(AppMessages.GetMessage("I3001"));
    }

    [HttpPut]
    public ActionResult UpdateCategory([FromBody] CategoryDTO categoryDTO)
    {
        string errCode = "";
        bool result = service.UpdateCategory(categoryDTO, ref errCode);

        if (!result)
        {
            if (errCode == "E3001")
            {
                return NotFound(AppMessages.GetMessage(errCode));
            }
            else
            {
                return StatusCode(500,AppMessages.GetMessage(errCode));
            }
        }

        return Ok(AppMessages.GetMessage("I3002"));
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteCategory(string id)
    {
        string errCode = "";
        bool result = service.DeleteCategory(id, ref errCode);

        if (!result)
        {
            if (errCode == "E3001")
            {
                return NotFound(AppMessages.GetMessage(errCode));
            }
            else
            {
                return StatusCode(500,AppMessages.GetMessage(errCode));
            }
        }

        return Ok(AppMessages.GetMessage("I3003"));
    }

    [HttpGet("topfive")]
    public ActionResult GetTop5Categories()
    {
        var resp = service.GetTop5Categories();

        return Ok(resp);
    }
}