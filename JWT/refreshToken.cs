using System.Data;

namespace JWT
{
    public class refreshToken
    {
        public string token {  get; set; } = string.Empty;
        public DateTime created { get; set; } = DateTime.Now;
        public DateTime expired {  get; set; }
    }
}
