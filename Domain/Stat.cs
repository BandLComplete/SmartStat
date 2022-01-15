using System;

namespace Domain
{
    /// <summary>
    /// Ключ складывается из User+Date+Name
    /// Unit необязателен
    /// </summary>
    public class Stat
    {
	    
	    public string? User { get; set; }

	    public DateTime Date
	    {
		    get => date;
		    set => date = value.SetUtcKind();
	    }
	    private DateTime date;

	    public string? Name { get; set; }
        public int Value { get; set; }
        public string? Unit { get; set; }
    }
}