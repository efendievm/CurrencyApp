using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Xml;
using System.Data.SqlTypes;
using TypesLibrary;

namespace Model
{
    public partial class Model
    {
        // метод, добаляющий курсы валют на заданную дату DateTime, если их нет в таблице CurrenciesHistory
        private void UpdateCurrenciesHistoryIfRequired(SqlConnection connection, DateTime DateTime)
        {
            if (DateTime.Date > System.DateTime.Now.Date) // если заданная дата больше текущей, кидаем исключение
                throw new Exception();
            // строка запроса на определение количества записей с заданной датой
            var sqlDateExpression = String.Format(
                    "SELECT COUNT(*) FROM CurrenciesHistory WHERE CurrenciesHistory.Date = '{0}-{1}-{2}'",
                    DateTime.Year, DateTime.Month, DateTime.Day);
            var command = new SqlCommand(sqlDateExpression, connection); // создаём команду
            var RequiredDatesCount = (int)command.ExecuteScalar(); // выполняем команду
            if (RequiredDatesCount == 0) // если нет записей с заданной датой, то добавляем в таблицу CurrenciesHistory курсы валют на заданную дату
                UpdateCurrenciesHistory(connection, DateTime.Date); // см. файл Update.cs
        }


        // Методы, возвращающие курс валюты на определённую дату
        public double GetCurrencyValueById(string Id, DateTime DateTime)
        {
            double Value; // курс валюты
            using (var connection = new SqlConnection(connectionString)) // устанавливаем соединение с БД
            {
                connection.Open(); // открываем соедиение

                UpdateCurrenciesHistoryIfRequired(connection, DateTime.Date); // добавить курсы валют на дату DateTime в случае их отсутствия

                // строка запроса на определения валюты
                var sqlDateExpression = String.Format("SELECT dbo.GetCurrencyValueById('{0}', '{1}-{2}-{3}')", Id, DateTime.Year, DateTime.Month, DateTime.Day);
                var command = new SqlCommand(sqlDateExpression, connection); // создаём команду
                try
                {
                    Value = (double)command.ExecuteScalar(); // выполняем команду
                }
                catch
                {
                    throw new Exception(String.Format("Нет данных о курсе валюты с значением Id = {0} на дату {1}", Id, DateTime.Date.ToString("dd.MM.yyy")));
                }
            }
            return Value;
        }

        public double GetCurrencyValueByRusName(string RusName, DateTime DateTime)
        {
            double Value; // курс валюты
            using (var connection = new SqlConnection(connectionString)) // устанавливаем соединение с БД
            {
                connection.Open(); // открываем соедиение

                UpdateCurrenciesHistoryIfRequired(connection, DateTime.Date); // добавить курсы валют на дату DateTime в случае их отсутствия

                // строка запроса на определения валюты
                var sqlDateExpression = String.Format("SELECT dbo.GetCurrencyValueByRusName('{0}', '{1}-{2}-{3}')", RusName, DateTime.Year, DateTime.Month, DateTime.Day);
                var command = new SqlCommand(sqlDateExpression, connection); // создаём команду
                try
                {
                    Value = (double)command.ExecuteScalar(); // выполняем команду
                }
                catch
                {
                    throw new Exception(String.Format("Нет данных о курсе валюты с значением Name = {0} на дату {1}", RusName, DateTime.Date.ToString("dd.MM.yyy")));
                }
            }
            return Value;
        }

        public double GetCurrencyValueByEngName(string EngName, DateTime DateTime)
        {
            double Value; // курс валюты
            using (var connection = new SqlConnection(connectionString)) // устанавливаем соединение с БД
            {
                connection.Open(); // открываем соедиение

                UpdateCurrenciesHistoryIfRequired(connection, DateTime.Date); // добавить курсы валют на дату DateTime в случае их отсутствия

                // строка запроса на определения валюты
                var sqlDateExpression = String.Format("SELECT dbo.GetCurrencyValueByEngName('{0}', '{1}-{2}-{3}')", EngName, DateTime.Year, DateTime.Month, DateTime.Day);
                var command = new SqlCommand(sqlDateExpression, connection); // создаём команду
                try
                {
                    Value = (double)command.ExecuteScalar(); // выполняем команду
                }
                catch
                {
                    throw new Exception(String.Format("Нет данных о курсе валюты с значением EngName = {0} на дату {1})", EngName, DateTime.Date.ToString("dd.MM.yyy")));
                }
            }
            return Value;
        }

        public double GetCurrencyValueByNumCode(int NumCode, DateTime DateTime)
        {
            double Value; // курс валюты
            using (var connection = new SqlConnection(connectionString)) // устанавливаем соединение с БД
            {
                connection.Open(); // открываем соедиение

                UpdateCurrenciesHistoryIfRequired(connection, DateTime.Date); // добавить курсы валют на дату DateTime в случае их отсутствия

                // строка запроса на определения валюты
                var sqlDateExpression = String.Format("SELECT dbo.GetCurrencyValueByNumCode({0}, '{1}-{2}-{3}')", NumCode, DateTime.Year, DateTime.Month, DateTime.Day);
                var command = new SqlCommand(sqlDateExpression, connection); // создаём команду
                try
                {
                    Value = (double)command.ExecuteScalar(); // выполняем команду
                }
                catch
                {
                    throw new Exception(String.Format("Нет данных о курсе валюты с значением ISO_Num_Code = {0} на дату {1}", NumCode, DateTime.Date.ToString("dd.MM.yyy")));
                }
            }
            return Value;
        }

        public double GetCurrencyValueByCharCode(string CharCode, DateTime DateTime)
        {
            double Value; // курс валюты
            using (var connection = new SqlConnection(connectionString)) // устанавливаем соединение с БД
            {
                connection.Open(); // открываем соедиение

                UpdateCurrenciesHistoryIfRequired(connection, DateTime.Date); // добавить курсы валют на дату DateTime в случае их отсутствия

                // строка запроса на определения валюты
                var sqlDateExpression = String.Format("SELECT dbo.GetCurrencyValueByCharCode('{0}', '{1}-{2}-{3}')", CharCode, DateTime.Year, DateTime.Month, DateTime.Day);
                var command = new SqlCommand(sqlDateExpression, connection); // создаём команду
                try
                {
                    Value = (double)command.ExecuteScalar(); // выполняем команду
                }
                catch
                {
                    throw new Exception(String.Format("Нет данных о курсе валюты с значением ISO_Char_Code = {0} на дату {1}", CharCode, DateTime.Date.ToString("dd.MM.yyy")));
                }
            }
            return Value;
        }
    }
}
