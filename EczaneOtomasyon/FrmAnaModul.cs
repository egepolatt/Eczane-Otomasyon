using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EczaneOtomasyon
{
    public partial class AnaModul : Form
    {
        public AnaModul()
        {
            InitializeComponent();
        }
        FrmStoklar fr7;
        private void BtnStoklar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr7 == null || fr7.IsDisposed)
            {
                fr7 = new FrmStoklar();
                fr7.MdiParent = this;
                fr7.Show();
            }

        }
        Hastalar fr2;
        private void BtnHastalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(fr2 == null || fr2.IsDisposed)
            {
                fr2 = new Hastalar();
                fr2.MdiParent = this;
                fr2.Show();
            }

        }
        FrmIlaclar fr;
        private void BtnIlaclar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(fr==null || fr.IsDisposed) { 
            fr = new FrmIlaclar();
            fr.MdiParent = this;
            fr.Show();
            }
        }
        FrmAyarlar fr8;
        private void BtnAyarlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr8 == null || fr8.IsDisposed)
            {
                fr8 = new FrmAyarlar();
                fr8.Show();
            }
        }


        Notlar fr6;
        private void BtnNotlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr6 == null || fr6.IsDisposed)
            {
                fr6 = new Notlar();
                fr6.MdiParent = this;
                fr6.Show();
            }
        }

        FrmGiderler fr5;
        private void BtnGiderler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr5 == null || fr5.IsDisposed)
            {
                fr5 = new FrmGiderler();
                fr5.MdiParent = this;
                fr5.Show();
            }
        }
        FrmPersonel fr4;
        private void BtnPersoneller_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if( fr4 == null || fr4.IsDisposed)
            {
                fr4 = new FrmPersonel();
                fr4.MdiParent = this;
                fr4.Show();
            }
        }
        FrmFirmalar fr3;
        private void BtnFirmalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(fr3 == null || fr3.IsDisposed)
            {
                fr3 = new FrmFirmalar();
                fr3.MdiParent = this;
                fr3.Show();
            }
        }
        FrmAnaSayfa fr9;
        private void BtnAnaSayfa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr9 == null || fr9.IsDisposed)
            {
                fr9 = new FrmAnaSayfa();
                fr9.MdiParent = this;
                fr9.Show();
            }
        }

        private void AnaModul_Load(object sender, EventArgs e)
        {

        }
    }
}
