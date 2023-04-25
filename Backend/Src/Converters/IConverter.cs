namespace Backend.Src.Converters;

public interface IConverter<T, TReadDTO, TCreateDTO, TUpdateDTO>
{
    public TReadDTO ConvertReadDTO(T model);
    public void UpdateModel(T model, TUpdateDTO update);
    public void CreateModel(T model, TCreateDTO create);
}