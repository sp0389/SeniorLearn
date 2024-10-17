namespace SeniorLearn.Data
{
    public abstract class Schedule
    {
        public DateTime StartDate { get; protected set; }
        public DateTime EndDate { get; protected set; }
        public int Occurrences { get; set; } = 0;

        protected Schedule(DateTime startDate, DateTime endDate, int occurrences)
        {
            StartDate = startDate;
            EndDate = endDate;
            Occurrences = occurrences;
        }

        public abstract ICollection<DateTime> GenerateSchedule();
    }

    public class OneTime : Schedule
    {
        public OneTime(DateTime startDate, DateTime endDate) : base(startDate, endDate, 1) { }

        public override ICollection<DateTime> GenerateSchedule()
        {
            return new List<DateTime> { StartDate };
        }
    }

    public class DailyRepeating : Schedule
    {
        public DailyRepeating(DateTime startDate, DateTime endDate, int occurrences)
            : base(startDate, endDate, occurrences) { }

        public override ICollection<DateTime> GenerateSchedule()
        {
            var scheduledDays = new List<DateTime>();
            var count = 0;
            var currentDate = StartDate;

            while (count < Occurrences)
            {
                scheduledDays.Add(currentDate);
                currentDate = currentDate.AddDays(1);
                count++;
            }

            return scheduledDays;
        }
    }

    public class WeeklyRepeating : Schedule
    {
        public Dictionary<DayOfWeek, bool> DaysOfWeek { get; protected set; } = new();

        public WeeklyRepeating(DateTime startDate, DateTime endDate, int occurrences, IList<DayOfWeek> chosenDays)
            : base(startDate, endDate, occurrences)
        {
            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                DaysOfWeek[day] = false;
            }

            foreach (var day in chosenDays)
            {
                DaysOfWeek[day] = true;
            }
        }

        public override ICollection<DateTime> GenerateSchedule()
        {
            var scheduledDays = new List<DateTime>();
            var count = 0;
            var currentDate = StartDate;

            // Move to the first selected day after the start date
            while (!DaysOfWeek[currentDate.DayOfWeek])
            {
                currentDate = currentDate.AddDays(1);
            }

            while (count < Occurrences && currentDate <= EndDate)
            {
                if (DaysOfWeek[currentDate.DayOfWeek])
                {
                    scheduledDays.Add(currentDate);
                    count++;
                }
                currentDate = currentDate.AddDays(1);
            }

            return scheduledDays;
        }
    }
}
