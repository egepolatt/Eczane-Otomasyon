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
    public partial class FrmIlaclar : Form
    {
        public FrmIlaclar()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-8JKFN02\SQLEXPRESS;Initial Catalog=EczaneOtomasyon;Integrated Security=True");
        Sqlbaglantisi bgl = new Sqlbaglantisi();
        
        void listele()
        {

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select*From TBL_ILACLAR",baglanti);
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void temizle()
        {
            Txtid.Text = "";
            TxtIlacAdı.Text = "";
            TxtIlacTuru.Text = "";
            TxtSatıs.Text = "";
            TxtAlıs.Text = "";
            RchDetay.Text = "";
            NudAdet.Text = "";
        }

        private void FrmIlaclar_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        private class Sqlbaglantisi
        {

        }
        private void BtnKaydet_Click(object sender, EventArgs e)
        {


            baglanti.Open();           
            SqlCommand cmd = new SqlCommand("insert into TBL_ILACLAR (ILACTURU,ILACADI,ADET,[ALISFIYATI(TL)],[SATISFIYATI(TL)],DETAY) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
            cmd.Parameters.AddWithValue("@p1", TxtIlacTuru.Text);
            cmd.Parameters.AddWithValue("@p2", TxtIlacAdı.Text);
            cmd.Parameters.AddWithValue("@p3", int.Parse((NudAdet.Value).ToString()));
            cmd.Parameters.AddWithValue("@p4", decimal.Parse(TxtAlıs.Text));
            cmd.Parameters.AddWithValue("@p5", decimal.Parse(TxtSatıs.Text));
            cmd.Parameters.AddWithValue("@p6", RchDetay.Text);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün Sisteme Eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {

            baglanti.Open();
            SqlCommand cmdsil = new SqlCommand("Delete From TBL_ILACLAR where ID=@p1",
                baglanti);
            cmdsil.Parameters.AddWithValue("@p1", Txtid.Text);
            cmdsil.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün Silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            listele();
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            Txtid.Text = dr["ID"].ToString();
            TxtIlacTuru.Text = dr["ILACTURU"].ToString();
            TxtIlacAdı.Text = dr["ILACADI"].ToString();
            NudAdet.Value = decimal.Parse(dr["ADET"].ToString());
            TxtAlıs.Text = dr["ALISFIYATI(TL)"].ToString();
            TxtSatıs.Text = dr["SATISFIYATI(TL)"].ToString();
            RchDetay.Text = dr["DETAY"].ToString();

        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {

            baglanti.Open();
            SqlCommand cmd = new SqlCommand("update TBL_ILACLAR set ILACTURU=@p1,ILACADI=@p2,ADET=@p3,[ALISFIYATI(TL)]=@p4,[SATISFIYATI(TL)]=@p5,DETAY=@p6 where ID=@p7", baglanti);
            cmd.Parameters.AddWithValue("@p1", TxtIlacTuru.Text);
            cmd.Parameters.AddWithValue("@p2", TxtIlacAdı.Text);
            cmd.Parameters.AddWithValue("@p3", int.Parse((NudAdet.Value).ToString()));
            cmd.Parameters.AddWithValue("@p4", decimal.Parse(TxtAlıs.Text));
            cmd.Parameters.AddWithValue("@p5", decimal.Parse(TxtSatıs.Text));
            cmd.Parameters.AddWithValue("@p6", RchDetay.Text);
            cmd.Parameters.AddWithValue("@p7", Txtid.Text);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
            temizle();

        }


        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
