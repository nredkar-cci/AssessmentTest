using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AssessmentTest.Domain.Enums
{
    public enum CoachLevel
    {
        [Description("Regular")]
        Regular = 1,
        [Description("Elite")]
        Elite = 2,
    }
}
