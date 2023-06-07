namespace Paukertj.Autoconverter.Generator.Contexts
{
    public class ConversionGeneratorContext<TContext>
        where TContext : TypeGeneratorContext
    {
        public TContext From { get; }

        public TContext To { get; }

        public ConversionGeneratorContext(TContext from, TContext to)
        {
            From = from;
            To = to;
        }
    }
}
