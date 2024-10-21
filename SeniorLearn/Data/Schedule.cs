namespace SeniorLearn.Data
{
    public abstract class Schedule
    {
        public DateTime StartDate { get; protected set; }
        public DateTime EndDate { get; protected set; }
        public Schedule(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
    public abstract class Repeating : Schedule
    {
        public int Occurrences { get; set; } = default;
        public Repeating(DateTime startDate, DateTime endDate) : base(startDate, endDate){}
        public abstract ICollection<DateTime> GenerateScheduledDates();
    }
    public class DailyRepeating : Repeating
    {
        public ICollection<DateTime> ScheduledDates { get; protected set; } = new List<DateTime>();

        public DailyRepeating(DateTime startDate, DateTime endDate, int occurrences)
            : base(startDate, endDate)
        {
            Occurrences = occurrences;
        }
        public override ICollection<DateTime> GenerateScheduledDates()
        {
            if (Occurrences > 0)
            {
                var count = 0;
                var currentDate = StartDate;

                while (count < Occurrences)
                {
                    ScheduledDates.Add(currentDate);
                    currentDate = currentDate.AddDays(1);
                    count++;
                }
            }
            else
            {
                var currentDate = StartDate;
                while (currentDate < EndDate)
                {
                    ScheduledDates.Add(currentDate);
                    currentDate = currentDate.AddDays(1);
                }
            }
            return ScheduledDates;
        }
    }
    public class WeeklyRepeating : Repeating
    {
        public Dictionary<DayOfWeek, bool> ChosenDaysOfWeek { get; protected set; } = new();
        public ICollection<DateTime> ScheduledDates { get; protected set; } = new List<DateTime>();
        public WeeklyRepeating(DateTime startDate, DateTime endDate, int occurrence, IList<DayOfWeek> chosenDays) : base(startDate, endDate)
        {
            Occurrences = occurrence;

            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                ChosenDaysOfWeek[day] = false;
            }
            SetChosenDays(chosenDays);
        }
        public void SetChosenDays(IList<DayOfWeek> chosenDays)
        {
            foreach (var day in chosenDays)
            {
                ChosenDaysOfWeek[day] = true;
            }
        }
        public override ICollection<DateTime> GenerateScheduledDates()
        {
            var currentDate = StartDate;
            var count = 0;

            if (Occurrences > 0)
            {
                while (count < Occurrences)
                {
                    if (ChosenDaysOfWeek[currentDate.DayOfWeek])
                    {
                        ScheduledDates.Add(currentDate);
                        count++;
                    }
                    currentDate = currentDate.AddDays(1);
                }
            }
            else
            {
                while (currentDate <= EndDate)
                {
                    if (ChosenDaysOfWeek[currentDate.DayOfWeek])
                    {
                        ScheduledDates.Add(currentDate);
                    }
                    currentDate = currentDate.AddDays(1);
                }
            }
            return ScheduledDates;
        }
    }
}