using Machine.Specifications;

namespace Calculator
{
	[Subject(typeof(Calculator))]
	public class When_two_numbers_are_added
	{
		static Calculator Calculator;
		static int Result;

		Establish context = () => { Calculator = new Calculator(); };

		Because of = () => { Result = Calculator.Add(40, 2); };

		It should_add_both_operands =
			() => Result.ShouldEqual(42);
	}
}