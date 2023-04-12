using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;
using System.Windows.Documents;

namespace WpfApp2.Entities
{
    public class Direction
    {
        public int Id { get; set; }

        [MaxLength (50)]
        public string Name { get; set; }

        virtual public List<Event> Event { get; set; }
    }
}