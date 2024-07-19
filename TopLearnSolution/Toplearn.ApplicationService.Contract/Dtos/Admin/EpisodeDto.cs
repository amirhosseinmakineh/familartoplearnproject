using Toplearn.Domain.Models;

namespace Toplearn.ApplicationService.Contract.Dtos.Admin
{
    public class EpisodeDto
    {
        public Course Course { get; set; }
        public long EpisodeId { get; set; }
        public string EpisodeTitle { get; set; }
        public bool IsFree { get; set; }
        public TimeSpan Time { get; set; }
    }
}
