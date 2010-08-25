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
	
	[Subject(typeof(Calculator))]
	public class When_two_numbers_are_divided
	{
		static Calculator Calculator;
		static int Result;

		Establish context = () => { Calculator = new Calculator(); };

		Because of = () => { Result = Calculator.Divide(40, 2); };

		It should_divide_the_left_operand_with_the_right_operand =
			() => Result.ShouldEqual(20);
	}
}
