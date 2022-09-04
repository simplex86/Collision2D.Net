using System;

namespace SimpleX.Collision2D.App
{
    static class GMT
    {
        // 时间起始
        public static readonly DateTime zero = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        
        // 当前时间
        public static long now
        {
            get
            {
                var ts = DateTime.UtcNow - GMT.zero;
                return Convert.ToInt64(ts.TotalMilliseconds);
            }
        }
    }
}
