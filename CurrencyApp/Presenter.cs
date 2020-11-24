using System;
using System.Data;
using System.Linq;
using ModelLibrary;
using ViewInterface;

namespace CurrencyApp
{
    // класс представителя
    public class Presenter
    {
        IView View; // ссылка на представление
        IModel Model; // ссыла на модель

        public Presenter(IView View, IModel Model)
        {
            this.View = View;
            this.Model = Model;

            // создание обработчиков событий представления
            View.Update += View_Update;
            View.CurrencyIdentificator += View_CurrencyIdentificator;
            View.CurrencyValue += View_CurrencyValue;

            // создания обработчиков событий модели
            Model.ProgressChanged += Model_ProgressChanged;
        }


        // обработчик события представления на обновления таблиц БД
        private void View_Update(object sender, UpdateEventArgs e)
        {
            Model.Update(); // вызов метода модели, обновляющий таблицы БД

            // возвращение представителю списка названий валют через поле объекта e, хранящего данные события
            e.Currencies = Model.Currencies.Select(x => x.RusName).ToList();

            // сортировка списка названий валют
            e.Currencies.Sort();
        }


        // обработчик события представления на получение параметра валюты по её названию
        private void View_CurrencyIdentificator(object sender, CurrencyIdentificatorEventAgs e)
        {
            // если в качестве параметра валюты задано название валюты, 
            // то возвращаем значение поля объекта e, хранящего название валюты
            if (e.Identificator == CurrencyIdentificator.RusName)
                e.IdentificatorValue = e.RusName;
            else
            {
                var Currencies = Model.Currencies; // список валют, предоставляемый моделью через одноимённое свойство
                // поиск валюты с заданным именем;
                var Currency = Model.Currencies.First(x => x.RusName == e.RusName);

                // возвращаем значение параметра валюты через соответсвующее поле объекта e
                if (e.Identificator == CurrencyIdentificator.Id)
                    e.IdentificatorValue = Currency.Id.ToString();
                else if (e.Identificator == CurrencyIdentificator.EngName)
                    e.IdentificatorValue = Currency.EngName;
                else if (e.Identificator == CurrencyIdentificator.NumCode)
                    e.IdentificatorValue = Currency.NumCode.HasValue ? Currency.NumCode.ToString() : "";
                else if (e.Identificator == CurrencyIdentificator.CharCode)
                    e.IdentificatorValue = Currency.CharCode != null ? Currency.CharCode : "";
            }
        }


        // обработчик события представления на получение курса валюты
        private void View_CurrencyValue(object sender, CurrencyValueEventArgs e)
        {
            // возвращаем курс валюты через соответсвующее поле объекта e, определяемый посредством вызова требуемого метода модели
            if (e.Identificator == CurrencyIdentificator.Id)
                e.Value = Model.GetCurrencyValueById(e.IdentificatorValue, e.dateTime);
            else if (e.Identificator == CurrencyIdentificator.RusName)
                e.Value = Model.GetCurrencyValueByRusName(e.IdentificatorValue, e.dateTime);
            else if (e.Identificator == CurrencyIdentificator.EngName)
                e.Value = Model.GetCurrencyValueByEngName(e.IdentificatorValue, e.dateTime);
            else if (e.Identificator == CurrencyIdentificator.CharCode)
                e.Value = Model.GetCurrencyValueByCharCode(e.IdentificatorValue, e.dateTime);
            else
            {
                int NumCode;
                try
                {
                    NumCode = Convert.ToInt32(e.IdentificatorValue);
                }
                catch
                {
                    throw new Exception("Числовой код должен содержать только цифры");
                }
                e.Value = Model.GetCurrencyValueByNumCode(NumCode, e.dateTime);
            }
        }


        // обработчик события модели на изменение прогресса обновления таблиц БД
        private void Model_ProgressChanged(int Progress)
        {
            // отображение прогресса обновления таблиц БД посредством вызова соответствующего метода представления
            View.SetProgress(Progress);
        }
    }
}