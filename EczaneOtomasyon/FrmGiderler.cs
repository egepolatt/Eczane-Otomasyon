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
    public partial class FrmGiderler : Form
    {
        public FrmGiderler()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-8JKFN02\SQLEXPRESS;Initial Catalog=EczaneOtomasyon;Integrated Security=True");
        Sqlbaglantisi bgl = new Sqlbaglantisi();

        void giderlistesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select*from TBL_GIDERLER Order By ID Asc", baglanti);
            da.Fill(dt);
            gridControl1.DataSource = dt;

        }
        void temizle()
        {
            Txtid.Text = "";
            CmbAy.Text = "";
            CmbYıl.Text = "";
            TxtElektrik.Text = "";
            TxtSu.Text = "";
            TxtDogalgaz.Text = "";
            TxtEkstra.Text = "";
            TxtInternet.Text = "";
            TxtMaaslar.Text = "";
            RchNotlar.Text = "";
        }

        private void FrmGiderler_Load(object sender, EventArgs e)
        {
            giderlistesi();
            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("insert into TBL_GIDERLER (AY,YIL,ELEKTRIK,SU,DOGALGAZ,INTERNET,MAASLAR,EKSTRA,NOTLAR) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", baglanti);
            cmd.Parameters.AddWithValue("@p1", CmbAy.Text);
            cmd.Parameters.AddWithValue("@p2", CmbYıl.Text);
            cmd.Parameters.AddWithValue("@p3", decimal.Parse(TxtElektrik.Text));
            cmd.Parameters.AddWithValue("@p4", decimal.Parse(TxtSu.Text));
            cmd.Parameters.AddWithValue("@p5", decimal.Parse(TxtDogalgaz.Text));
            cmd.Parameters.AddWithValue("@p6", decimal.Parse(TxtInternet.Text));
            cmd.Parameters.AddWithValue("@p7", decimal.Parse(TxtMaaslar.Text));
            cmd.Parameters.AddWithValue("@p8", decimal.Parse(TxtEkstra.Text));
            cmd.Parameters.AddWithValue("@p9", RchNotlar.Text);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Gider Bilgileri Kaydedildi", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            giderlistesi();
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                Txtid.Text = dr["ID"].ToString();
                CmbAy.Text = dr["AY"].ToString();
                CmbYıl.Text = dr["YIL"].ToString();
                TxtElektrik.Text = dr["ELEKTRIK"].ToString();
                TxtSu.Text = dr["SU"].ToString();
                TxtDogalgaz.Text = dr["DOGALGAZ"].ToString();
                TxtInternet.Text = dr["INTERNET"].ToString();
                TxtMaaslar.Text = dr["MAASLAR"].ToString();
                TxtEkstra.Text = dr["EKSTRA"].ToString();
                RchNotlar.Text = dr["NOTLAR"].ToString();
            }
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("Delete from TBL_GIDERLER where ID=@p1", baglanti);
            cmd.Parameters.AddWithValue("@p1", Txtid.Text);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            giderlistesi();
            MessageBox.Show("Gider Listeden Silindi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            temizle();

        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("Update TBL_GIDERLER set AY=@p1,YIL=@p2,ELEKTRIK=@p3,SU=@p4,DOGALGAZ=@p5,INTERNET=@p6,MAASLAR=@p7,EKSTRA=@p8,NOTLAR=@p9 where ID=@p10", baglanti);
            cmd.Parameters.AddWithValue("@p1", CmbAy.Text);
            cmd.Parameters.AddWithValue("@p2", CmbYıl.Text);
            cmd.Parameters.AddWithValue("@p3", decimal.Parse(TxtElektrik.Text));
            cmd.Parameters.AddWithValue("@p4", decimal.Parse(TxtSu.Text));
            cmd.Parameters.AddWithValue("@p5", decimal.Parse(TxtDogalgaz.Text));
            cmd.Parameters.AddWithValue("@p6", decimal.Parse(TxtInternet.Text));
            cmd.Parameters.AddWithValue("@p7", decimal.Parse(TxtMaaslar.Text));
            cmd.Parameters.AddWithValue("@p8", decimal.Parse(TxtEkstra.Text));
            cmd.Parameters.AddWithValue("@p9", RchNotlar.Text);
            cmd.Parameters.AddWithValue("@p10", Txtid.Text);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Gider Bilgileri Güncellendi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            giderlistesi();
            temizle();
        }
    }
}
