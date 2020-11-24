using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using ViewInterface;

namespace CurrencyApp
{
    public partial class MainForm : Form, IView // класс формы, реализующий интерфейс представления
    {
        public MainForm()
        {
            InitializeComponent();
        }


        // обработчик события загрузки формы
        private async void MainForm_Load(object sender, EventArgs e)
        {
            // установка ширины ProgressBar на всю свободную область строки состояния
            ProgressBar.Width = StatusStrip.Width - ProgressLabel.Width - 20;

            // объект, хранящий данные события Update;
            var UpdateEventArgs = new UpdateEventArgs();
            // вызов события на обновление таблиц БД
            await Task.Run(new Action(() => Update(this, UpdateEventArgs)));

            // заполнение списка валют названиями валют
            foreach (var Currency in UpdateEventArgs.Currencies)
                CurrenciesListBox.Items.Add(Currency);
        }



        // обработчки события изменение размера формы
        private void MainForm_Resize(object sender, EventArgs e)
        {
            // установка ширины ProgressBar на всю свободную область строки состояния
            ProgressBar.Width = StatusStrip.Width - ProgressLabel.Width - 20;
        }


        // метод, возвращающий пареметр, определяющий валюту
        private CurrencyIdentificator GetCurrencyIdentificator()
        {
            if (IdentificatorComboBox.SelectedIndex == 0)
                return ViewInterface.CurrencyIdentificator.RusName;
            else if (IdentificatorComboBox.SelectedIndex == 1)
                return ViewInterface.CurrencyIdentificator.EngName;
            else if (IdentificatorComboBox.SelectedIndex == 2)
                return ViewInterface.CurrencyIdentificator.NumCode;
            else if (IdentificatorComboBox.SelectedIndex == 3)
                return ViewInterface.CurrencyIdentificator.CharCode;
            else
                return ViewInterface.CurrencyIdentificator.Id;
        }


        // обработчик события выбора валюты из списка
        private void CurrenciesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((CurrenciesListBox.SelectedIndex != -1) && (IdentificatorComboBox.SelectedIndex != -1))
            {
                var CurrencyRusName = (string)CurrenciesListBox.SelectedItem; // название валюты
                var Identificator = GetCurrencyIdentificator(); // параметр, определяющий валюту
                // объект, хранящий данные события CurrencyIdentificator
                var currencyIdentificatorEventAgs = new CurrencyIdentificatorEventAgs(CurrencyRusName, Identificator);
                // вызов события на получение параметра валюты по её названию
                CurrencyIdentificator(this, currencyIdentificatorEventAgs);
                // запись в текстовое поле значения параметра, определяющего валюту
                IdentificatorValueTextBox.Text = currencyIdentificatorEventAgs.IdentificatorValue;
            }
        }


        // обработчик события выбора даты в календаре
        private void Calendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            var IdentificatorValue = IdentificatorValueTextBox.Text; // значение параметра, определяющего валюту
            if ((IdentificatorValue != "") && (IdentificatorComboBox.SelectedIndex != -1))
            {
                var Identificator = GetCurrencyIdentificator(); // параметр, определяющий валюту
                var Date = e.Start.Date; // дата, выбранная пользователем
                // объект, хранящий данные события CurrencyValue
                var CurrencyValueEventArgs = new CurrencyValueEventArgs(Identificator, IdentificatorValue, Date);
                try
                {
                    // вызов события на получение курса валюты
                    CurrencyValue(this, CurrencyValueEventArgs);
                    // запись в текстовое поле курса валюты
                    CurrencyValueTextBox.Text = CurrencyValueEventArgs.Value.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message); // отображние сообщение об ошибке в случае её возникновения
                }
            }
        }


        // Реализация интерфейсных методов и событий
        public void SetProgress(int Progress)
        {
            Invoke(new Action(() =>
            {
                ProgressLabel.Text = String.Format("Обновление курсов валют: {0}%", Progress);
                ProgressBar.Value = Progress;
            }));
        }

        public event EventHandler<UpdateEventArgs> Update;
        public event EventHandler<CurrencyIdentificatorEventAgs> CurrencyIdentificator;
        public event EventHandler<CurrencyValueEventArgs> CurrencyValue;
    }
}
