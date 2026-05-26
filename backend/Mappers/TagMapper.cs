namespace BlogApp.Mappers
{
    using BlogApp.DTOs;
    using BlogApp.Entities;

    /// <summary>
    ///     Map between Tag Entity and DTO
    /// </summary>
    public static class TagMapper
    {
        /// <summary>
        ///    Map Tag Entity to Tag DTO
        /// </summary>
        /// <param name="tag">Tag Entity Object</param>
        /// <returns>Tag DTO Object</returns>
        public static TagDTO ToDTO(this Tag tag)
        {
            return new TagDTO
            {
                Name = tag.Name,
                Description = tag.Description,
                CreatedAt = tag.CreatedAt,
                UpdatedAt = tag.UpdatedAt
            };
        }

        /// <summary>
        ///     Map Tag DTO to Tag Entity
        /// </summary>
        /// <param name="tagDTO">Tag DTO Object</param>
        /// <returns>Tag Entity Object</returns>
        public static Tag ToModel(this TagDTO tagDTO)
        {
            return new Tag
            {
                Name = tagDTO.Name,
                Description = tagDTO.Description
            };
        }

        /// <summary>
        ///    Map List of Tag Entity to List of Tag DTO
        /// </summary>
        /// <param name="tags">List of Tag Entities</param>
        /// <returns>List of Tag DTOs</returns>
        public static List<TagDTO> ToDTOList(this List<Tag> tags)
        {
                return tags.Select(t => t.ToDTO()).ToList();
        }
    }
}