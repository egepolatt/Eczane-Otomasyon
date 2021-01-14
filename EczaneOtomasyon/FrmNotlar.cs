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
    public partial class Notlar : Form
    {
        public Notlar()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-8JKFN02\SQLEXPRESS;Initial Catalog=EczaneOtomasyon;Integrated Security=True");
        Sqlbaglantisi bgl = new Sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select*from TBL_NOTLAR", baglanti);
            da.Fill(dt);
            gridControl1.DataSource = dt;

        }
        void temizle()
        {
            Txtid.Text = "";
            MskTarih.Text = "";
            MskSaat.Text = "";
            TxtBaslık.Text = "";
            TxtOlusturan.Text = "";
            RchDetay.Text = "";
            TxtGorev.Text = "";
        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }

        private void Notlar_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("insert into TBL_NOTLAR (TARİH,SAAT,BASLIK,DETAY,OLUSTURAN,GÖREV) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
            cmd.Parameters.AddWithValue("@p1", MskTarih.Text);
            cmd.Parameters.AddWithValue("@p2", MskSaat.Text);
            cmd.Parameters.AddWithValue("@p3", TxtBaslık.Text);
            cmd.Parameters.AddWithValue("@p4", RchDetay.Text);
            cmd.Parameters.AddWithValue("@p5", TxtOlusturan.Text);
            cmd.Parameters.AddWithValue("@p6", TxtGorev.Text);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            listele();
            MessageBox.Show("Not Bilgileri Kaydedildi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                Txtid.Text = dr["ID"].ToString();
                MskTarih.Text = dr["TARİH"].ToString();
                MskSaat.Text = dr["SAAT"].ToString();
                TxtBaslık.Text = dr["BASLIK"].ToString();
                RchDetay.Text = dr["DETAY"].ToString();
                TxtOlusturan.Text = dr["OLUSTURAN"].ToString();
                TxtGorev.Text = dr["GÖREV"].ToString();

            }
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("Delete from TBL_NOTLAR where ID=@p1", baglanti);
            cmd.Parameters.AddWithValue("@p1", Txtid.Text);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            listele();
            MessageBox.Show("Not Listeden Silindi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("Update TBL_NOTLAR set TARİH=@p1,SAAT=@p2,BASLIK=@p3,DETAY=@p4,OLUSTURAN=@p5,GÖREV=@p6 where ID=@p7", baglanti);
            cmd.Parameters.AddWithValue("@p1", MskTarih.Text);
            cmd.Parameters.AddWithValue("@p2", MskSaat.Text);
            cmd.Parameters.AddWithValue("@p3", TxtBaslık.Text);
            cmd.Parameters.AddWithValue("@p4", RchDetay.Text);
            cmd.Parameters.AddWithValue("@p5", TxtOlusturan.Text); 
            cmd.Parameters.AddWithValue("@p6", TxtGorev.Text);
            cmd.Parameters.AddWithValue("@p7", Txtid.Text);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Not Bilgileri Güncellendi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmNotDetay fr = new FrmNotDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                fr.metin = dr["DETAY"].ToString();
            }
            fr.Show();
        }
    }
}
