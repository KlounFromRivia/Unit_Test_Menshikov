using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserStory_Men_Cher.Models;

namespace UserStory_Men_Cher
{
    public partial class Form1 : Form
    {
        private readonly List<Tovar> tovars;
        private readonly BindingSource bindingSource;
        double AllpriceB = 0, AllpriceNDS = 0;

        public Form1()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            tovars = new List<Tovar>();
            bindingSource = new BindingSource();
            bindingSource.DataSource = tovars;
            dataGridView1.DataSource = bindingSource;
        }
        private void ToolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ToolStripMenuItemProg_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа 'Реестр товаров' ", "О программе", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void provLabel()
        {
            toolStripStatuslblAllKolvo.Text = "Колличество товаров на складе: " + tovars.Count();
            toolStripStatuslblAllPrice.Text = "Общая сумма товаров без НДС: " + Math.Round(AllpriceB,2);
            toolStripStatuslblPriceNDS.Text = "Общая сумма товаров с НДС 20%: " + Math.Round(AllpriceNDS,2);
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            var tvrInfoForm = new FormTovarInfo();
            tvrInfoForm.Text = "Добавление студента";
            if (tvrInfoForm.ShowDialog(this) == DialogResult.OK)
            {
                tvrInfoForm.Tovar.Id = Guid.NewGuid();
                //MessageBox.Show($"Name: {stdInfoForm.Student.FullName}\n\rGender: {stdInfoForm.Student.Gender}\n\rBirthDay: {stdInfoForm.Student.BirthDay:dd.MM.yyyy}\n\rAvgRate: {stdInfoForm.Student.AvgRate}\n\rDeduct: {stdInfoForm.Student.Deduct}\n\rDebtor: {stdInfoForm.Student.Debtor}", "", MessageBoxButtons.OK);
                tovars.Add(tvrInfoForm.Tovar);
                bindingSource.ResetBindings(false);
                AllpriceB += tvrInfoForm.Tovar.AllPrice;
                AllpriceNDS += tvrInfoForm.Tovar.AllPrice + (tvrInfoForm.Tovar.AllPrice * 0.2);
                provLabel();
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "MaterialColumn")
            {
                var val = (Material)e.Value;
                switch (val)
                {
                    case Material.Med:
                        e.Value = "Медь";
                        break;
                    case Material.Stal:
                        e.Value = "Сталь";
                        break;
                    case Material.Iron:
                        e.Value = "Железо";
                        break;
                    case Material.Hrom:
                        e.Value = "Хром";
                        break;
                }
            }
            if (e.ColumnIndex >= 0 &&
                e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "CountColumn")
            {
                var val = int.Parse(e.Value.ToString());
                var minVal = int.Parse(dataGridView1[e.ColumnIndex+1,e.RowIndex].Value.ToString());
                if (val < minVal)
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = System.Drawing.Color.Red;
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            toolStripButtonChange.Enabled =
                toolStripButtonDelete.Enabled = ToolStripMenuItemChange.Enabled = ToolStripMenuItemDelete.Enabled =
                dataGridView1.SelectedRows.Count > 0;
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            var data = (Tovar)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].DataBoundItem;
            if(MessageBox.Show($"Вы действительно хотите удалить '{data.TovarName}'?",
                    "Удаление записи",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                AllpriceB = AllpriceB - data.AllPrice;
                AllpriceNDS = AllpriceNDS - (data.AllPrice + (data.AllPrice * 0.2));
                tovars.Remove(data);
                bindingSource.ResetBindings(false);
            }
            provLabel();
        }

        private void toolStripButtonChange_Click(object sender, EventArgs e)
        {
            var data = (Tovar)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].DataBoundItem;
            var infoForm = new FormTovarInfo(data);
            infoForm.Text = "Редактирование студента";
            if (infoForm.ShowDialog(this)== DialogResult.OK)
            {
                AllpriceB -= data.AllPrice;
                AllpriceNDS -= data.AllPrice + (data.AllPrice * 0.2);
                data.TovarName = infoForm.Tovar.TovarName;
                data.Material = infoForm.Tovar.Material;
                data.Size  = infoForm.Tovar.Size;
                data.Count = infoForm.Tovar.Count;
                data.MinCount = infoForm.Tovar.MinCount;
                data.Price = infoForm.Tovar.Price;
                data.AllPrice = infoForm.Tovar.Price * infoForm.Tovar.Count;
                bindingSource.ResetBindings(false);
                AllpriceB += data.AllPrice;
                AllpriceNDS += data.AllPrice + (data.AllPrice * 0.2);
                provLabel();
            }
        }
    }
}
