using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLRandomDataGenerator
{
    public class NameAddressEntry
    {
        public enum SexValues
        {
            Unknown,
            Male,
            Female
        };

        #region Properties
        public string Name { get; private set; }
        public string ZipCode { get; private set; }
        public string StreetName { get; private set; }
        public SexValues Sex { get; private set; }
        public int StreetNumber { get; private set; }
        public int Age { get; private set; }
        #endregion

        #region Constructor

        public NameAddressEntry(string name, string zip, string streetName, SexValues sex, int streetNumber, int age)
        {
            this.Age = age;
            this.Sex = sex;
            this.Name = name;
            this.StreetName = streetName;
            this.StreetNumber = streetNumber;
            this.ZipCode = zip;
        }
        #endregion

        public override string ToString()
        {
            return ToString(";");
        }

        public string ToString(string seperator)
        {
            return Name + seperator + StreetName + seperator + StreetNumber + seperator + ZipCode + seperator + SexString(Sex) + seperator + Age;
        }

        #region Private methods

        private string SexString(SexValues sex)
        {
            switch (sex)
            {
                case SexValues.Female:
                    return "Female";
                case SexValues.Male:
                    return "Male";
                default:
                    return "Not quite sure...";
            }
        }
        #endregion
    }
}
