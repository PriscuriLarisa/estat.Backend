using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStat.Library.Helpers
{
    public static class DateHelper
    {
        public static DateTime GetRandomDate()
        {
            var randomTest = new Random();
            DateTime endDate = new DateTime(2000, 1, 1);
            DateTime startDate = new DateTime(1980, 1, 1);

            TimeSpan timeSpan = endDate - startDate;
            TimeSpan newSpan = new TimeSpan(0, randomTest.Next(0, (int)timeSpan.TotalMinutes), 0);
            return startDate + newSpan;
        }
    }
}
