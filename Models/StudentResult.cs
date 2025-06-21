using System.ComponentModel.DataAnnotations;

namespace StudentCourseResults.Models
{
    public enum ResultStatus
    {
        NeedsImprovement,
        Good,
        Excellent
    }

    public class StudentResult
    {
        public int Id { get; set; }

        [Required]
        public string StudentName { get; set; }

        [Required]
        public string CourseTitle { get; set; }

        [Range(0, 100)]
        public int TotalMarks { get; set; }

        public ResultStatus Status { get; set; }
    }
}