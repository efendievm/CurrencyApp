using System;
using System.Collections.Generic;

namespace ViewInterface
{
    public interface IView // интерфейс представления
    {
        // метод, устанавливающий прогресс
        void SetProgress(int Progress);

        // событие на обновление таблиц БД (возникает при каждом запуске приложения)
        event EventHandler<UpdateEventArgs> Update;

        // событие на получение параметра валюты по её названию
        event EventHandler<CurrencyIdentificatorEventAgs> CurrencyIdentificator;

        // событие на получение курса валюты
        event EventHandler<CurrencyValueEventArgs> CurrencyValue;
    }


    // класс, содержащий данные события Update
    public class UpdateEventArgs : EventArgs
    {
        public List<string> Currencies { get; set; } // названия валют, хранящихся в таблице БД
    }


    // класс, содержащий данные события CurrencyIdentificator
    public class CurrencyIdentificatorEventAgs : EventArgs
    {
        public string RusName { get; private set; } // параметр, определяющий валюту
        public CurrencyIdentificator Identificator { get; private set; } // название параметра, определяющего валюту
        public string IdentificatorValue { get; set; } // информация о валюте
        public CurrencyIdentificatorEventAgs(string RusName, CurrencyIdentificator Identificator)
        {
            this.RusName = RusName;
            this.Identificator = Identificator;
        }
    }


    // класс, содержащий данные события CurrencyValue
    public class CurrencyValueEventArgs : EventArgs
    {
        public CurrencyIdentificator Identificator { get; private set; } // параметр, определяющий валюту
        public string IdentificatorValue { get; private set; } // значение параметра, определющего валюту
        public DateTime dateTime { get; private set; } // дата, на которую необходимо определить курс валюты
        public double Value { get; set; } // курс валюты
        public CurrencyValueEventArgs(
            CurrencyIdentificator Identificator,
            string IdentificatorValue,
            DateTime dateTime)
        {
            this.Identificator = Identificator;
            this.IdentificatorValue = IdentificatorValue;
            this.dateTime = dateTime;
        }
    }


    // перечисление параметров, определяющие валюту
    public enum CurrencyIdentificator
    {
        Id, RusName, EngName, NumCode, CharCode
    }
}