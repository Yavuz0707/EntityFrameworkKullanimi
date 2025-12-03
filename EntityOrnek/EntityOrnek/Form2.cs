using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityOrnek
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }



        DbSinavOgrenciEntities db = new DbSinavOgrenciEntities();
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {

                var degerler = db.TBLNOTLAR.Where(x => x.SINAV1 < 50);
                dataGridView1.DataSource = degerler.ToList();

            }

            if (radioButton2.Checked == true)
            {

                var degerler = db.TBLOGRENCI.Where(x => x.AD == "ali");
                dataGridView1.DataSource = degerler.ToList();

            }
            if (radioButton3.Checked == true)
            {

                var degerler = db.TBLOGRENCI.Where(x => x.AD == textBox1.Text || x.SOYAD == textBox1.Text);
                dataGridView1.DataSource = degerler.ToList();

            }
            if (radioButton4.Checked == true)
            {

                var degerler = db.TBLOGRENCI.Select(x => new { soyadı = x.SOYAD }); // sadece istediğimiz değeri getirmek için
                dataGridView1.DataSource = degerler.ToList();

            }

            if (radioButton5.Checked == true)
            {

                var degerler = db.TBLOGRENCI.Select(x => new { Ad = x.AD.ToUpper(), Soyad = x.SOYAD.ToLower() }).Where(x => x.Ad != "Ali"); // şartlı sorgu
                dataGridView1.DataSource = degerler.ToList();

            }
            if (radioButton6.Checked == true)
            {

                var degerler = db.TBLOGRENCI.Select(x => new { Ad = x.AD.ToUpper(), Soyad = x.SOYAD.ToLower() });
                dataGridView1.DataSource = degerler.ToList();

            }

            if (radioButton7.Checked == true)
            {

                var degerler = db.TBLNOTLAR.Select(x => new
                {
                    OgrenciAd = x.OGR,
                    Ortalama = x.ORTALAMA,
                    Durum = x.DURUM == true ? "Geçti" : "Kaldı",


                }
                );
                dataGridView1.DataSource = degerler.ToList();

            }
            if (radioButton8.Checked == true)
            {

                var degerler = db.TBLNOTLAR.SelectMany(x => db.TBLOGRENCI.Where(y => y.ID == x.OGR), (x, y) => new
                {
                    y.AD,
                    x.ORTALAMA,

                });
                dataGridView1.DataSource = degerler.ToList();

            }
            if (radioButton9.Checked == true)
            {

                var degerler = db.TBLOGRENCI.OrderBy(x => x.ID).Take(3);
                dataGridView1.DataSource = degerler.ToList();

            }
            if (radioButton10.Checked == true)
            {

                var degerler = db.TBLOGRENCI.OrderByDescending(x => x.ID).Take(3);
                dataGridView1.DataSource = degerler.ToList();

            }
            if (radioButton11.Checked == true)
            {

                var degerler = db.TBLOGRENCI.OrderBy(x => x.AD);
                dataGridView1.DataSource = degerler.ToList();

            }

            if (radioButton12.Checked == true)
            {

                var degerler = db.TBLOGRENCI.OrderBy(x => x.ID).Skip(5);
                dataGridView1.DataSource = degerler.ToList();

            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var degerler = db.TBLOGRENCI.OrderBy(x => x.SEHİR).GroupBy(y => y.SEHİR).Select(z => new
            {
                Şehir = z.Key, Toplam = z.Count()
            });

            dataGridView1.DataSource = degerler.ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label2.Text = db.TBLNOTLAR.Max(x=>x.ORTALAMA.ToString());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label3.Text = db.TBLNOTLAR.Min(x => x.ORTALAMA.ToString());
        }
    }
}
