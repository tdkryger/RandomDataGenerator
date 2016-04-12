using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using CLRandomDataGenerator.DTO;

namespace CLRandomDataGenerator
{
    public class DataGenerator : IDisposable
    {
        /*
            Roadnames in denmark from: http://dawa.aws.dk/vejedok
            Import json: http://www.codeproject.com/Tips/397574/Use-Csharp-to-get-JSON-Data-from-the-Web-and-Map-i
        */

        private readonly Random _rnd = new Random(DateTime.Now.Millisecond);
        private readonly List<string> _lastNames = new List<string>();
        private readonly List<string> _firstNamesGirls = new List<string>();
        private readonly List<string> _firstNameBoys = new List<string>();
        private readonly List<string> _zipCodes = new List<string>();

        public DataGenerator()
        {
            string[] linStrings = loadFile(@"SourceData\DanishLastNames.txt");
            foreach (string line in linStrings)
            {
                _lastNames.Add(line);
            }

            linStrings = loadFile(@"SourceData\DanishFirstNames.Girls.txt");
            foreach (string line in linStrings)
            {
                _firstNamesGirls.Add(line);
            }

            linStrings = loadFile(@"SourceData\DanishFirstNames.Boys.txt");
            foreach (string line in linStrings)
            {
                _firstNameBoys.Add(line);
            }
            linStrings = loadFile(@"SourceData\ZipPostBy.txt");
            foreach (string line in linStrings)
            {
                _zipCodes.Add(line);
            }
        }

        /// <summary>
        /// Generates a random id
        /// </summary>
        /// <returns> int between 1 and int.MaxValue</returns>
        public int GenerateRandomId()
        {
            return _rnd.Next(1, int.MaxValue);
        }

        public int GenerateRandomId(int min, int max)
        {
            return _rnd.Next(min, max);
        }

        public string GenerateRandomWord(int numLetters)
        {
            // Make an array of the letters we will use.
            char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

            // Make the words.
            // Make a word.
            string word = string.Empty;
            for (int j = 1; j <= numLetters; j++)
            {
                // Pick a random number between 0 and 25
                // to select a letter from the letters array.
                int letterNum = _rnd.Next(0, letters.Length - 1);

                // Append the letter.
                word += letters[letterNum];
            }
            return word;
        }

        public string GenerateRandomName()
        {
            string randomName = string.Empty;
            if (_rnd.Next(0, 2) == 0)
            {
                int rndValue1 = _rnd.Next(0, _firstNameBoys.Count);
                randomName = _firstNameBoys[rndValue1].Trim();
            }
            else
            {
                int rndValue1 = _rnd.Next(0, _firstNamesGirls.Count);
                randomName = _firstNamesGirls[rndValue1].Trim();
            }
            randomName = randomName + " ";
            int rndValue = _rnd.Next(0, _lastNames.Count);
            randomName = randomName + _lastNames[rndValue].Trim();

            return randomName;
        }

        public string GenerateRandomName(NameAddressEntry.SexValues sex)
        {
            string randomName = string.Empty;

            if (sex == NameAddressEntry.SexValues.Male)
            {
                int rndValue1 = _rnd.Next(0, _firstNameBoys.Count);
                randomName = _firstNameBoys[rndValue1].Trim();
            }
            else
            {
                int rndValue1 = _rnd.Next(0, _firstNamesGirls.Count);
                randomName = _firstNamesGirls[rndValue1].Trim();
            }
            randomName = randomName + " ";
            int rndValue = _rnd.Next(0, _lastNames.Count);
            randomName = randomName + _lastNames[rndValue].Trim();

            return randomName;
        }

        public string GenerateRandomZipCode()
        {
            int randomNumber = _rnd.Next(0, _zipCodes.Count);
            string line = _zipCodes[randomNumber];
            int idx = line.IndexOf(';');
            line = line.Substring(0, idx);
            return line.Trim();
        }

        public NameAddressEntry GenerateNameAddressEntry()
        {
            NameAddressEntry.SexValues sex = NameAddressEntry.SexValues.Unknown;
            if (GenerateRandomId(0, 1) == 0)
                sex = NameAddressEntry.SexValues.Male;
            else
                sex = NameAddressEntry.SexValues.Female;

            string zipCode = GenerateRandomZipCode();

            return new NameAddressEntry(name: GenerateRandomName(sex),
                zip: zipCode,
                streetName: GenerateStreetNamesFromZip(zipCode),
                sex: sex,
                streetNumber: GenerateRandomId(1, 500),
                age: GenerateRandomId(0, 100));
        }

        public Dictionary<int, NameAddressEntry> GenerateNameAddressEntry(int count)
        {
            Dictionary<int, NameAddressEntry> dictionary = new Dictionary<int, NameAddressEntry>();

            for (int i = 1; i <= count; i++)
            {
                dictionary.Add(i, GenerateNameAddressEntry());
            }

            return dictionary;
        }

        /// <summary>
        /// Gets a list of street names from external source based on zip code
        /// Should make a version that saves the resultlist on disc, and use that after, to reduce runtime and load on the dawa server..
        /// </summary>
        /// <param name="zip"></param>
        /// <returns></returns>
        public string GenerateStreetNamesFromZip(string zip)
        {
            string url = string.Format("http://dawa.aws.dk/vejnavne?postnr={0}", zip);

            using (WebClient webClient = new System.Net.WebClient())
            {
                string json = webClient.DownloadString(url);

                List<DTO.DawaRoadName> list = JsonConvert.DeserializeObject<List<DTO.DawaRoadName>>(json);
                if (list.Count != 0)
                {
                    int idx = _rnd.Next(0, list.Count);
                    return list[idx].navn;
                }
                else
                {
                    return GenerateRandomWord(20);
                }
            }
        }


        #region Private methods

        private string[] loadFile(string pathToFile)
        {
            return File.ReadAllLines(pathToFile);
        }

        #endregion

        public void Dispose()
        {
            _lastNames.Clear();
            _firstNameBoys.Clear();
            _firstNamesGirls.Clear();
            _zipCodes.Clear();
        }
    }
}
