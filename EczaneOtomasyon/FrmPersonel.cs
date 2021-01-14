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
    public partial class FrmPersonel : Form
    {
        public FrmPersonel()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-8JKFN02\SQLEXPRESS;Initial Catalog=EczaneOtomasyon;Integrated Security=True");
        Sqlbaglantisi bgl = new Sqlbaglantisi();

        void personelliste()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select*from TBL_PERSONELLER", baglanti);
            da.Fill(dt);
            gridControl1.DataSource = dt;

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
            TxtGorev.Text = "";
            MskTc.Text = "";
            MskTelefon.Text = "";
            RchAdres.Text = "";
            Cmbil.Text = "";
            Cmbilce.Text = "";
        }


        private void FrmPersonel_Load(object sender, EventArgs e)
            {
                personelliste();
                sehirlistesi();
                temizle();
            }

            private void BtnKaydet_Click(object sender, EventArgs e)
            {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("insert into TBL_PERSONELLER (AD,SOYAD,TELEFON,TC,MAIL,IL,ILCE,ADRES,GOREV) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", baglanti);
            cmd.Parameters.AddWithValue("@p1", TxtAd.Text);
            cmd.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            cmd.Parameters.AddWithValue("@p3", MskTelefon.Text);
            cmd.Parameters.AddWithValue("@p4", MskTc.Text);
            cmd.Parameters.AddWithValue("@p5", TxtMail.Text);
            cmd.Parameters.AddWithValue("@p6", Cmbil.Text);
            cmd.Parameters.AddWithValue("@p7", Cmbilce.Text);
            cmd.Parameters.AddWithValue("@p8", RchAdres.Text);
            cmd.Parameters.AddWithValue("@p9", TxtGorev.Text);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Personel Bilgileri Kaydedildi", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            personelliste();
            
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

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if( dr!= null)
            {
                Txtid.Text = dr["ID"].ToString();
                TxtAd.Text = dr["AD"].ToString();
                TxtSoyad.Text = dr["SOYAD"].ToString();
                MskTelefon.Text = dr["TELEFON"].ToString();
                MskTc.Text = dr["TC"].ToString();
                TxtMail.Text = dr["MAIL"].ToString();
                Cmbil.Text = dr["IL"].ToString();
                Cmbilce.Text = dr["ILCE"].ToString();
                RchAdres.Text = dr["ADRES"].ToString();
                TxtGorev.Text = dr["GOREV"].ToString();
            }
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("delete from TBL_PERSONELLER where ID=@p1", baglanti);
            cmd.Parameters.AddWithValue("@p1", Txtid.Text);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Personel Listeden Silindi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            personelliste();
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("Update TBL_PERSONELLER set AD=@p1,SOYAD=@p2,TELEFON=@p3,TC=@p4,MAIL=@p5,IL=@p6,ILCE=@p7,ADRES=@p8,GOREV=@p9 where ID=@p10", baglanti);
            cmd.Parameters.AddWithValue("@p1", TxtAd.Text);
            cmd.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            cmd.Parameters.AddWithValue("@p3", MskTelefon.Text);
            cmd.Parameters.AddWithValue("@p4", MskTc.Text);
            cmd.Parameters.AddWithValue("@p5", TxtMail.Text);
            cmd.Parameters.AddWithValue("@p6", Cmbil.Text);
            cmd.Parameters.AddWithValue("@p7", Cmbilce.Text);
            cmd.Parameters.AddWithValue("@p8", RchAdres.Text);
            cmd.Parameters.AddWithValue("@p9", TxtGorev.Text);
            cmd.Parameters.AddWithValue("@p10", Txtid.Text);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Personel Bilgileri Güncellendi", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            personelliste();
            
        }
    }
    } 
