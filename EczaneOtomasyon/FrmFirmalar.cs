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

namespace EczaneOtomasyon
{
    public partial class FrmFirmalar : Form
    {
        public FrmFirmalar()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-8JKFN02\SQLEXPRESS;Initial Catalog=EczaneOtomasyon;Integrated Security=True");
        Sqlbaglantisi bgl = new Sqlbaglantisi();

        void listele()
        {

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select*From TBL_FIRMALAR", baglanti);
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            TxtAd.Text = "";
            Txtid.Text = "";
            TxtYetkili.Text = "";
            TxtMail.Text = "";
            MskFax.Text = "";
            MskTelefon1.Text = "";
            MskTelefon2.Text = "";
            RchAdres.Text = "";
            Cmbil.Text = "";
            Cmbilce.Text = "";

        }
        void sehirlistesi()
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("Select Sehir From TBL_ILLER", baglanti);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Cmbil.Properties.Items.Add(dr[0]);
            }
            baglanti.Close();

        }
        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            sehirlistesi();
            listele();
            temizle();
        }


        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                Txtid.Text = dr["ID"].ToString();
                TxtAd.Text = dr["AD"].ToString();
                TxtYetkili.Text = dr["YETKILI"].ToString();
                MskTelefon1.Text = dr["TELEFON"].ToString();
                MskTelefon2.Text = dr["TELEFON2"].ToString();
                MskFax.Text = dr["FAX"].ToString();
                TxtMail.Text = dr["MAIL"].ToString();
                Cmbil.Text = dr["IL"].ToString();
                Cmbilce.Text = dr["ILCE"].ToString();
                RchAdres.Text = dr["ADRES"].ToString();

            }
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("insert into TBL_FIRMALAR (AD,YETKILI,TELEFON,TELEFON2,MAIL,FAX,IL,ILCE,ADRES) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)",baglanti);
            cmd.Parameters.AddWithValue("@p1", TxtAd.Text);
            cmd.Parameters.AddWithValue("@p2", TxtYetkili.Text);
            cmd.Parameters.AddWithValue("@p3", MskTelefon1.Text);
            cmd.Parameters.AddWithValue("@p4", MskTelefon2.Text);
            cmd.Parameters.AddWithValue("@p5", TxtMail.Text);
            cmd.Parameters.AddWithValue("@p6", MskFax.Text);
            cmd.Parameters.AddWithValue("@p7", Cmbil.Text);
            cmd.Parameters.AddWithValue("@p8", Cmbilce.Text);
            cmd.Parameters.AddWithValue("@p9", RchAdres.Text);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Firma Sisteme Eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void Cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            Cmbilce.Properties.Items.Clear();
            SqlCommand cmd = new SqlCommand("Select ilce from TBL_ILCELER where sehir=@p1", baglanti);
            cmd.Parameters.AddWithValue("@p1", Cmbil.SelectedIndex + 1);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Cmbilce.Properties.Items.Add(dr[0]);
            }
            baglanti.Close();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("Delete From TBL_FIRMALAR where ID=@p1", baglanti);
            cmd.Parameters.AddWithValue("@p1", Txtid.Text);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Firma Listeden Silindi", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            listele();
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("Update TBL_FIRMALAR set AD=@p1,YETKILI=@p2,TELEFON=@p3,TELEFON2=@p4,MAIL=@p5,FAX=@p6,IL=@p7,ILCE=@p8,ADRES=@p9 where ID=@p10",baglanti);         
            cmd.Parameters.AddWithValue("@p1", TxtAd.Text);
            cmd.Parameters.AddWithValue("@p2", TxtYetkili.Text);
            cmd.Parameters.AddWithValue("@p3", MskTelefon1.Text);
            cmd.Parameters.AddWithValue("@p4", MskTelefon2.Text);
            cmd.Parameters.AddWithValue("@p5", TxtMail.Text);
            cmd.Parameters.AddWithValue("@p6", MskFax.Text);
            cmd.Parameters.AddWithValue("@p7", Cmbil.Text);
            cmd.Parameters.AddWithValue("@p8", Cmbilce.Text);
            cmd.Parameters.AddWithValue("@p9", RchAdres.Text);
            cmd.Parameters.AddWithValue("@p10", Txtid.Text);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Firma Bilgileri Güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

    }
}

