using System.Collections.Generic;
using System.IO;
using System;

namespace xmlConveter {
    /// <summary>
    /// Class used to create a list of People containing Person objects from a
    /// rowbased file path provided in constructor.  
    /// </summary>
    public class People {

        private string _path;
        private string _row;
        private bool _personAdded;
        private Person _person;
        private Family _family;
        private List<Person> _people; 
        private StreamReader _sr;
        private string[] _columns;
        public People(string path) 
        {
            _path = path;
            _person = new Person();
            _person.Family = new List<Family>();
            _people = new List<Person>();
            _personAdded = false;
            populatePeople();
        }

        /// <summary>
        /// Using a StreamReader to read each line in inputfile and with the  
        /// first index on each line decide what action to take.
        /// </summary>
        /// <exception cref="The File could not be read"></exception>
        private void populatePeople() 
        {
            try {
                using(_sr = new StreamReader(_path)) 
                {
                    while((_row = _sr.ReadLine()) != null)
                    {
                        _columns = _row.Split("|");
                        switch(_columns[0])
                        {
                            case "P":
                                if(String.IsNullOrEmpty(_person.FirstName))
                                {
                                    handlePerson();
                                }
                                else
                                {
                                    addPerson();
                                }
                                break;
                            case "T":
                                handlePhone();
                                break;
                            case "A":
                                handleAddress();
                                break;
                            case "F":
                                handleFamily();
                                _personAdded = false;
                                break;
                        }
                    }
                    _people.Add(_person);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("File could not be read");
                
            }
        }

        ///<summary>
        /// Add the current _person to the _people list, creates a new person
        /// and starts to handle that person
        ///</summary>
        private void addPerson()
        {
            _people.Add(_person);
            _person = new Person();
            _person.Family = new List<Family>();
            _family = new Family();
            handlePerson();
        }

        ///<summary>
        /// Set firstname and lastname to _person
        ///</summary>
        private void handlePerson()
        {
            switch(_columns.Length)
            {
                case 3:
                    _person.FirstName = _columns[1];
                    _person.LastName = _columns[2];
                    return;
                case 2:
                    _person.FirstName = _columns[1];
                    return;
                default:
                    return;
            }
            
        }

        ///<summary>
        /// Set phone details to _person
        ///</summary>
        private void handlePhone()
        {
            switch(_columns.Length)
            {
                case 3:
                    _person.Phone = new Phone(){
                        Mobile = _columns[1],
                        LandLine =_columns[2]
                    };
                    return;
                case 2:
                    _person.Phone = new Phone(){
                        Mobile = _columns[1]
                    };
                    return;
                default:
                    return;
            }
        }

        ///<summary>
        /// Set address details to _person
        ///</summary>
        private void handleAddress()
        {
            switch(_columns.Length)
            {
                case 4:
                    _person.Address = new Address(){
                        Street = _columns[1],
                        City = _columns[2],
                        PostalCode = int.Parse(_columns[3])
                    };
                    return;
                case 3:
                    _person.Address = new Address(){
                        Street = _columns[1],
                        City = _columns[2]
                    };
                    return;
                case 2:
                    _person.Address = new Address(){
                        Street = _columns[1]
                    };
                    return;
                default:
                    return;
            }
        }

        ///<summary>
        /// Check if person has any family details, if so set family details
        /// to in _family and continue to handle a family members details.
        ///</summary>
        private void handleFamily()
        {
            _family = new Family();    
            switch(_columns.Length)
            {
                case 3:
                    _family.Name = _columns[1];
                    _family.Born = int.Parse(_columns[2]);
                    break;
                case 2:
                    _family.Name = _columns[1];
                    break;
                default:
                    return;
            }
            handleFamilyProperty();
            if(!String.IsNullOrEmpty(_family.Name))
                _person.Family.Add(_family);
        }

        ///<summary>
        /// Set phone and address details for a family member.
        /// Read new line from input file and if first index is P new person handeling
        /// is started. If F a new family member is added to _person.
        ///</summary>
        private void handleFamilyProperty() 
        {
            
            int counter = 0;
            while(!_personAdded && counter < 2)
            {
                if((_row = _sr.ReadLine()) != null)
                {
                    _columns = _row.Split("|");
                    switch(_columns[0])
                    {
                        case "P":
                            _person.Family.Add(_family);
                            addPerson();
                            _personAdded = true;
                            return;
                        case "F":
                            _person.Family.Add(_family);
                            handleFamily();
                            break;
                        case "T":
                            handleFamilyPhone();
                            counter++;
                            break;
                        case "A":
                            handleFamilyAddress();
                            counter++;
                            break;
                        default:
                            return;
                    }
                }
            }
        }

        ///<summary>
        /// Set phone details to _family
        ///</summary>
        private void handleFamilyPhone()
        {
            switch(_columns.Length)
            {
                case 3:
                    _family.Phone = new Phone(){
                        Mobile = _columns[1],
                        LandLine = _columns[2]
                    };
                    return;
                case 2:
                    _family.Phone = new Phone(){
                        Mobile = _columns[1]
                    };
                    return;
                default:
                    return;
            }
        }

        ///<summary>
        /// Set address details to _family
        ///</summary>
        private void handleFamilyAddress()
        {
            switch(_columns.Length)
            {
                case 4:
                    _family.Address = new Address(){
                        Street = _columns[1],
                        City = _columns[2],
                        PostalCode = int.Parse(_columns[3])
                    };
                    return;
                case 3:
                    _family.Address = new Address(){
                        Street = _columns[1],
                        City = _columns[2]
                    };
                    return;
                case 2:
                    _family.Address = new Address(){
                        Street = _columns[1]
                    };
                    return;
                default:
                    return;
            }
        }
        
        ///<summary>
        /// Returns a list contining Person's. 
        ///</summary>
        public List<Person> GetPeople() 
        {
            return _people;
        }
    }
}