using System.ComponentModel.DataAnnotations;
using WordleHelper.Models;
using WordleHelper.Services;

namespace WordleHelper.Tests
{
	public class InputModelTests
	{
		[Theory]
		[InlineData("CRANE ")]
		[InlineData(" CRANE")]
		[InlineData("\tCRANE \n")]
		public void Word_SurroundingWhitespace_ProducesValidFiveLetterWord(string input)
		{
			// Assemble
			var model = new InputModel(new Wordlizer()) { Word = input };

			// Act
			var validationResults = Validate(model);

			// Assert
			Assert.Equal("CRANE", model.Word);
			Assert.Empty(validationResults);
		}

		[Theory]
		[InlineData(" CRAN ")]
		[InlineData(" CRANES ")]
		public void Word_WrongTrimmedLength_FailsValidation(string input)
		{
			// Assemble
			var model = new InputModel(new Wordlizer()) { Word = input };

			// Act
			var validationResults = Validate(model);

			// Assert
			Assert.NotEmpty(validationResults);
		}

		[Fact]
		public void AddGuess_TrimmedWord_StoresFiveLetters()
		{
			// Assemble
			var model = new InputModel(new Wordlizer()) { Word = "CRANE " };

			// Act
			model.AddGuess(model.Word);

			// Assert
			Assert.Equal("CRANE", model.Guesses.Single().ToString());
		}

		[Fact]
		public void GenerateSuggestions_SameYellowLetterAcrossGuesses_RequiresOneCopy()
		{
			// Assemble
			var model = new InputModel(new Wordlizer());
			model.AddGuess("EXXXX");
			model.AddGuess("XEXXX");
			model.Guesses[0].Letters[0].ToggleState();
			model.Guesses[1].Letters[1].ToggleState();

			// Act
			model.GenerateSuggestions();

			// Assert
			Assert.Equal(3, model.Suggestions.Count);
			Assert.All(model.Suggestions, suggestion => Assert.Equal(1, suggestion.Count(l => l == 'E')));
		}

		private static List<ValidationResult> Validate(InputModel model)
		{
			var results = new List<ValidationResult>();
			Validator.TryValidateObject(model, new ValidationContext(model), results, true);
			return results;
		}
	}
}
