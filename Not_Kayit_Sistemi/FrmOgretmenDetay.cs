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

namespace Not_Kayit_Sistemi
{
    public partial class FrmOgretmenDetay : Form
    {
        public FrmOgretmenDetay()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-F1A12T8\KORAY;Initial Catalog=DbNotKayit;Integrated Security=True");

        private void FrmOgretmenDetay_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbNotKayitDataSet.TBLDERS' table. You can move, or remove it, as needed.
            this.tBLDERSTableAdapter.Fill(this.dbNotKayitDataSet.TBLDERS);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Insert Into TBLDERS (OGRNUMARA, OGRAD, OGRSOYAD) Values (@p1, @p2, @p3)", baglanti);
            komut.Parameters.AddWithValue("@p1", MskNo.Text);
            komut.Parameters.AddWithValue("@p2", TxtAd.Text);
            komut.Parameters.AddWithValue("@p3", TxtSoyad.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci Sisteme Eklendi");
            this.tBLDERSTableAdapter.Fill(this.dbNotKayitDataSet.TBLDERS);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            MskNo.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            TxtS1.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            TxtS2.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            TxtS3.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double ortalama, s1, s2, s3;
            string durum;
            s1 = Convert.ToDouble(TxtS1.Text);
            s2 = Convert.ToDouble(TxtS2.Text);
            s3 = Convert.ToDouble(TxtS3.Text);

            ortalama = (s1 + s2 + s3) / 3;
            LblOrt.Text = ortalama.ToString("0.00");

            if (ortalama >= 50)
            {
                durum = "True";
            }
            else
            {
                durum = "False";
            }
            //Ödev1: Geçen ve Kalan öğrenci sayısını yazsın
            LblGecenS.Text = dbNotKayitDataSet.TBLDERS.Count(x => x.DURUM == true).ToString();
            LblKalanS.Text = dbNotKayitDataSet.TBLDERS.Count(x => x.DURUM == false).ToString();
            LblOrt.Text = dbNotKayitDataSet.TBLDERS.Average(y => y.ORTALAMA).ToString("0.00");

            baglanti.Open();
            SqlCommand komut = new SqlCommand("Update TBLDERS Set OGRS1 = @p1, OGRS2 = @p2, OGRS3 = @p3, ORTALAMA = @p4, DURUM = @p5 Where OGRNUMARA = @p6", baglanti);
            komut.Parameters.AddWithValue("@p1", TxtS1.Text);
            komut.Parameters.AddWithValue("@p2", TxtS2.Text);
            komut.Parameters.AddWithValue("@p3", TxtS3.Text);
            komut.Parameters.AddWithValue("@p4", decimal.Parse(LblOrt.Text));
            komut.Parameters.AddWithValue("@p5", durum);
            komut.Parameters.AddWithValue("@p6", MskNo.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci Notları Güncellendi");
            this.tBLDERSTableAdapter.Fill(this.dbNotKayitDataSet.TBLDERS);
        }
    }
}
