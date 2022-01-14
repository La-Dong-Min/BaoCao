using BaoCao.BLL;
using BaoCao.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaoCao
{
    public partial class Form1 : Form
    {
        DepartmentBLL depart = new DepartmentBLL();
        ProjectBLL cusBLL = new ProjectBLL();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<ProjectDTO> lstCus = cusBLL.ReadCustomer();
            foreach (ProjectDTO cus in lstCus)
            {
                dgvNhanVien.Rows.Add(cus.IdEmployee, cus.Name, cus.DateBirth, cus.Gender, cus.PlaceBirth, cus.IdDepartment);
            }

            List<Department> dbm = depart.ReadAreaList();
            foreach (Department Depart in dbm)
            {
                cmBDonVi.Items.Add(Depart);
                cmBDonVi.DisplayMember = "Name";
            }
        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            DialogResult Thoat;
            Thoat = MessageBox.Show("Bạn có muốn thoát?", "Thông Báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (Thoat == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void btThem_Click(object sender, EventArgs e)
        {
            ProjectDTO cus = new ProjectDTO();
            if (tbMa.Text.Equals("") || tbHoTen.Text.Equals("") || tbNoiSinh.Text.Equals(""))
            {
                MessageBox.Show("Không bỏ trống", "Thông báo");
            }
            else
            {
                cus.IdEmployee = tbMa.Text;
                cus.Name = tbHoTen.Text;
                cus.DateBirth = dateTimePickerNgaySinh.Value.Date.ToString();
                cus.Gender = checkBoxGioiTinh.Checked;
                cus.PlaceBirth = tbNoiSinh.Text;
                cus.Depart = (Department)cmBDonVi.SelectedItem;
                cusBLL.NewCustomer(cus);

                dgvNhanVien.Rows.Add(cus.IdEmployee, cus.Name, cus.DateBirth, cus.Gender, cus.PlaceBirth, cus.IdDepartment);
            }
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            ProjectDTO Nv = new ProjectDTO();

            if (tbMa.Text.Equals("") || tbHoTen.Text.Equals("") || tbNoiSinh.Text.Equals(""))
            {
                MessageBox.Show("Không được bỏ trống", "Thông báo");
            }
            else
            {
                Nv.IdEmployee = tbMa.Text;
                Nv.Name = tbHoTen.Text;
                Nv.PlaceBirth = tbNoiSinh.Text;
                Nv.DateBirth = dateTimePickerNgaySinh.Text;
                Nv.Gender = dateTimePickerNgaySinh.Checked;
                Nv.Depart = (Department)cmBDonVi.SelectedItem;
                cusBLL.EditCustomer(Nv);

                DataGridViewRow row = dgvNhanVien.CurrentRow;
                row.Cells[0].Value = Nv.IdEmployee;
                row.Cells[1].Value = Nv.Name;
                row.Cells[2].Value = Nv.DateBirth;
                row.Cells[3].Value = Nv.Gender;
                row.Cells[4].Value = Nv.PlaceBirth;
                row.Cells[5].Value = Nv.IdDepartment;
            }
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            DialogResult Xoa;
            Xoa = MessageBox.Show("Bạn có muốn xóa thoát?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Xoa == DialogResult.Yes)
            {
                ProjectDTO Nv = new ProjectDTO();
                Nv.IdEmployee = tbMa.Text;
                Nv.Name = tbHoTen.Text;
                Nv.PlaceBirth = tbNoiSinh.Text;
                Nv.DateBirth = dateTimePickerNgaySinh.Text;
                Nv.Depart = (Department)cmBDonVi.SelectedItem;
                int idx = dgvNhanVien.CurrentCell.RowIndex;
                cusBLL.DeleteCustomer(Nv);
                dgvNhanVien.Rows.RemoveAt(idx);
             }
        }

    

        private void Row(object sender, DataGridViewCellEventArgs e)
        {
            int idx = e.RowIndex;
            if (idx >= 0)
            {
                tbMa.Text = dgvNhanVien.Rows[idx].Cells[0].Value.ToString();
                tbHoTen.Text = dgvNhanVien.Rows[idx].Cells[1].Value.ToString();
                dateTimePickerNgaySinh.Text = dgvNhanVien.Rows[idx].Cells[2].Value.ToString();
                checkBoxGioiTinh.Checked = bool.Parse(dgvNhanVien.Rows[idx].Cells[3].Value.ToString());
                tbNoiSinh.Text = dgvNhanVien.Rows[idx].Cells[4].Value.ToString();
                cmBDonVi.Text = dgvNhanVien.Rows[idx].Cells[5].Value.ToString();
            }
        }
    }
}
