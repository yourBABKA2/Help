using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        virtual public Direction Direction { get; set; }
        public DateTime DateTime { get; set; }
        public string Duration { get; set; }
        public Byte[] Photo { get; set; }
    }
}
