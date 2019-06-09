using System;

namespace Kiosk.Models
{
    public class Post: BaseDbObject
    {
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string DateString => Date.ToString("dd.MM.yyyy");
    }
}
