namespace BlogApp.Mappers
{
    using BlogApp.DTOs;
    using BlogApp.Entities;

    public static class RateMapper
    {
        public static RateDTO ToDTO(this Rate rate)
        {
            return new RateDTO
            {
                Id = rate.Id,
                Score = rate.Score,
                AuthorId = rate.AuthorId,
                BlogId = rate.BlogId
            };
        }

        public static Rate ToModel(this RateDTO rateDTO)
        {
            return new Rate
            {
                Id = rateDTO.Id,
                Score = rateDTO.Score,
                AuthorId = rateDTO.AuthorId,
                BlogId = rateDTO.BlogId
            };
        }
    }
}