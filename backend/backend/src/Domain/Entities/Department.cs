using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Domain.BaseDefinitions;

namespace backend.Domain.Entities;
public class Department:ISoftDeletable
{
    public int Id { get; set; }
    //public string CreatorEmail { get; set; }
    public IList<PrefaceTranslation> PrefaceTranslations { get; set; }
    public string Name { get; set; }
    public IList<InternShip> Internships { get; set; }
    private string _managerEmails;
    
    public List<string> ManagerEmails
    {
        get { return _managerEmails.Split(',').ToList(); }
        set { _managerEmails = string.Join(',',value); }
    }

}
