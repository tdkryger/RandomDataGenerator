using System;
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
    }
}
