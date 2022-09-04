using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;

namespace RehberUygulamasi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection bag = new SqlConnection(@"Data Source=THINKPAD-E470;
        Initial Catalog=Rehber;Integrated Security=True");
        void listele()
        {
            DataTable dt = new DataTable(); 
            SqlDataAdapter da = new SqlDataAdapter("SELECT*FROM TBLKİSİLER",bag);   
            da.Fill(dt);    
            dataGridView1.DataSource = dt;  
        }
        void temizle ()
        {
            txtAd.Text = "";
            txtSoyad.Text = "";
            txtMail.Text = "";
            txtTelefon.Text = "";
            txtID.Text = "";
            txtAd.Focus();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }
        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
        private void btnKisiEkle_Click(object sender, EventArgs e)
        {
            bag.Open();
            SqlCommand komut = new SqlCommand("insert into TBLKİSİLER(AD,SOYAD,TELEFON,MAİL) VALUES (@P1,@P2,@P3,@P4)",bag);
            komut.Parameters.AddWithValue("@P1",txtAd.Text);
            komut.Parameters.AddWithValue("@P2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@P3", txtTelefon.Text);
            komut.Parameters.AddWithValue("@P4", txtMail.Text);
            komut.ExecuteNonQuery();
            bag.Close();
            MessageBox.Show("Kayıt Başarılı :)");
            listele();
            temizle();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Seçili satırdaki veriyi textboxa taşıma
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtTelefon.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtMail.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
        }
        private void btnSil_Click(object sender, EventArgs e)
        {
            bag.Open();
            SqlCommand komut = new SqlCommand("DELETE FROM TBLKİSİLER where ID="+txtID.Text,bag);
            komut.ExecuteNonQuery();
            bag.Close();
            MessageBox.Show("Kişi Rehberden Silindi...","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Error);
            listele();
            temizle();
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            bag.Open();
            SqlCommand komut = new SqlCommand("UPDATE TBLKİSİLER SET AD=@P1,SOYAD=@P2,TELEFON=@P3,MAİL=@P4 WHERE ID= @P5",bag);
            komut.Parameters.AddWithValue("@P1",txtAd.Text);
            komut.Parameters.AddWithValue("@P2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@P3", txtTelefon.Text);
            komut.Parameters.AddWithValue("@P4", txtMail.Text);
            komut.Parameters.AddWithValue("@P5", txtID.Text);
            komut.ExecuteNonQuery();
            bag.Close();
            MessageBox.Show("Kişi Güncellendi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            listele();
            temizle();
        }
    }
}
