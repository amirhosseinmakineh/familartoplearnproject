namespace Toplearn.Domain.Models
{
    public class Status:BaseEntity<int>
    {
        public Status()
        {
            Statuses = new HashSet<Course>();
        }
        public string Title { get; set; }
        #region Relations
        public ICollection<Course> Statuses { get; set; }
        #endregion
    }
}
