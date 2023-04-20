namespace Backend.Src.Converter;

public interface IConvertReadDTO<T, TReadDTO>
{
    public TReadDTO ConvertReadDTO(T model);    
}