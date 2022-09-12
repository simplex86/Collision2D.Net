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

    // 变化时间
    public class DeltaTime
    {
        private long previous = 0;

        public void Init()
        {
            previous = GMT.now;
        }

        public float Get()
        {
            var current = GMT.now;
            var deltaTime = (current - previous) / 1000.0f;
            previous = current;

            return deltaTime;
        }
    }
}
