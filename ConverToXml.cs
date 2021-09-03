using System.Collections.Generic;
using System.Xml.Linq;

namespace xmlConveter
{
    /// <summary>
    /// Class used to create the xml file. First param in constructor is path to 
    /// row based file. Second param is xml filename. 
    /// </summary>
    public class ConvertToXml
    {
        private List<Person> _people;
        private XElement _xElement;
        private XElement _xPerson;
        private string _outputFileName;

        public ConvertToXml(string path, string output)
        {
            _people = new People(path).GetPeople();
            _outputFileName = output;
            converter();
            createXmlFile();
        }


        /// <summary>
        /// Convert thea rowbased file to xml with root element people
        /// </summary>
        private void converter()
        {
            
            _xElement = new XElement("people");
            _people.ForEach(person => {
                _xPerson = new XElement("person");
                _xPerson.Add(new XElement("firstname", person.FirstName));
                _xPerson.Add(new XElement("lastname", person.LastName));
                
                if(person.Address != null)
                    handlePersonAddress(person);
                
                if(person.Phone != null)
                    handlePersonPhone(person);
                
                if(person.Family != null) 
                    handlePersonFamily(person);

                _xElement.Add(_xPerson);
            });
        }

        ///<summary>    
        /// Handle a persons address details and creates associated xml elements
        ///</summary>
        private void handlePersonAddress(Person person)
        {
            XElement address = new XElement("address");
            if(person.Address.Street != null)
                address.Add(new XElement("street", person.Address.Street));
            if(person.Address.City != null)
                address.Add(new XElement("city", person.Address.City));
            if(person.Address.PostalCode != null)
                address.Add(new XElement("postalcode", person.Address.PostalCode));
            _xPerson.Add(address);
        }

        ///<summary>    
        /// Handle a persons phone details and creates associated xml elements
        ///</summary>
        private void handlePersonPhone(Person person)
        {
            XElement phone = new XElement("phone");
            if(person.Phone.Mobile != null)
                phone.Add(new XElement("mobile", person.Phone.Mobile));
            if(person.Phone.LandLine != null)
                phone.Add(new XElement("landline", person.Phone.Mobile));
            _xPerson.Add(phone);
        }

        ///<summary>    
        /// Handle a persons family details and creates associated xml elements
        /// Family members can be none to many
        ///</summary>
        private void handlePersonFamily(Person person)
        {
            person.Family.ForEach( fMember => {
                XElement family = new XElement("family");
                if(fMember.Name != null)
                    family.Add(new XElement("name", fMember.Name));
                if(fMember.Born != null)
                    family.Add(new XElement("born", fMember.Born));
                if(fMember.Address != null)
                {
                    XElement familyAddress = new XElement("address");
                    if(fMember.Address.Street != null)
                        familyAddress.Add(new XElement("street", fMember.Address.Street));
                    if(fMember.Address.City != null)
                        familyAddress.Add(new XElement("city", fMember.Address.City));
                    if(fMember.Address.PostalCode != null)
                        familyAddress.Add(new XElement("postalcode", fMember.Address.PostalCode));
                    family.Add(familyAddress);
                }
                _xPerson.Add(family);
            });
        }

        ///<summary>    
        /// Save and create xml file created from a rowbased file
        ///</summary>
        public void createXmlFile()
        {
            _xElement.Save(_outputFileName);
        }
    }
}