namespace Paukertj.Autoconverter.Generator.Pipes
{
    internal interface ICodeGeneratingPipe
    {
        string GetFileName();

        string GetSourceCode();
    }
}
