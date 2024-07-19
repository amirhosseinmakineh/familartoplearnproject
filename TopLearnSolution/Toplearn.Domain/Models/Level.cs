namespace Toplearn.Domain.Models
{
    public class Level:BaseEntity<int>
    {
        public Level()
        {
            Courses = new HashSet<Course>();
        }
        public string Title { get; set; }
        #region Relations
        public ICollection<Course> Courses { get; set; }
        #endregion
    }
}
