namespace TimeApi
{
    public class TimeResult
    {
        public string ApplicationName { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public long ElapsedMs { get; set; }
        public TimeResult Response { get; set; }
    }
}
