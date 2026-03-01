namespace BlogApp.Mappers
{
    using BlogApp.DTOs;
    using BlogApp.Entities;

    /// <summary>
    ///     Map between Category Entity and DTO
    /// </summary>
    public static class CategoryMapper
    {
        /// <summary>
        ///    Map Category Entity to Category DTO
        /// </summary>
        /// <param name="category">Category Entity Object</param>
        /// <returns>Category DTO Object</returns>
        public static CategoryDTO ToDTO(this Category category)
        {
            return new CategoryDTO
            {
                Name = category.Name,
                Description = category.Description,
                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt
            };
        }

        /// <summary>
        ///     Map Category DTO to Category Entity
        /// </summary>
        /// <param name="categoryDTO">Category DTO Object</param>
        /// <returns>Category Entity Object</returns>
        public static Category ToModel(this CategoryDTO categoryDTO)
        {
            return new Category
            {
                Name = categoryDTO.Name,
                Description = categoryDTO.Description
            };
        }

        /// <summary>
        ///    Map List of Category Entity to List of Category DTO
        /// </summary>
        /// <param name="categories">List of Category Entities</param>
        /// <returns>List of Category DTOs</returns>
        public static List<CategoryDTO> ToDTOList(this List<Category> categories)
        {
                return categories.Select(c => c.ToDTO()).ToList();
        }
    }
}