using System.ComponentModel.DataAnnotations;

namespace InterviewProjectApi.Models
{
    public class Line
    {
        [Key]
        public int Id { get; set; }
        public string LineofString { get; set; }
    }
}
