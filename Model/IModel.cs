using System;
using System.Collections.Generic;
using TypesLibrary;

namespace ModelLibrary
{
    public interface IModel // интерфейс 
    {
        // список валют
        List<Currency> Currencies { get; }

        // метод, обновляющий таблицы БД (с информацией о валютах и их курсах)
        void Update();

        // метод, возвращающий курс валюты с заданным Id на дату DateTime
        double GetCurrencyValueById(string Id, DateTime DateTime);

        // метод, возвращающий курс валюты с заданным именем RusName на дату DateTime
        double GetCurrencyValueByRusName(string RusName, DateTime DateTime);

        // метод, возвращающий курс валюты с заданным именем EngName на дату DateTime
        double GetCurrencyValueByEngName(string EngName, DateTime DateTime);

        // метод, возвращающий курс валюты с заданным кодом NumCode на дату DateTime
        double GetCurrencyValueByNumCode(int NumCode, DateTime DateTime);

        // метод, возвращающий курс валюты с заданным кодом CharCode на дату DateTime
        double GetCurrencyValueByCharCode(string CharCode, DateTime DateTime);

        // событие на изменение прогресса обновления таблиц БД
        event Action<int> ProgressChanged;
    }
}