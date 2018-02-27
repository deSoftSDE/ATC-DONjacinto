using System;

namespace dsASPCAtc.DataAccess
{
    public static class Extensiones
    {
        public static object NullValue(this int? i)
        {
            if (i.HasValue)
            {
                return i;
            }
            else
            {
                return DBNull.Value;
            }
        }
    }
}
