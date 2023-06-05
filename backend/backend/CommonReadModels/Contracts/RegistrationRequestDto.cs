using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonReadModels.Contracts;
public class RegistrationRequestDto
{
    public string Email{ get; set; }
    public string Password{ get; set; }
    public string SubmitPassword{ get; set; }
}
