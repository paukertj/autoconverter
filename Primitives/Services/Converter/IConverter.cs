namespace Paukertj.Autoconverter.Primitives.Services.Converter
{
	public interface IConverter<TFrom, TTo>
	{
		TTo Convert(TFrom from);
	}
}
