namespace Time
{
    static class Time
    {
        public static List<Event> Events = new List<Event>();

        public class Event
        {
            public Func<int> callback;
            public int ms;
            public int endtime;

            public Event(Func<int> callback, int ms)
            {
                this.callback = callback;
                this.ms = ms;
                this.endtime = (int)(DateTime.Now.Ticks + ms * 10000);
            }
        }

        public static void Runin(Func<int> function, int ms)
        {
            Events.Add(new Event(function, ms));
        }

        public static void UpdateEvents()
        {
            List<Event> calledEvents = new List<Event>();

            int i = 0;
            foreach (Event ev in Events)
            {
                if ((int)DateTime.Now.Ticks >= ev.endtime)
                {
                    ev.callback();
                    calledEvents.Add(ev);
                }
                i++;
            }

            foreach (Event ev in calledEvents)
            {
                Events.Remove(ev);
            }
        }
    }
}