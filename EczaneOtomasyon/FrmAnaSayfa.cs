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
    public partial class FrmAnaSayfa : Form
    {
        public FrmAnaSayfa()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-8JKFN02\SQLEXPRESS;Initial Catalog=EczaneOtomasyon;Integrated Security=True");
        Sqlbaglantisi bgl = new Sqlbaglantisi();

        void stoklar()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select ILACADI,SUM(ADET) As 'ADET' from TBL_ILACLAR Group By ILACADI having Sum(adet) <= 15 Order By Sum(adet)", baglanti);
            da.Fill(dt);
            gridControlStoklar.DataSource = dt;

        }
        private void FrmAnaSayfa_Load(object sender, EventArgs e)
        {
            stoklar();
            webBrowser1.Navigate("https://www.tcmb.gov.tr/wps/wcm/connect/tr/tcmb+tr/main+page+site+area/bugun");
            webBrowser2.Navigate("https://www.youtube.com/");
        }
    }
}
