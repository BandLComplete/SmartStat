using System;

namespace FirstApp.Service
{
    [Serializable]
    public class Practice
    {
        public int Id { get; set; } // ИД тренировки
        public string Name { get; set; } //Название события done
        public string[] Users { get; set; } //Логины done
        public DateTime Date { get; set; } // Дата done
        public TimeSpan Length { get; set; } // Продолжительность тренировки done
        public string Place { get; set; } //Место done
        public string Type { get; set; } // Тип done
        public string Description { get; set; } //Описание тренировки done
        public string Tag { get; set; } // Тэг от маши done
        public TimeSpan? Notification { get; set; } //Уведомление 
    }
}