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
    public partial class Hastalar : Form
    {
        public Hastalar()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-8JKFN02\SQLEXPRESS;Initial Catalog=EczaneOtomasyon;Integrated Security=True");
        Sqlbaglantisi bgl = new Sqlbaglantisi();
                
        void listele()
        {

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select*From TBL_HASTALAR", baglanti);
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        private class Sqlbaglantisi
        {

        }


        private void labelControl4_Click(object sender, EventArgs e)
        {
            
        }

        private void labelControl5_Click(object sender, EventArgs e)
        {

        }

        private void labelControl7_Click(object sender, EventArgs e)
        {

        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
        void sehirlistesi()
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("Select Sehir From TBL_ILLER", baglanti);
            SqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                Cmbil.Properties.Items.Add(dr[0]);
            }
            baglanti.Close();
                
        }
        void temizle()
        {
            Txtid.Text = "";
            TxtAd.Text = "";
            TxtSoyad.Text = "";
            TxtMail.Text = "";
            MskTc.Text = "";
            MskTelefon.Text = "";
            RchAdres.Text = "";
            Cmbil.Text = "";
            Cmbilce.Text = "";
        }


        private void Hastalar_Load(object sender, EventArgs e)
        {

            listele();
            sehirlistesi();
            temizle();
        }

        private void Cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            Cmbilce.Properties.Items.Clear();
            SqlCommand cmd = new SqlCommand("Select ilce from TBL_ILCELER where sehir=@p1", baglanti);
            cmd.Parameters.AddWithValue("@p1", Cmbil.SelectedIndex + 1 );
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Cmbilce.Properties.Items.Add(dr[0]);
            }
            baglanti.Close();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("insert into TBL_HASTALAR (AD,SOYAD,TELEFON,TC,MAIL,ADRES,IL,ILCE) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)",baglanti);
            cmd.Parameters.AddWithValue("@p1", TxtAd.Text);
            cmd.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            cmd.Parameters.AddWithValue("@p3", MskTelefon.Text);
            cmd.Parameters.AddWithValue("@p4", MskTc.Text);
            cmd.Parameters.AddWithValue("@p5", TxtMail.Text);
            cmd.Parameters.AddWithValue("@p6", RchAdres.Text);
            cmd.Parameters.AddWithValue("@p7", Cmbil.Text);
            cmd.Parameters.AddWithValue("@p8", Cmbilce.Text);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Hasta Sisteme Eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dr != null)
            {
                Txtid.Text = dr["ID"].ToString();
                TxtAd.Text = dr["AD"].ToString();
                TxtSoyad.Text = dr["SOYAD"].ToString();
                MskTelefon.Text = dr["TELEFON"].ToString();
                MskTc.Text = dr["TC"].ToString();
                TxtMail.Text = dr["MAIL"].ToString();
                RchAdres.Text = dr["ADRES"].ToString();
                Cmbil.Text = dr["IL"].ToString();
                Cmbilce.Text = dr["ILCE"].ToString();
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("Delete from TBL_HASTALAR where ID=@p1",baglanti);
            cmd.Parameters.AddWithValue("@p1", Txtid.Text);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Hasta Kaydı Silindi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            listele();
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("update TBL_HASTALAR set AD=@p1,SOYAD=@p2,TELEFON=@p3,TC=@p4,MAIL=@p5,ADRES=@p6,IL=@p7,ILCE=@p8 where ID=@p9", baglanti);
            cmd.Parameters.AddWithValue("@p1", TxtAd.Text);
            cmd.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            cmd.Parameters.AddWithValue("@p3", MskTelefon.Text);
            cmd.Parameters.AddWithValue("@p4", MskTc.Text);
            cmd.Parameters.AddWithValue("@p5", TxtMail.Text);
            cmd.Parameters.AddWithValue("@p6", RchAdres.Text);
            cmd.Parameters.AddWithValue("@p7", Cmbil.Text);
            cmd.Parameters.AddWithValue("@p8", Cmbilce.Text);
            cmd.Parameters.AddWithValue("@p9", Txtid.Text);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Hasta Bilgileri Güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
            temizle();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
