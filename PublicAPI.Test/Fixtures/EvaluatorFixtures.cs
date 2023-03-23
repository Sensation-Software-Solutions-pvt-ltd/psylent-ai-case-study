using ApplicationCore.Enums;
using ApplicationCore.Services;
using ApplicationCore.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicAPI.Test.Fixtures
{
    public class EvaluatorFixtures : IEvaluatorFixture
    {

        public EvaluationResult CheckAllZeroRule(RawScore rawScore) => Evaluator.CheckAllZeroRule(rawScore);
        public EvaluationResult CheckAllLowScoreRule(RawScore rawScore) => Evaluator.CheckAllLowScoreRule(rawScore);

    }
}
