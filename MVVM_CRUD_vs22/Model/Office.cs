using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD_mvvm_.Model
{
    public class Office
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Inn { get; set; }
        public string Address { get; set; }
        public List<Position> Positions { get; set; }
        [NotMapped]
        public List<Position> OfficePositions
        {
            get
            {
                return DataWorker.GetAllPositionsByOfficeId(Id);
            }
        }
    }
}
