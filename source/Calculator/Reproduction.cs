using Machine.Specifications;

namespace Calculator
{
	// The test reproducing the bug in Calculator.Add()).
	[Subject(typeof(Calculator))]
	public class When_two_numbers_are_added_with_the_broken_implementation
	{
		static Calculator Calculator;
		static int Result;

		Establish context = () => { Calculator = new Calculator(); };

		Because of = () => { Result = Calculator.Add(40, 2); };

		It should_add_both_operands =
			() => Result.ShouldEqual(42);
	}
}
