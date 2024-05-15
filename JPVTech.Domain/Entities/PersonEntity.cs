using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JPVTech.Domain.Entities
{
    public class PersonEntity : BaseEntity
    {
        public string? Name { get; set; }
        public string? CPF { get; set; }
        public decimal IncomeValue { get; set; }
        public DateTime DateBirth { get; set; }

    }
}
