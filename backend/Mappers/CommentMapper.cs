namespace BlogApp.Mappers
{
    using BlogApp.DTOs;
    using BlogApp.Entities;

    public static class CommentMapper
    {
        public static CommentDTO ToDTO(this Comment comment)
        {
            return new CommentDTO
            {
                Id = comment.Id,
                Content = comment.Content,
                Likes = comment.Likes,
                Dislikes = comment.Dislikes,
                State = comment.State,
                AuthorId = comment.AuthorId,
                BlogId = comment.BlogId,
                ParentId = comment.ParentId
            };
        }

        public static Comment ToModel(this CommentDTO commentDTO)
        {
            return new Comment
            {
                Id = commentDTO.Id,
                Content = commentDTO.Content,
                Likes = commentDTO.Likes,
                Dislikes = commentDTO.Dislikes,
                State = commentDTO.State,
                AuthorId = commentDTO.AuthorId,
                BlogId = commentDTO.BlogId,
                ParentId = commentDTO.ParentId
            };
        }
    }
}