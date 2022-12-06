using System;
using System.Collections.Generic;

namespace Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.Basic.Composition3
{
    public class BasicToEntity3Scenario123
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

	public class BasicToEntity3Scenario4
	{
        public IReadOnlyList<BasicToEntity31> Level1 { get; set; }

        public class BasicToEntity31
        {
            public DateTimeOffset? Level21 { get; set; }

            public DateTimeOffset? Level22 { get; set; }
        }
    }
}
