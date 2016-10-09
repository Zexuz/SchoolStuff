
namespace IMDBThingy.lib.ValueObjects {

    public class Person {

        public string LastName { get; }
        public string FirstName { get; }
        public DateOfBirth DateOfBirth { get; }

        public Person(string firstName, string lastName, DateOfBirth dateOfBirth) {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
        }

        public override string ToString() {
            return $"{FirstName} {LastName} ( {GetType().Name}), born {DateOfBirth}";
        }

    }

}