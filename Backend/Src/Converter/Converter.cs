using System.Reflection.Metadata.Ecma335;

namespace Backend.Src.Converter;

public class Converter : IConverter
{
    public TReadDTO ConvertReadDTO<T, TReadDTO>(T model) where TReadDTO : new()
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }
        var item = new TReadDTO();

        foreach (var property in item.GetType().GetProperties())
        {
            var modelProperty = model.GetType().GetProperty(property.Name);
            if (modelProperty == null) 
            {
                continue;
            }
            object? value = modelProperty.GetValue(model);
            if (modelProperty.PropertyType.Name != property.PropertyType.Name && modelProperty.PropertyType.FullName is not null)
            {
                if (modelProperty.PropertyType.FullName.Contains("Backend.Src.Models") && property.PropertyType.Name == "String" && value is not null)
                {
                    property.SetValue(item, value.ToString());
                    continue;
                }
            }
            property.SetValue(item, value);
        }
        return item;
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
            if (itemProperty is not null ) 
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
