using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SerializePeople
{
    [Serializable]
    public class Person : IDeserializationCallback, ISerializable
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public Genders Gender { get; set; }

        [NonSerialized]
        int age;


        Person() {}


        public Person(string name, DateTime BirthDate, Genders gender)
        {
            this.Name = name;
            this.BirthDate = BirthDate;
            this.Gender = gender;
            CalculateAge();
        }

        public Person(SerializationInfo info, StreamingContext context)
        {
            this.Name = (string)info.GetValue("Name", typeof(string));
            this.BirthDate = (DateTime)info.GetValue("BirthDate", typeof(DateTime));
            this.Gender = (Genders)info.GetValue("Gender", typeof(Genders));
            CalculateAge();

        }

        public void CalculateAge()
        {
            var today = DateTime.Today;
            var age = today.Year - BirthDate.Year;
            if (BirthDate > today.AddYears(-age)) age--;
            this.age = age;
        }

        public void Serialize()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("MyFile.bin",
                                     FileMode.Create,
                                     FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, this);
            stream.Close();
        }

        public static Person Deserialize()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("MyFile.bin",
                                      FileMode.Open,
                                      FileAccess.Read,
                                      FileShare.Read);
            Person p = (Person)formatter.Deserialize(stream);
            stream.Close();
            return p;
        }


        public override string ToString()
        {
            return this.Name + " " + this.BirthDate + " " + this.Gender + " " + age;
        }

        public void OnDeserialization(object sender)
        {
            CalculateAge();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name, typeof(string));
            info.AddValue("BirthDate", BirthDate, typeof(DateTime));
            info.AddValue("Gender", Gender, typeof(Genders));
        }
    }
}
