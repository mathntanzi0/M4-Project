using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M4_Project.Utilities
{
    /// <summary>
    ///     Define methods that handle date and time.
    /// </summary>
    public static class DateManager
    {
        /// <summary>
        ///     Returns true if two schedules overlap.
        /// </summary>
        public static bool SchedulesOverlap(DateTime start1, TimeSpan duration1, DateTime start2, TimeSpan duration2)
        {
            DateTime end1 = start1 + duration1;
            DateTime end2 = start2 + duration2;
            return start1 <= end2 && start2 <= end1;
        }
    }
}