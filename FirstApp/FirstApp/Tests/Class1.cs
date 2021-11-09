using FirstApp.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstApp.Tests
{
    public class Class1 
    {
        public static Practice practies = new Practice()
        {
            Name = "Тренировка в зале",
            Date = new DateTime(2021, 11, 09, 14, 00, 00),
            Length = new TimeSpan(2, 15, 00),
            Place = "Зал",
            Type = "Тренировка на ноги",
            Description = "йлткпйкптдлкйпдьпьйжукдпдйуп ",
            Tag = "#тренировка",
        };
    }
}
