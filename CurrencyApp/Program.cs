using System;
using System.Windows.Forms;
using ModelLibrary;

namespace CurrencyApp
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var MainForm = new MainForm();
            // создание объекта представителя, связывающего представление и модель
            new Presenter(MainForm, new Model());
            Application.Run(MainForm);
        }
    }
}