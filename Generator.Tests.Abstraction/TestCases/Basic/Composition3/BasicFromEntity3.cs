using System;
using System.Collections.Generic;

namespace Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.Basic.Composition3
{
    public class BasicFromEntity3Scenario123
    {
        public BasicFromEntity31 Level1 { get; set; }

        public class BasicFromEntity31
        {
            public BasicFromEntity32 Level2 { get; set; }

            public class BasicFromEntity32
            {
                public BasicFromEntity33 Level3 { get; set; }

                public class BasicFromEntity33
                {
                    public string Level4 { get; set; }
                }
            }
        }
    }

	public class BasicFromEntity3Scenario4
	{
        public IReadOnlyList<BasicFromEntity31> Level1 { get; set; }

        public class BasicFromEntity31
        {
            public DateTimeOffset? Level21 { get; set; }

            public DateTimeOffset? Level22 { get; set; }
        }
    }
}
