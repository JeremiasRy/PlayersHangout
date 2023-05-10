using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection.Metadata.Ecma335;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Backend.Src.Converter;

public class Converter : IConverter
{
    public TReadDTO ConvertReadDTO<T, TReadDTO>(T model, TReadDTO? readDTO = default) where TReadDTO : new()
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }
        readDTO ??= new TReadDTO();

        foreach (var property in readDTO.GetType().GetProperties())
        {
            var modelProperty = model.GetType().GetProperty(property.Name);
            if (modelProperty == null)
            {
                continue;
            }
            if (modelProperty.PropertyType.FullName is null || property.PropertyType.FullName is null)
            {
                throw new Exception();
            } 
            object? value = modelProperty.GetValue(model);

            if (modelProperty.PropertyType.Name != property.PropertyType.Name && modelProperty.PropertyType.FullName is not null)
            {
                if (modelProperty.PropertyType.FullName.Contains("Backend.Src.Models") && property.PropertyType.Name == "String" && value is not null)
                {
                    property.SetValue(readDTO, value.ToString());
                    continue;
                }
            }
            property.SetValue(readDTO, value);
        }
        return readDTO;
    }

    public void CreateModel<T, TCreateDTO>(TCreateDTO create, out T model) where T : new()
    {
        model = new T();

        if (create is null) 
        {
            throw new ArgumentNullException(nameof(create));
        }

        foreach (var property in create.GetType().GetProperties())
        {
            var itemProperty = model.GetType().GetProperty(property.Name);
            if (itemProperty == null)
            {
                continue;
            }

            object? value = property.GetValue(create);
            if (property.PropertyType.FullName is null || itemProperty.PropertyType.FullName is null)
            {
                throw new Exception();
            }
            if (property.PropertyType.FullName.Contains("String") && itemProperty.PropertyType.FullName.Contains("Backend.Src.Models"))
            {
                var constructor = itemProperty.PropertyType.GetConstructor(Array.Empty<Type>()) ?? throw new Exception("Couldn't create model from default ctor");
                var propertyModel = constructor.Invoke(Array.Empty<object>());
                var createdModelProperty = propertyModel.GetType().GetProperty("Name");
                if (createdModelProperty is not null)
                {
                    createdModelProperty.SetValue(propertyModel, property.GetValue(create));
                    itemProperty.SetValue(model, propertyModel);
                    continue;
                }

            }
            if (value is not null) 
            {
                itemProperty.SetValue(model, property.GetValue(create));
            }

        }
    }

    public void UpdateModel<T, TUpdateDTO>(T model, TUpdateDTO update)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }
        if (update is null)
        {
            throw new ArgumentNullException(nameof(model));
        }
        foreach (var property in model.GetType().GetProperties())
        {
            var updateProperty = update.GetType().GetProperty(property.Name);
            if (updateProperty is not null)
            {
                property.SetValue(model, updateProperty.GetValue(update));
            }
        }
    }
}
