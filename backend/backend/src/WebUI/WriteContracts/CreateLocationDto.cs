using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CommonReadModels.Contracts;
public class CreateLocationDto
{
    public string city { get; set; }
    public string streetname { get; set; }
    public int housenumber { get; set; }
    public string zipcode { get; set; }

   
}
