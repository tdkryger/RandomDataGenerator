using System;
using System.Collections.Generic;
using System.Diagnostics;
using CLRandomDataGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestRandomDataGenerator
{
    [TestClass]
    public class UnitTestDataGenerator
    {
        // To see Debug.WriteLine run test as debug. (no need for breakpoints)
        [TestMethod]
        public void DataGenerator_GenerateRandomName_Test()
        {
            DataGenerator dataGenerator = new DataGenerator();
            string randomName = dataGenerator.GenerateRandomName();
            Debug.WriteLine(randomName);
            Assert.AreNotEqual(string.Empty, randomName);
        }

        [TestMethod]
        public void DataGenerator_GenerateNameAddressEntry_Test()
        {
            DataGenerator dataGenerator = new DataGenerator();
            NameAddressEntry nameAddressEntry = dataGenerator.GenerateNameAddressEntry();
            Debug.WriteLine(nameAddressEntry.ToString());
            Assert.IsNotNull(nameAddressEntry);
        }

        [TestMethod]
        public void DataGenerator_GenerateNameAddressEntryList_Test()
        {
            int testCount = 10;
            DataGenerator dataGenerator = new DataGenerator();
            Dictionary<int, NameAddressEntry> dictionary = dataGenerator.GenerateNameAddressEntry(testCount);
            for (int i = 1; i <= dictionary.Count; i++)
            {
                Debug.WriteLine(i.ToString() + ";" + dictionary[i].ToString(";"));
            }
            Assert.AreEqual(testCount, dictionary.Count);
        }

        [TestMethod]
        public void DataGenerator_DawaRoad()
        {
            DataGenerator dataGenerator = new DataGenerator();
            string roadName = dataGenerator.GenerateStreetNamesFromZip("4000");
            Debug.WriteLine(roadName);
            
            Assert.IsFalse(string.IsNullOrEmpty(roadName));
        }
    }
}
