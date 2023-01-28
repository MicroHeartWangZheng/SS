namespace SS.Demo
{
    public class EventIdProvider
    {
        public EventId EventId { get; set; }

        public EventIdProvider()
        {
            string name = DateTime.Now.ToString("yyyyMMddHHmmssfffff") ?? "";
            EventId = new EventId(0, name);
        }
    }
}
