namespace SeniorLearn.Data.Schedule
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
        public List<DayOfWeek> ChosenDays { get; set; }

        public WeeklyRepeating(DateTime startDate, DateTime endDate, int occurrences, List<DayOfWeek> chosenDays)
            : base(startDate, endDate, occurrences)
        {
            ChosenDays = chosenDays;
        }

        public override ICollection<DateTime> GenerateSchedule()
        {
            var scheduledDays = new List<DateTime>();
            var count = 0;
            var currentDate = StartDate;

            while (count < Occurrences)
            {
                if (ChosenDays.Contains(currentDate.DayOfWeek))
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
