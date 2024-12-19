namespace ConsoleTest {
	internal class Program {
		static void Main(string[] args) {
			TakiGame game = new TakiGame(5);

			Console.Write("End of program");
			Console.ReadKey();
		}
	}

	class Person {
		public readonly string Name;

		public Person(string name) {
			Name = name;
			Console.WriteLine($"Hi I'm {Name}");
		}
	}
}
