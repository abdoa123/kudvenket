using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kudvenkit.ViewModels
{
    public class EditViewModel : CreateViewModel
    {
        public int Id { get; set; }
        public string photoPath { get; set; }
    }
}
