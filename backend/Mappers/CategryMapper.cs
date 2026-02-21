namespace BlogApp.Mappers
{
    using BlogApp.DTOs;
    using BlogApp.Entities;

    /// <summary>
    ///     Mapper for Category
    /// </summary>
    public static class CategoryMapper
    {
        /// <summary>
        ///    Convert Category Entity to Category DTO
        /// </summary>
        /// <param name="category">The category entity to be converted</param>
        /// <returns>The corresponding CategoryDTO</returns>
        public static CategoryDTO ToDTO(this Category category)
        {
            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        /// <summary>
        ///     Convert Category DTO to Category Entity
        /// </summary>
        /// <param name="categoryDTO">The category DTO to be converted</param>
        /// <returns>The corresponding Category entity</returns>
        public static Category ToModel(this CategoryDTO categoryDTO)
        {
            return new Category
            {
                Id = categoryDTO.Id,
                Name = categoryDTO.Name,
                Description = categoryDTO.Description
            };
        }

        /// <summary>
        ///    Convert a list of Category entities to a list of Category DTOs
        /// </summary>
        /// <param name="categories">The list of Category entities to be converted</param>
        /// <returns>The corresponding list of CategoryDTOs</returns>
        public static List<CategoryDTO> ToDTOList(this List<Category> categories)
        {
                return categories.Select(c => c.ToDTO()).ToList();
        }
    }
}