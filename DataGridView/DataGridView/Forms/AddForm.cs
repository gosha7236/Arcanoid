using DataGridView.Clasess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DataGridView.Forms
{
    public partial class AddForm : Form
    {
        public AddForm()
        {
            InitializeComponent();
        }
        private MainForm _mainForm;
        private int _editIndex = -1; // -1 = добавление, >=0 = редактирование

        // Конструктор для добавления
        public AddForm(MainForm main)
        {
            InitializeComponent();
            _mainForm = main;
        }

        // Конструктор для редактирования
        public AddForm(MainForm main, int index)
        {
            InitializeComponent();
            _mainForm = main;
            _editIndex = index;

            Item item = Storage.Items[index];

            txtName.Text = item.Name;
            txtPrice.Text = item.Price.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price))
            {
                MessageBox.Show("Цена должна быть числом!");
                return;
            }

            if (_editIndex == -1)
            {
                // Добавление
                Storage.Items.Add(new Item
                {
                    Name = txtName.Text,
                    Price = price
                });
            }
            else
            {
                // Изменение
                Storage.Items[_editIndex].Name = txtName.Text;
                Storage.Items[_editIndex].Price = price;
            }

            _mainForm.RefreshTable();
            this.Close();
        }
        private void AddForm_Load(object sender, EventArgs e)
        {

        }
    }
}
