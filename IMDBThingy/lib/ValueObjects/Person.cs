
namespace IMDBThingy.ValueTypeClasses {

    public class Person {

        public string LastName { get; }
        public string FirstName { get; }


        public Person(string firstName, string lastName) {
            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString() {
            return $"{FirstName} {LastName}";
        }

    }

}