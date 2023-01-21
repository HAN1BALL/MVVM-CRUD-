using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_mvvm_.Model
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public int MaxStaff { get; set; }
        public List<Staff> _Staff { get; set;}
        public int OfficeId { get; set; }
        public virtual Office Office { get; set; }
        [NotMapped]
        public Office PositionOffice
        {
            get
            {
                return DataWorker.GetOfficeById(OfficeId);
            }
        }
        [NotMapped]
        public List<Staff> PositionStaff
        {
            get
            {
                return DataWorker.GetAllStaffByPositionId(Id);
            }
        }
    }
}
