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
    public class Model
    {
        // строка подключения к БД
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\Desktop\CurrencyApp\Model\DB.mdf;Integrated Security=True";
        

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
                foreach (var DateTime in DateTimes)
                    UpdateCurrenciesHistory(connection, DateTime);
            }
        }
        
        
        // обновление таблиц БД
        public void Update()
        {
            FillCurrenciesTable(); // заполняем таблицу Currencies с информацией о валютах
            UpdateCurrenciesHistory(); // обновляем таблицу CurrenciesHistory с курсами валют
        }
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
