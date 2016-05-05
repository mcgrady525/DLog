using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DLog.Common.Helper
{
    public class DLogConfigHelper
    {
        public static int CacheTimeOutMinutes
        {
            get
            {
                var min = ConfigurationManager.AppSettings["Log.CacheTimeOutMinutes"];

                if (min == null)
                {
                    return 5;
                }
                else
                {
                    return int.Parse(min);
                }
            }
        }
    }
}
