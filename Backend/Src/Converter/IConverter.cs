namespace Backend.Src.Converter;

public interface IConverter
{
    public TReadDTO ConvertReadDTO<T, TReadDTO>(T model) where TReadDTO : new();
    public void UpdateModel<T, TUpdateDTO>(T model, TUpdateDTO update);
    public void CreateModel<T, TCreateDTO>(TCreateDTO create, out T model) where T : new();
}