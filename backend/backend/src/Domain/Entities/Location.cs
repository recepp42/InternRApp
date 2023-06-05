using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Domain.BaseDefinitions;

namespace backend.Domain.Entities;
public class Location:ISoftDeletable
{
    public int Id { get; set; }
    public string City { get; set; }
    public string StreetName { get; set; }
    public int HouseNumber { get; set; }
    public string ZipCode { get; set; }
    public IList<InternShip> InternShips { get; set; }
    public int? CreatorId { get; set; }
    public ApplicationUser? Creator { get; set; }

}
