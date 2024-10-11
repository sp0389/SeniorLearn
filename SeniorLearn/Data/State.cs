using SeniorLearn.Data.Core;

namespace SeniorLearn.Data
{
    public abstract class State
    {
        public abstract void Schedule(Lesson lesson);
        public abstract void Complete(Lesson lesson);
        public abstract void Cancel(Lesson lesson);
        public abstract void Close(Lesson lesson);
    }

    public abstract class TerminalState : State
    {
        public override void Schedule(Lesson lesson) => throw new DomainRuleException("You cannot schedule a lesson in this state!");

        public override void Complete(Lesson lesson) => throw new DomainRuleException("You cannot complete a lesson in this state!");
 
        public override void Cancel(Lesson lesson) => throw new DomainRuleException("You cannot cancel a lesson in this state!");

        public override void Close(Lesson lesson) => throw new DomainRuleException("You cannot close a lesson in this state!");             
    }

    public class Draft : State
    {
        public Draft(Lesson lesson)
        {
            lesson.Availability = Availability.Draft;
        }
        public override void Schedule(Lesson lesson)
        {
            lesson.ManageLessonState(new Scheduled(lesson));
        }
        public override void Complete(Lesson lesson) => throw new DomainRuleException("You cannot complete a lesson in a draft state!");
        public override void Cancel(Lesson lesson)
        {
            lesson.ManageLessonState(new Cancelled(lesson));
        }

        public override void Close(Lesson lesson)
        {
            lesson.ManageLessonState(new Closed(lesson));
        }
    }

    public class Scheduled : State
    {
        public Scheduled(Lesson lesson)
        {
            lesson.Availability = Availability.Scheduled;
        }
        public override void Schedule(Lesson lesson) => throw new DomainRuleException("You cannot schedule a lesson that is in a scheduled state!");
        public override void Complete(Lesson lesson)
        {
            lesson.ManageLessonState(new Completed(lesson));
        }
        public override void Cancel(Lesson lesson)
        {
            lesson.ManageLessonState(new Cancelled(lesson));
        }
        public override void Close(Lesson lesson)
        {
            lesson.ManageLessonState(new Closed(lesson));
        }
    }

    public class Completed : TerminalState
    {
        public Completed(Lesson lesson)
        {
            lesson.Availability = Availability.Complete;
        }
    }

    public class Cancelled : TerminalState
    {
        public Cancelled(Lesson lesson)
        {
            lesson.Availability = Availability.Cancelled;
        }
    }

    public class Closed : TerminalState
    {
        public Closed(Lesson lesson)
        {
            lesson.Availability = Availability.Closed;
        }
    }
}