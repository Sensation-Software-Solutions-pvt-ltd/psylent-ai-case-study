using ApplicationCore.Enums;
using ApplicationCore.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicAPI.Test.Fixtures
{
    public interface IEvaluatorFixture
    {

        EvaluationResult CheckAllZeroRule(RawScore rawScore);
        EvaluationResult CheckAllLowScoreRule(RawScore rawScore);

    }
}
