using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD_mvvm_.Model
{
    public class Staff
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Phone { get; set; }
        public int PositionId { get; set; }
        public virtual Position Position { get; set; }

        [NotMapped]
        public Position StaffPosition
        {
            get
            {
                return DataWorker.GetPositionById(PositionId);
            }
        }
    }
}
