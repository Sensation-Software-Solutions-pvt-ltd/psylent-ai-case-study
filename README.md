# Case Study

This project is Engine Scoring API that takes input score and process it.

A score is defined as values of all [Culture](https://github.com/psylent-ai-org/case-study/blob/main/src/ApplicationCore/Enums/Culture.cs).

The process consists of changing the input into a raw score, scaling the raw score based on the maximum score to 100, and ranking the score.

Examples:
```json lines
// Score
{
  "collaborate": 151,
  "create": 160,
  "compete": 140,
  "control": 50
}

// Scaled Score from Score above
// 
{
  "collaborate": 94,
  "create": 100,
  "compete": 88,
  "control": 31
}
```


It also can check the raw score whether it fits condition in certain rules.


## Instructions
- Given this simple project, add test projects to cover all possible cases.
You can use any testing tool (xUnit, MSTest, or nUnit).
- Some functions has its summary written in their respective comments. Read carefully.