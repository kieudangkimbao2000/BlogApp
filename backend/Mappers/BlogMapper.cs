namespace BlogApp.Mappers;
    using BlogApp.Models;
    using BlogApp.DTOs;

    public static class BlogMapper
    {
        public static BlogDTO ToDTO(Blog blog)
        {
            return new BlogDTO
            {
                Id = blog.Id,
                Title = blog.Title,
                Content = blog.Content,
                Categories = blog.Categories,
                Rating = blog.Rating,
                AmountOfAccesses = blog.AmountOfAccesses,
                AmountOfComments = blog.AmountOfComments,
                AmountOfRates = blog.AmountOfRates,
                AuthorId = blog.AuthorId
            };
        }

        public static Blog ToModel(BlogDTO blogDTO)
        {
            return new Blog
            {
                Id = blogDTO.Id,
                Title = blogDTO.Title,
                Content = blogDTO.Content,
                Categories = blogDTO.Categories,
                Rating = blogDTO.Rating,
                AmountOfAccesses = blogDTO.AmountOfAccesses,
                AmountOfComments = blogDTO.AmountOfComments,
                AmountOfRates = blogDTO.AmountOfRates,
                AuthorId = blogDTO.AuthorId
            };
        }
    }