namespace Gym.Models
{
    public class Booking:BaseEntity
    {
        public bool IsAttended { get; set; }=false;
        public DateTime BookingDate { get; set; }=DateTime.Now;
        public int MemberId { get; set; }
        public Member Member { get; set; }
        public int SessionId { get; set; }  
        public Session Session { get; set; }
     
    }
}
