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

		private static List<ValidationResult> Validate(InputModel model)
		{
			var results = new List<ValidationResult>();
			Validator.TryValidateObject(model, new ValidationContext(model), results, true);
			return results;
		}
	}
}
