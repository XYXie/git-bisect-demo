using Machine.Specifications;

namespace Calculator
{
	[Subject(typeof(Calculator))]
	public class When_two_numbers_are_subtracted
	{
		static Calculator Calculator;
		static int Result;

		Establish context = () => { Calculator = new Calculator(); };

		Because of = () => { Result = Calculator.Subtract(40, 2); };

		It should_subtract_the_right_operand_from_the_left_operand =
			() => Result.ShouldEqual(38);
	}
}
