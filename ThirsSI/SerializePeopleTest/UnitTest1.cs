using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerializePeople;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace SerializePeopleTest
{
    [TestClass]
    public class UnitTest1 
    {
        Person p = new Person("Bence",new DateTime(1996,09,13),Genders.Male);


        [TestMethod]
        public void ToStringTest()
        {
            Assert.AreEqual("Bence 1996. 09. 13. 0:00:00 Male 22", p.ToString());
        }

        public void SerializeItem()
        {
            string fileName = "dataStuff.myData";
            IFormatter formatter = new BinaryFormatter();
            FileStream s = new FileStream(fileName, FileMode.Create);
            formatter.Serialize(s, p);
            s.Close();
        }


        public Person DeserializeItem()
        {
            string fileName = "dataStuff.myData";
            IFormatter formatter = new BinaryFormatter();
            FileStream s = new FileStream(fileName, FileMode.Open);
            Person p = (Person)formatter.Deserialize(s);
            return p;
        }

        [TestMethod]
        public void SerializeDeserializeTest()
        {
            SerializeItem();
            p = DeserializeItem();
            Assert.AreEqual("Bence 1996. 09. 13. 0:00:00 Male 22", p.ToString());
        }


    }
}
