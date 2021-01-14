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
    public partial class FrmAyarlar : Form
    {
        public FrmAyarlar()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-8JKFN02\SQLEXPRESS;Initial Catalog=EczaneOtomasyon;Integrated Security=True");
        Sqlbaglantisi bgl = new Sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select*from TBL_ADMIN", baglanti);
            da.Fill(dt);
            gridControl1.DataSource = dt;

        }
        private void FrmAyarlar_Load(object sender, EventArgs e)
        {
            listele();
            TxtKullaniciAd.Text = "";
            TxtSifre.Text = "";
        }

        private void Btnıslem_Click(object sender, EventArgs e)
        {
            if (Btnıslem.Text == "KAYDET")
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("insert into TBL_ADMIN values (@p1,@p2)", baglanti);
                cmd.Parameters.AddWithValue("@p1", TxtKullaniciAd.Text);
                cmd.Parameters.AddWithValue("@p2", TxtSifre.Text);
                cmd.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Yeni Kullanıcı Kaydedildi.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            else if (Btnıslem.Text == "GÜNCELLE")
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("update TBL_ADMIN set sifre=@p2 where kullaniciad=@p1", baglanti);
                cmd.Parameters.AddWithValue("@p1", TxtKullaniciAd.Text);
                cmd.Parameters.AddWithValue("@p2", TxtSifre.Text);
                cmd.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kayıt Güncellendi", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                listele();
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dr != null)
            {
                TxtKullaniciAd.Text = dr["KullaniciAd"].ToString();
                TxtSifre.Text = dr["Sifre"].ToString();
                Btnıslem.Text = "GÜNCELLE";
                Btnıslem.BackColor = Color.Gainsboro;
            }
        }

        private void TxtKullaniciAd_TextChanged(object sender, EventArgs e)
        {
            if(TxtKullaniciAd.Text == "")
            {
   
                Btnıslem.Text = "KAYDET";
                Btnıslem.BackColor = Color.Teal;
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("delete from TBL_ADMIN where kullaniciad=@p1", baglanti);
            cmd.Parameters.AddWithValue("@p1", TxtKullaniciAd.Text);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kullanıcı Listeden Silindi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            listele();
        }
    }
}
