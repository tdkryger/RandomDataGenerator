using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLRandomDataGenerator.DTO
{
    /*
        {
          "href": "http://dawa.aws.dk/vejnavne/Abrikosvej",
          "navn": "Abrikosvej",
          "postnumre": [
            {
              "href": "http://dawa.aws.dk/postnumre/2400",
              "nr": "2400",
              "navn": "København NV"
            }
          ],
          "kommuner": [
            {
              "href": "http://dawa.aws.dk/kommuner/0101",
              "kode": "0101",
              "navn": "København"
            }
          ]
        }

    */

    class DawaRoadName
    {
        public string href { get; set; }
        public string navn { get; set; }
        public List<DawaPostnummer> postnumre { get; set; }
        public List<DawaKommune> kommuner { get; set; }

        public DawaRoadName()
        {
            postnumre= new List<DawaPostnummer>();
            kommuner=new List<DawaKommune>();
        }
    }
}
