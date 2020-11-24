using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Xml;
using System.Data.SqlTypes;
using TypesLibrary;

namespace ModelLibrary
{
    public class Model : IModel // класс, реализующий интерфейс IModel
    {
        // строка подключения к БД
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\Desktop\CurrencyApp\Model\DB.mdf;Integrated Security=True";


        // Список валют
        public List<Currency> Currencies { get; private set; }


        // заполнение таблицы Currencies информацией о валютах
        private void FillCurrenciesTable()
        {
            using (var connection = new SqlConnection(connectionString)) // устанавливаем соединение с БД
            {
                connection.Open(); // открываем соединение

                string sqlExpression = "SELECT COUNT(*) FROM Currencies"; // строка запроса на количество записей в таблице Currencies 
                var commandRowCount = new SqlCommand(sqlExpression, connection); // создаём команду
                int Count = (int)commandRowCount.ExecuteScalar(); // выполняем команду

                if (Count == 0) // если записей нет, заполняем таблицу Currencies
                {
                    sqlExpression = "AddCurrency"; // строка запроса на добавление записи в таблицу Currencies

                    var xmlDoc = new XmlDocument();
                    xmlDoc.Load("http://www.cbr.ru/scripts/XML_valFull.asp"); // загрузка с сайта XML документа с информацией о валютах
                    var Root = xmlDoc.ChildNodes.Find("Valuta"); // узел Valuta

                    foreach (var node in Root.ChildNodes.Cast<XmlNode>()) // node -- текущий узел Item с информацией о валюте
                    {
                        var nodeContent = node.ChildNodes; // список дочерних узлов узла Item, содержащие информацию о валюте
                        var command = new SqlCommand(sqlExpression, connection); // создаём команду
                        command.CommandType = CommandType.StoredProcedure; // указываем, что комманда представляет хранимую процедуру

                        // Передём параметры
                        command.Parameters.AddWithValue("@Id", node.Attributes["ID"].InnerText);
                        command.Parameters.AddWithValue("@RusName", nodeContent.Find("Name").InnerText);
                        command.Parameters.AddWithValue("@EngName", nodeContent.Find("EngName").InnerText);
                        command.Parameters.AddWithValue("@Nominal", Convert.ToInt32((string)nodeContent.Find("Nominal").InnerText));

                        // Передаём параметр ISO_Num_Code с учётом возможности отсутствия соответсвуюзей информации в xml документе
                        // ( случаи { ID = "R01436", Name = "Литовский талон" } и { ID = "R01720A", Name = "Украинский карбованец" }
                        command.Parameters.Add(new SqlParameter("@ISO_Num_Code", SqlInt32.Null));
                        var ISO_Num_Code_NodeText = nodeContent.Find("ISO_Num_Code").InnerText;
                        if (ISO_Num_Code_NodeText != "")
                            command.Parameters["@ISO_Num_Code"].Value = Convert.ToInt32(ISO_Num_Code_NodeText);

                        // Аналогично с парметром ISO_Char_Code
                        command.Parameters.Add(new SqlParameter("@ISO_Char_Code", SqlString.Null));
                        var ISO_Char_Code_NodeText = nodeContent.Find("ISO_Char_Code").InnerText;
                        if (ISO_Char_Code_NodeText != "")
                            command.Parameters["@ISO_Char_Code"].Value = ISO_Char_Code_NodeText;
                        else
                            command.Parameters["@ISO_Char_Code"].Value = System.DBNull.Value;

                        command.ExecuteNonQuery(); // вызываем команду
                    }
                }
            }
        }


        // обновление курсов валют CurrenciesHistory на заданную дату
        private void UpdateCurrenciesHistory(SqlConnection connection, DateTime DateTime)
        {
            var sqlExpression = "AddCurrencyValue"; // запрос на добаление курса валюты

            var xmlDoc = new XmlDocument();
            // загрузка с сайта XML документа с информацией курсе валют
            xmlDoc.Load(String.Format("http://www.cbr.ru/scripts/XML_daily.asp?date_req={0}", DateTime.ToString("dd.MM.yyy")));
            var Root = xmlDoc.ChildNodes.Find("ValCurs"); // Узел ValCurs"

            foreach (var node in Root.ChildNodes.Cast<XmlNode>()) // node -- текущий узел Valute с информацией о курсе валюты
            {
                var nodeContent = node.ChildNodes; // список дочерних узлов узла Item, содержащие информацию о валюте
                var command = new SqlCommand(sqlExpression, connection); // создаём команду
                command.CommandType = CommandType.StoredProcedure; // указываеи, что комманда представляет хранимую процедуру

                // Передаём параметры
                command.Parameters.AddWithValue("@Date", DateTime.Date);
                command.Parameters.AddWithValue("@CurrencyId", node.Attributes["ID"].InnerText);
                command.Parameters.AddWithValue("@Value", Convert.ToDouble(nodeContent.Find("Value").InnerText));

                command.ExecuteNonQuery(); // вызываем команду
            }
        }


