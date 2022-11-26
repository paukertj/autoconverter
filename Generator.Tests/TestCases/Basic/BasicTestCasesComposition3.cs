using Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.Basic.Composition3;
using Paukertj.Autoconverter.Primitives.Services.Converting;

namespace Paukertj.Autoconverter.Generator.Tests.TestCases.Basic
{
    public class BasicTestCasesComposition3 : TestCasesCompositionBase<IBasicTestCasesService3, BasicTestCasesService3>
    {

    }

    public class BasicTestCasesService3 : IBasicTestCasesService3
    {
        private readonly IConvertingService _convertingService;

        public BasicTestCasesService3(IConvertingService convertingService)
        {
            _convertingService = convertingService;
        }

        public BasicToEntity3 ConvertScenario1()
        {
            var from = GetSourceScenario1();

            return _convertingService.Convert<BasicFromEntity3, BasicToEntity3>(from);
        }

        public BasicToEntity3 ConvertScenario2()
        {
            var from = GetSourceScenario2();

            return _convertingService.Convert<BasicFromEntity3, BasicToEntity3>(from);
        }

        public BasicToEntity3 ConvertScenario3()
        {
            var from = GetSourceScenario3();

            return _convertingService.Convert<BasicFromEntity3, BasicToEntity3>(from);
        }

        public BasicFromEntity3 GetSourceScenario1()
        {
            return new BasicFromEntity3
            {
                Level1 = new BasicFromEntity3.BasicFromEntity31
                {
                    Level2 = new BasicFromEntity3.BasicFromEntity31.BasicFromEntity32
                    {
                        Level3 = new BasicFromEntity3.BasicFromEntity31.BasicFromEntity32.BasicFromEntity33
                        {
                            Level4 = "end"
                        }
                    }
                }
            };
        }

        public BasicFromEntity3 GetSourceScenario2()
        {
            return new BasicFromEntity3
            {
                Level1 = new BasicFromEntity3.BasicFromEntity31
                {
                    Level2 = new BasicFromEntity3.BasicFromEntity31.BasicFromEntity32
                    {
                        Level3 = null
                    }
                }
            };
        }

        public BasicFromEntity3 GetSourceScenario3()
        {
            return new BasicFromEntity3
            {
                Level1 = null
            };
        }
    }
}
