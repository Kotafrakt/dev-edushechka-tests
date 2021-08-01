namespace DevEdu.Tests.Models
{
    public abstract class BaseDto
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}