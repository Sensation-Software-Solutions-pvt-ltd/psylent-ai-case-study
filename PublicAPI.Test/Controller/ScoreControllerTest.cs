using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using ApplicationCore.TransferObjects;
using ApplicationCore.ValueObjects;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PublicApi.Controllers;
using PublicAPI.Test.Fixtures;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicAPI.Test.Controller
{
    public class ScoreControllerTest
    {
        private readonly Mock<IScoring> _scoringServiceMock;
        private readonly ScoresController _controller;
        private readonly ScoringService _scoring;


        public ScoreControllerTest()
        {
            _scoringServiceMock = new Mock<IScoring>();
            _controller = new ScoresController(_scoringServiceMock.Object);
            _scoring = new ScoringService();

        }

        #region ControllerTest

        [Fact]
        public void CheckRulesHandler_ResultExpected_AllZeroFailCheck_OK()
        {
            //Arrange
            var request = new EvaluateScoreRequest
            {
                Data = new Score
                {
                    Collaborate = 5,
                    Create = 9,
                    Compete = 5,
                    Control = 5,
                }
            };
            var rawScore = new RawScore(request.Data.Value);
            var allZeroResult = Evaluator.CheckAllZeroRule(rawScore);
            var allLowResult = Evaluator.CheckAllLowScoreRule(rawScore);
            var expectedResponse = new EvaluatedScoreResponse
            {
                Results = new List<EvaluatedResult>()
                {
                new() { Result = allZeroResult, Name = RuleType.AllZeros },
                new() { Result = allLowResult, Name = RuleType.AllLowScore }
                }
            };
            //Act
            var result = _controller.CheckRulesHandler(request);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as EvaluatedScoreResponse;
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equivalent(response, expectedResponse);
            Assert.Equal(allZeroResult, response.Results[0].Result);
        }

        [Fact]
        public void CheckRulesHandler_ResultExpected_OneValueHigher_OK()
        {
            //Arrange
            var request = new EvaluateScoreRequest
            {
                Data = new Score
                {
                    Collaborate = 5,
                    Create = 50,
                    Compete = 5,
                    Control = 5,
                }
            };
            var rawScore = new RawScore(request.Data.Value);
            var allZeroResult = Evaluator.CheckAllZeroRule(rawScore);
            var allLowResult = Evaluator.CheckAllLowScoreRule(rawScore);
            var expectedResponse = new EvaluatedScoreResponse
            {
                Results = new List<EvaluatedResult>()
                {
                new() { Result = allZeroResult, Name = RuleType.AllZeros },
                new() { Result = allLowResult, Name = RuleType.AllLowScore }
                }
            };
            //Act
            var result = _controller.CheckRulesHandler(request);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as EvaluatedScoreResponse;
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equivalent(response, expectedResponse);
            Assert.Equal(allZeroResult, response.Results[0].Result);
        }

        [Fact]
        public void ProcessScoreHandler_WithValidRequest_ReturnOk()
        {
            // Arrange
            var request = new ProcessScoreRequest
            {
                Data = new Score
                {
                    Collaborate = 0,
                    Create = 2,
                    Compete = 3,
                    Control = 4,
                }
            };
            var raw = new RawScore(request.Data.Value);
            var scaled = _scoring.Scale(raw);
            var rank = _scoring.Rank(scaled);

            var expectedResponse = new ProcessedScoreResponse
            {
                Data = new ProcessedScore
                {
                    Raw = raw,
                    Scaled = scaled,
                    Ranked = rank
                }
            };

            _scoringServiceMock.Setup(x => x.Scale(It.IsAny<RawScore>())).Returns(scaled);
            _scoringServiceMock.Setup(x => x.Rank(It.IsAny<ScaledScore>())).Returns(rank);

            // Act
            var result = _controller.ProcessScoreHandler(request);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ProcessedScoreResponse;
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equivalent(response, expectedResponse);
        }
        [Fact]
        public void ProcessScoreHandler_WithAllZeroValues_ReturnsBadRequest()
        {
            // Arrange
            var request = new ProcessScoreRequest
            {
                Data = new Score
                {
                    Collaborate = 0,
                    Create = 0,
                    Compete = 0,
                    Control = 0,
                }
            };
            // Act
            var result = _controller.ProcessScoreHandler(request);
            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Please input atleast one value", badRequestResult.Value);
            Assert.Equal(400, badRequestResult.StatusCode);
        }
        [Fact]
        public void CheckRulesHandler_ResultExpected_AllZeroApplied_OK()
        {
            //Arrange
            var request = new EvaluateScoreRequest
            {
                Data = new Score
                {
                    Collaborate = 0,
                    Create = 0,
                    Compete = 0,
                    Control = 0,
                }
            };
            var rawScore = new RawScore(request.Data.Value);
            var allZeroResult = Evaluator.CheckAllZeroRule(rawScore);
            var allLowResult = Evaluator.CheckAllLowScoreRule(rawScore);
            var expectedResponse = new EvaluatedScoreResponse
            {
                Results = new List<EvaluatedResult>()
                {
                new() { Result = allZeroResult, Name = RuleType.AllZeros },
                new() { Result = allLowResult, Name = RuleType.AllLowScore }
                }
            };
            //Act
            var result = _controller.CheckRulesHandler(request);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as EvaluatedScoreResponse;
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equivalent(response, expectedResponse);
            Assert.Equal(allZeroResult, response.Results[0].Result);
        }
        [Fact]
        public void CheckAllLowScoreRule_ShouldReturnApplied_WhenOneQuadrantHasLowScore()
        {
            // Arrange

            var request = new ProcessScoreRequest
            {
                Data = new Score
                {
                    Collaborate = 5,
                    Create = 5,
                    Compete = 5,
                    Control = 1,
                }
            };
            var rawScore = new RawScore(request.Data.Value);
            // Act
            var result = Evaluator.CheckAllLowScoreRule(rawScore);
            // Assert
            Assert.Equal(EvaluationResult.Applied, result);
        }

        #endregion
        #region ServiceTest
        [Fact]
        public void CheckScoringMethod_ReturnActualRank()
        {
            //Arrange
            var request = new ProcessScoreRequest
            {
                Data = new Score
                {
                    Collaborate = 0,
                    Create = 1,
                    Compete = 2,
                    Control = 3,
                }
            };
            var raw = new RawScore(request.Data.Value);
            var scale = _scoring.Scale(raw);
            //Act
            var rank = _scoring.Rank(scale);
            //Assert
            Assert.NotNull(rank);
            Assert.Equal(rank.First, scale.Control.Culture);
            Assert.Equal(rank.Second, scale.Compete.Culture);
            Assert.Equal(rank.Third, scale.Create.Culture);
            Assert.Equal(rank.Fourth, scale.Collaborate.Culture);

        }

        [Fact]
        public void CheckAllZeroRule_ShouldReturnApplied_WhenAllQuadrantsAreZero()
        {
            // Arrange
            var request = new ProcessScoreRequest
            {
                Data = new Score
                {
                    Collaborate = 0,
                    Create = 0,
                    Compete = 0,
                    Control = 0,
                }
            };
            var rawScore = new RawScore(request.Data.Value);
            var expectedResult = new EvaluatedScoreResponse();
            // Act
            var result = Evaluator.CheckAllZeroRule(rawScore);
            // Assert
            Assert.Equal(EvaluationResult.Applied, result);
        }

        [Fact]
        public void CheckOneHighScore_ShouldReturnApplied_WhenOneQuadrantsHaveHighScore()
        {
            // Arrange

            var request = new ProcessScoreRequest
            {
                Data = new Score
                {
                    Collaborate = 5,
                    Create = 55,
                    Compete = 5,
                    Control = 5,
                }
            };
            var rawScore = new RawScore(request.Data.Value);
            // Act
            var result = Evaluator.CheckAllLowScoreRule(rawScore);
            // Assert
            Assert.Equal(EvaluationResult.FailedChecks, result);
        }

        #endregion



        [Fact]
        public void CheckScoringMethod_ReturnActualScale()
        {
            //Arrange
            var request = new ProcessScoreRequest
            {
                Data = new Score
                {
                    Collaborate = 0,
                    Create = 1,
                    Compete = 2,
                    Control = 3,
                }
            };
            var raw = new RawScore(request.Data.Value);
            //Act
            var res = _scoring.Scale(raw);
            //Assert
            Assert.IsType<ScaledScore>(res);
            Assert.NotNull(res);
        }
       

    }
}