        // обновление курсов валют CurrenciesHistory
        private void UpdateCurrenciesHistory()
        {
            var dateTime = DateTime.Now; // сегодняшняя дата
            object LatestDataObj; // объект, хранящий последнюю дату в таблице курсов валют CurrenciesHistory
            var DateTimes = new List<DateTime>(); // список дат между последней датой в таблице CurrenciesHistory и сегодняшней датой

            using (var connection = new SqlConnection(connectionString)) // устанавливаем соединение с БД
            {
                connection.Open(); // открываем соединение

                string sqlExpression = "SELECT MAX(Date) FROM CurrenciesHistory"; // строка запроса на получение последней даты в таблице CurrenciesHistory
                SqlCommand command = new SqlCommand(sqlExpression, connection); // создаём команду
                LatestDataObj = command.ExecuteScalar(); // выполняем команду

                DateTime StartDate; // дата, начиная с которой (не влючая) будем добалять курсы валют в таблицу CurrenciesHistory 
                if (DBNull.Value == LatestDataObj) // если command вернула null, т.е. в таблице CurrenciesHistory нет записей
                    StartDate = dateTime.AddDays(-1).Date; // начнём заполнять таблицу со вчерашней даты 
                else
                    StartDate = ((DateTime)LatestDataObj).Date; // иначе присвоим переменной StartDate последнюю дату в таблице CurrenciesHistory

                // заполняем список дат между начальной датой StartDate (не включая)  и сегодняшней датой
                while (dateTime.Date > StartDate)
                {
                    StartDate = StartDate.AddDays(1);
                    DateTimes.Add(StartDate);
                }

                sqlExpression = "AddCurrencyValue"; // запрос на добаление курса валюты
                for (int i = 0; i < DateTimes.Count; i++)
                {
                    UpdateCurrenciesHistory(connection, DateTimes[i]);
                    ProgressChanged((int)(i * 100.0 / DateTimes.Count)); // сообщаем о изменении прогресса
                }
                ProgressChanged(100); // сообщаем об окончании обновления таблицы
            }
        }


        // реализуем интерфейсный метод на обновление таблиц БД
        public void Update()
        {
            FillCurrenciesTable(); // заполняем таблицу Currencies с информацией о валютах
            UpdateCurrenciesHistory(); // обновляем таблицу CurrenciesHistory с курсами валют

            Currencies = new List<Currency>();
            using (var connection = new SqlConnection(connectionString)) // устанавливаем соединение с БД
            {
                connection.Open(); // открываем соединение
                string sqlExpression = "SELECT * FROM Currencies"; // строка запроса на вывод информации о валютах
                var command = new SqlCommand(sqlExpression, connection); // создаём команду
                var reader = command.ExecuteReader(); // объект для чтения результата запроса

                while (reader.Read()) // построчно считываем данные
                {
                    var Id = (string)reader.GetValue(0);
                    var RusName = (string)reader.GetValue(1);
                    var EngName = (string)reader.GetValue(2);
                    object NumCode = reader.GetValue(4);
                    object CharCode = reader.GetValue(5);
                    // добавляем в список валют данные о текущей валюте
                    Currencies.Add(new Currency()
                    {
                        Id = Id,
                        RusName = RusName,
                        EngName = EngName,
                        NumCode = DBNull.Value == NumCode ? null : (int?)NumCode,
                        CharCode = DBNull.Value == CharCode ? null : (string)CharCode
                    });
                }
            }
        }


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
                UpdateCurrenciesHistory(connection, DateTime.Date);
        }


        // Реализуем интерфейсные методы, возвращающие курс валюты на определённую дату
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

        // Интерфейсное событие на изменение прогресса обновления таблиц БД
        public event Action<int> ProgressChanged;
    }


    static class XmlNodeExtension // статический класс, содержащий метод расширения
    {
        // метод расширения, возвращающий элемент XmlNode с именем Name коллекции XmlNodeList
        public static XmlNode Find(this XmlNodeList node, string Name)
        {
            return node.Cast<XmlNode>().First(x => x.Name == Name);
        }
    }
}