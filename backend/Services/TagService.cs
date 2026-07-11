namespace BlogApp.Services;

using System.Numerics;
using BlogApp.DTOs;
using BlogApp.Interfaces;
using BlogApp.Mappers;

public class TagService(ITagRepository repository) :   ITagService
{
    public ResponseBaseDTO GetAllTags()
    {
        var tags = repository.GetAllTags();

        return new ResponseBaseDTO<List<TagDTO>>(tags.ToDTOList(), 200);
    }

    public ResponseBaseDTO GetTop5Tags()
    {
        var tags = repository.GetTop5Tags();
        

        return new ResponseBaseDTO<List<TagDTO>>(tags.ToDTOList(), 200);
    }

    public TagDTO? GetTagByName(string name, ref string errCode)
    {
        var tag = repository.GetTagByName(name);

        if (tag == null)
        {
            errCode = "E3001"; // Tag not found
            return null;
        }

        return tag.ToDTO();
    }

    public bool AddTag(TagDTO tagDTO, ref string errCode)
    {
        bool result = false;
        var tag = repository.GetTagByName(tagDTO.Name);

        if(tag != null) 
        {
            errCode = "E3002"; // Tag already exists
            return false;
        }

        result = repository.AddTag(tagDTO.ToModel());

        if (!result)
        {
            errCode = "E3003"; // Failed to add tag
        }

        return result;
    }

    public bool UpdateTag(TagDTO tagDTO, ref string errCode)
    {
        bool result = false;
        var tag = repository.GetTagByName(tagDTO.Name);

        if(tag == null) 
        {
            errCode = "E3001"; // Tag not found
            return false;
        }

        result = repository.UpdateTag(tagDTO.ToModel());

        if (!result)
        {
            errCode = "E3004"; // Failed to update tag
        }

        return result;
    }

    public bool DeleteTag(string name, ref string errCode)
    {
        bool result = false;
        var tag = repository.GetTagByName(name);

        if(tag == null) 
        {
            errCode = "E3001"; // Tag not found
            return false;
        }

        result = repository.DeleteTag(tag);

        if (!result)
        {
            errCode = "E3005"; // Failed to delete tag
        }

        return result;
    }
}