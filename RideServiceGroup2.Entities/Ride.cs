using System;
using System.Collections.Generic;

namespace RideServiceGroup2.Entities
{
    public class Ride
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public string Description { get; set; }
        public RideCategory Category { get; set; }
        public Status Status
        {
            get
            {
                if (Reports.Count > 0)
                {
                    return Reports[0].Status;
                }
                else
                {
                    return Status.Working;
                }
            }
        }
        public List<Report> Reports { get; set; } = new List<Report>();
        public int NumberOfShutdowns()
        {
            int numberOfShutDowns = 0;
            foreach (Report report in Reports)
            {
                if (report.Status == Status.Broken)
                {
                    numberOfShutDowns++;
                }
            }
            return numberOfShutDowns;
        }
        public int DaysSinceLastShutdown()
        {
            DateTime lastShutdown = new DateTime();
            foreach (Report report in Reports)
            {
                if (report.Status == Status.Broken)
                {
                    lastShutdown = report.ReportTime;
                    break;
                }
            }
            TimeSpan timeSinceLastShutdown = DateTime.Now - lastShutdown;
            return timeSinceLastShutdown.Days;
        }
        public string GetShortDescription()
        {
            return Description.Substring(0, 50) + "...";
        }

        public string TranslateStatus()
        {
            string status = "";
            switch(Status)
                {
                    case Status.Working:
                        status = "Virker";
                break;
                    case Status.Broken:
                        status = "Virker ikke";
                break;
                    case Status.BeingRepaired:
                        status = "Under reperation";
                break;
            }
            return status;
        }
    }
}
