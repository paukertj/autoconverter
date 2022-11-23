using Microsoft.CodeAnalysis;
using Paukertj.Autoconverter.Generator.Tests.Exceptions;
using System;
using System.IO;
using System.Reflection;

namespace Paukertj.Autoconverter.Generator.Tests.Extensions
{
    internal static class CompilationExtension
    {
        internal static TOutput Run<TOutput, TCompositor>(this Compilation compilation)
        {
            using (var ms = new MemoryStream())
            {
                // write IL code into memory
                var result = compilation.Emit(ms);

                if (!result.Success)
                {
                    throw new TestCompilationRunException(result.Diagnostics);
                }

                // load this 'virtual' DLL so that we can use
                ms.Seek(0, SeekOrigin.Begin);
                var assembly = Assembly.Load(ms.ToArray());

                // create instance of the desired class and call the desired function

                Type type = assembly.GetType(typeof(TCompositor).FullName);
                object obj = Activator.CreateInstance(type);

                return (TOutput)type.InvokeMember("Main",
                    BindingFlags.Default | BindingFlags.InvokeMethod,
                    null,
                    obj,
                    new object[] { });
            }
        }
    }
}
