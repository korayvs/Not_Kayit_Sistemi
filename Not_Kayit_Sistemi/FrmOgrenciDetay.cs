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
    public partial class FrmOgrenciDetay : Form
    {
        public FrmOgrenciDetay()
        {
            InitializeComponent();
        }

        public string numara;

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-F1A12T8\KORAY;Initial Catalog=DbNotKayit;Integrated Security=True");

        private void FrmOgrenciDetay_Load(object sender, EventArgs e)
        {
            LblNo.Text = numara;

            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From TBLDERS Where OGRNUMARA = @p1", baglanti);
            komut.Parameters.AddWithValue("@p1", numara);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoy.Text = dr[2].ToString() + " " + dr[3].ToString();
                LblS1.Text = dr[4].ToString();
                LblS2.Text = dr[5].ToString();
                LblS3.Text = dr[6].ToString();
                LblOrt.Text = dr[7].ToString();
                LblDurum.Text = dr[8].ToString();
            }
            //Ödev2: Durum kısmında true/false yerine geçti/kaldı yazsın
            if (LblDurum.Text == "True")
            {
                LblDurum.Text = "Geçti";
            }

            if (LblDurum.Text == "False")
            {
                LblDurum.Text = "Kaldı";
            }

            baglanti.Close();
        }
    }
}
