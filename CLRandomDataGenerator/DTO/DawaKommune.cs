using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLRandomDataGenerator.DTO
{
    /*
        "kommuner": [
            {
              "href": "http://dawa.aws.dk/kommuner/0101",
              "kode": "0101",
              "navn": "København"
            }
          ]
    */

    class DawaKommune
    {
        public string href { get; set; }
        public string kode { get; set; }
        public string navn { get; set; }
    }
}
