using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ykbredis.core.data;

namespace ykbredis.data.model
{
    public class Car : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [StringLength(50)]
        public string Name{ get; set; }
        [StringLength(3)]
        public string Location{ get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public long? UpdateUserID { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long CreateUserID { get; set; }
        public DateTime CreateDate { get; set; }
    }

}
