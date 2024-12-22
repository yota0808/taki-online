namespace ConsoleTest {
	internal class Program {
		static void Main(string[] args) {
			List<Person> list = [];
			Person bob = new Person("Bob");
			Person bob2 = new Person("Bob");
			list.Add(bob);
			list.Add(bob2);
			list.Remove(bob2);
			Console.WriteLine(list.Single() == bob2);

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
