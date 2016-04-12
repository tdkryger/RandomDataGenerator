using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLRandomDataGenerator.DTO
{
    /*
        "postnumre": [
            {
              "href": "http://dawa.aws.dk/postnumre/2400",
              "nr": "2400",
              "navn": "København NV"
            }
          ],

    */

    class DawaPostnummer
    {
        public string href { get; set; }
        public string nr { get; set; }
        public  string navn { get; set; }
    }
}
