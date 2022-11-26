namespace Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.Basic.Composition3
{
    public class BasicToEntity3
    {
        public BasicToEntity31 Level1 { get; set; }

        public class BasicToEntity31
        {
            public BasicToEntity32 Level2 { get; set; }

            public class BasicToEntity32
            {
                public BasicToEntity33 Level3 { get; set; }

                public class BasicToEntity33
                {
                    public string Level4 { get; set; }
                }
            }
        }
    }
}
