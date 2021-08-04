namespace DevEdu.Core.Models
{
    public abstract class BaseDto
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}