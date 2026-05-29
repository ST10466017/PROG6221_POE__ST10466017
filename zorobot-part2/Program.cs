using System;
using System.Windows;

namespace ZoroCyberSecurityBot
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            MainWindow window = new MainWindow();
            app.Run(window);
        }
    }
}