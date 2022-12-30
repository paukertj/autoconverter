namespace Paukertj.Autoconverter.Generator.Services.ResolverAnalysis.Resolving
{
    public class ResolvingInfoIgnore : ResolvingInfoBase
    {
        public string Member { get; }

        public ResolvingInfoIgnore(string member)
        {
            Member = member;
        }
    }
}
