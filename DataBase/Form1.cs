using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;


namespace DataBase
{
    public partial class Form1 : Form
    {
        private Dictionary<string, DataGridView> tableViews;
        private string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=2603;Database=Agent";
        public Form1()
        {
            InitializeComponent();
            InitializeTableViews();
        }

        private void InitializeTableViews()
        {
            tableViews = new Dictionary<string, DataGridView>
            {
                { "clients", dataGridView1 },
                { "properties", dataGridView2 },
                { "deals", dataGridView3 },
                { "payments", dataGridView4 },
                { "legaldocuments", dataGridView5 },
                { "requests", dataGridView6 }
            };
            
        }

        
        private void YourMethod()
        {
            

            using (var connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MessageBox.Show("Подключение к базе данных успешно установлено!");

                    //
                    string clients = "SELECT * FROM clients";
                    using (var command = new NpgsqlCommand(clients, connection))
                    {
                        using (var adapter = new NpgsqlDataAdapter(command))
                        {
                            DataTable dataTableJobPostings = new DataTable();
                            adapter.Fill(dataTableJobPostings);
                            dataGridView1.DataSource = dataTableJobPostings;
                        }
                    }

                    //
                    string deals = "SELECT * FROM deals";
                    using (var command = new NpgsqlCommand(deals, connection))
                    {
                        using (var adapter = new NpgsqlDataAdapter(command))
                        {
                            DataTable dataTableJobPostings = new DataTable();
                            adapter.Fill(dataTableJobPostings);
                            dataGridView2.DataSource = dataTableJobPostings;
                        }
                    }

                    //
                    string legaldocuments = "SELECT * FROM legaldocuments";
                    using (var command = new NpgsqlCommand(legaldocuments, connection))
                    {
                        using (var adapter = new NpgsqlDataAdapter(command))
                        {
                            DataTable dataTableJobPostings = new DataTable();
                            adapter.Fill(dataTableJobPostings);
                            dataGridView3.DataSource = dataTableJobPostings;
                        }
                    }

                    //
                    string payments = "SELECT * FROM payments";
                    using (var command = new NpgsqlCommand(payments, connection))
                    {
                        using (var adapter = new NpgsqlDataAdapter(command))
                        {
                            DataTable dataTableJobPostings = new DataTable();
                            adapter.Fill(dataTableJobPostings);
                            dataGridView4.DataSource = dataTableJobPostings;
                        }
                    }

                    //
                    string properties = "SELECT * FROM properties";
                    using (var command = new NpgsqlCommand(properties, connection))
                    {
                        using (var adapter = new NpgsqlDataAdapter(command))
                        {
                            DataTable dataTableJobPostings = new DataTable();
                            adapter.Fill(dataTableJobPostings);
                            dataGridView5.DataSource = dataTableJobPostings;
                        }
                    }

                    //
                    string requestes = "SELECT * FROM requestes";
                    using (var command = new NpgsqlCommand(requestes, connection))
                    {
                        using (var adapter = new NpgsqlDataAdapter(command))
                        {
                            DataTable dataTableJobPostings = new DataTable();
                            adapter.Fill(dataTableJobPostings);
                            dataGridView6.DataSource = dataTableJobPostings;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка подключения: {ex.Message}");
                }
            }
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                string selectedTable = comboBox1.SelectedItem.ToString();
                if (tableViews.ContainsKey(selectedTable))
                {
                    var dataGridView = tableViews[selectedTable];

                    using (var connection = new NpgsqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();

                            
                            var dataTable = (DataTable)dataGridView.DataSource;
                            var newRow = dataTable.NewRow(); 
                            dataTable.Rows.Add(newRow);

                            
                            var adapter = new NpgsqlDataAdapter($"SELECT * FROM {selectedTable}", connection);
                            var commandBuilder = new NpgsqlCommandBuilder(adapter);
                            adapter.Update(dataTable);

                            MessageBox.Show($"Строка добавлена в таблицу {selectedTable}.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка добавления строки в таблицу {selectedTable}: {ex.Message}");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите таблицу из списка.");
            }
            YourMethod();
        }
            

        private void Form1_Load(object sender, EventArgs e)
        {
            YourMethod();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                string selectedTable = comboBox1.SelectedItem.ToString();
                if (tableViews.ContainsKey(selectedTable))
                {
                    var dataGridView = tableViews[selectedTable];

                    if (dataGridView.SelectedRows.Count > 0)
                    {
                        using (var connection = new NpgsqlConnection(connectionString))
                        {
                            try
                            {
                                connection.Open();

                                
                                foreach (DataGridViewRow row in dataGridView.SelectedRows)
                                {
                                    if (!row.IsNewRow)
                                    {
                                        dataGridView.Rows.Remove(row);
                                    }
                                }

                                
                                var dataTable = (DataTable)dataGridView.DataSource;
                                var adapter = new NpgsqlDataAdapter($"SELECT * FROM {selectedTable}", connection);
                                var commandBuilder = new NpgsqlCommandBuilder(adapter);
                                adapter.Update(dataTable);

                                MessageBox.Show($"Выбранные строки удалены из таблицы {selectedTable}.");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Ошибка удаления строки из таблицы {selectedTable}: {ex.Message}");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Выберите строку для удаления.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите таблицу из списка.");
            }
            YourMethod();
        }
            
    }
}
