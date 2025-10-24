using DataGridView.Forms;
using DataGridView.Clasess;
namespace DataGridView
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            RefreshTable();

            // Добавляем события
            dataGridView1.CellDoubleClick += dgvItems_CellDoubleClick;

        }
        public void RefreshTable()
        {
            dataGridView1.Rows.Clear();

            foreach (var item in Storage.Items)
            {
                dataGridView1.Rows.Add(item.Name, item.Price);
            }
        }

        // Открываем форму редактирования по двойному клику
        private void dgvItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                AddForm editForm = new AddForm();
                editForm.Show();
            }
        }

        // Клик по кнопке "Добавить"
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm();
            addForm.Show();
        }

        // Клик по кнопке "Удалить"
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строку!");
                return;
            }

            int index = dataGridView1.SelectedRows[0].Index;
            Storage.Items.RemoveAt(index);

            RefreshTable();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm();
            addForm.ShowDialog();
        }
    }
}
