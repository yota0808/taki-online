namespace ConsoleTest {
	internal class Program {
		static void Main(string[] args) {
			new Person("Bob");
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
