﻿

using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using System.Reflection;

namespace MedicalOffice.Shared.Helper.Mapper;

public static class AboutUsMapper
{
    public static AboutUs Mapper(this AboutUsDto model)
    {
        var result = new AboutUs()
        {
            Id = model.Id,
            Title = model.Title,
            Text = model.Text,
            Image=model.ImageUrl
            
        };

        return result;
    }

    public static AboutUsDto Mapper(this AboutUs model)
    {
        var result = new AboutUsDto()
        {
            Id = model.Id,
            Title = model.Title,
            Text = model.Text
        };

        return result;
    }


    public static IEnumerable<AboutUsDto> Mapper(this IEnumerable<AboutUs> models)
    {

        var result = models.Select(model => new AboutUsDto
        {
            Id = model.Id,
            Title = model.Title,
            Text = model.Text,
            ImageUrl=model.Image
        });

        return result;
    }
    public static IEnumerable<AboutUs> Mapper(this IEnumerable<AboutUsDto> models)
    {

        var result = models.Select(model => new AboutUs
        {
            Id = model.Id,
            Title = model.Title,
            Text = model.Text,
        });

        return result;
    }
}
