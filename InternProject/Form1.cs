using System;
using System.Runtime.Caching;
using System.Windows.Forms;

namespace InternProject
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }
        void RefreshList()
        {
            dataGridView1.DataSource = Leaderboard.GetUserDt("Select * FROM Users Order By 'score' DESC");
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshList();
        }
        void ClearTextBox()
        {
            foreach (Control item in this.panel2.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string userName = txtUser.Text;
            if (!Leaderboard.CheckUserNameUnique(userName))
            {
                MessageBox.Show(string.Format("The user named {0} already exists.", userName), "Error");
                return;
            }
            string sql = "insert into users(user_name,score) values('" + userName + "','" + txtScore.Text + "')";
            Leaderboard.ExecuteSql(sql);

            RefreshList();
            ClearTextBox();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtUser.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtScore.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string userName = txtUser.Text;
            if (!Leaderboard.CheckUserNameUnique(userName))
            {
                MessageBox.Show(string.Format("The user named {0} already exists.", userName), "Error");
                return;
            }

            string sql = "update users set user_name = '" + userName + "',score = '" + txtScore.Text + "' where id='" + txtId.Text + "' ";

            Leaderboard.ExecuteSql(sql);
            RefreshList();
            ClearTextBox();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Record will be deleted", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialog == DialogResult.OK)
            {
                string sql = string.Format("Delete from Users Where user_name = '{0}' ", dataGridView1.CurrentRow.Cells[1].Value.ToString());
                Leaderboard.ExecuteSql(sql);
                RefreshList();
                ClearTextBox();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            string sql = "Delete from Users ";
            Leaderboard.ExecuteSql(sql);
            RefreshList();
            ClearTextBox();
        }
    }
}
