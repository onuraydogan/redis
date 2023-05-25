namespace ykbredis.core.data
{
    public interface IEntity
    {
        long Id { get; set; }
        bool IsActive { get; set; }
        bool IsDeleted { get; set; }
        DateTime CreateDate { get; set; }
        long CreateUserID { get; set; }
        DateTime? UpdateDate { get; set; }
        long? UpdateUserID { get; set; }
    }
}
