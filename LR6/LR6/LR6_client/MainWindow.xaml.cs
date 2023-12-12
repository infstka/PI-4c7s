using System;
using System.Data.Services.Client;
using System.Windows;
using WSBGSModel;

namespace LR6_client
{
    public partial class MainWindow : Window
    {
        private readonly Uri _serviceUrl = new Uri("http://localhost:58061/StudentNotes.svc");

        public MainWindow()
        {
            InitializeComponent();
        }

        public void OnGetStudentsClick(object sender, RoutedEventArgs e) => Exec(new WSBGSEntities(_serviceUrl).Students);

        public void OnGetNotesClick(object sender, RoutedEventArgs e) => Exec(new WSBGSEntities(_serviceUrl).Notes);

        // DataServiceQuery - абстрактный класс, представляющий один запрос к службам данных WCF.
        private void Exec<T>(DataServiceQuery<T> serviceQuery)
        {
            Result.Text = string.Empty;

            if (!string.IsNullOrWhiteSpace(FilterColumn.Text) && !string.IsNullOrWhiteSpace(FilterValue.Text))
            {
                if (int.TryParse(FilterValue.Text, out var result))
                    serviceQuery = serviceQuery.AddQueryOption("$filter", $"{FilterColumn.Text} eq {result}");
                else
                    serviceQuery = serviceQuery.AddQueryOption("$filter", $"substringof('{FilterValue.Text}', {FilterColumn.Text})");
            }

            if (!string.IsNullOrWhiteSpace(OrderBy.Text))
                serviceQuery = serviceQuery.AddQueryOption("$orderby", OrderBy.Text);

            if (!string.IsNullOrWhiteSpace(Select.Text))
                serviceQuery = serviceQuery.AddQueryOption("$select", Select.Text);

            try
            {
                // выполняем запрос к службе данных
                var queryResult = serviceQuery.Execute();

                foreach (var item in queryResult)
                {
                    if (item is Student student)
                    {
                        Result.Text += (student?.Id > 0) ? "ID: " + (student?.Id).ToString() : "";
                        Result.Text += (!string.IsNullOrWhiteSpace(student?.Name)) ? " | Name: " + student?.Name.ToString() : "";
                        Result.Text += "\n";
                    }
                    else if (item is Note note)
                    {
                        Result.Text += (note?.Id > 0) ? "ID: " + (note?.Id).ToString() : "";
                        Result.Text += (!string.IsNullOrWhiteSpace(note?.Subject)) ? " | Subject: " + note?.Subject.ToString() : "";
                        Result.Text += (note?.Note1 != 0) ? " | Note: " + (note?.Note1).ToString() : "";
                        Result.Text += (note?.StudentId != 0) ? ("\t| StudentID: " + note?.StudentId).ToString() : "";
                        Result.Text += "\n";
                    }
                }
            }
            catch (Exception ex)
            {
                Result.Text = ex.ToString();
            }
        }
    }
}
