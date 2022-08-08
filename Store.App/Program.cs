using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
using Store.Models;
using Store.Services;
using Store.Repositories;

namespace Store.App
{
    static class Program
    {
        public static IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
            .AddJsonFile("appsettings.json")
            .Build();

        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LoginForm loginForm = new();
            DialogResult result = loginForm.ShowDialog();
            if (result != DialogResult.OK) return;
            Application.Run(new MainForm());
        }
    }
}