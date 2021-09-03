using System.Collections.Generic;
namespace xmlConveter
{
    public class Person {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Phone Phone {get; set;}
        public Address Address { get; set; }
        public List<Family> Family { get; set; }
    }
}