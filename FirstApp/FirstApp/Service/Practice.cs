using System;

namespace FirstApp.Service
{
    [Serializable]
    public class Practice
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string[] Users { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Length { get; set; }
        public string Place { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        public TimeSpan? Notification { get; set; }
    }
}