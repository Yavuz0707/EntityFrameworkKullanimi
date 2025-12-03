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
using System.Security.Cryptography;
using System.ComponentModel.Design.Serialization;

namespace EntityOrnek
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DbSinavOgrenciEntities db = new DbSinavOgrenciEntities();
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnListele_Click(object sender, EventArgs e)
        {

            dataGridView1.DataSource = db.TBLOGRENCI.ToList();  //Bütün Öğrencileri Listele
            dataGridView1.Columns[3].Visible = false; // 3.sütun gözükmesin.
            dataGridView1.Columns[4].Visible = false;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }


        private void BtnDersListesi_Click(object sender, EventArgs e)
        {



            var degerler = db.TBLDERSLER
                             .Select(x => new
                             {
                                 x.DERSID,
                                 x.DERSAD
                             }).ToList();

            dataGridView1.DataSource = degerler;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BtnNotListesi_Click(object sender, EventArgs e)
        {
            // dataGridView1.DataSource = db.TBLNOTLAR.ToList(); -> İlişkili olan tabloların ekrana gelmemesi için.

            var query = from item in db.TBLNOTLAR select new { item.NOTID, item.OGR, item.DERS, item.SINAV1, item.SINAV2, item.SINAV3 };

            dataGridView1.DataSource = query.ToList();

        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            TBLOGRENCI t = new TBLOGRENCI();

            t.AD = TxtAd.Text; //TxtAd TextBox ından gelen txt değeri ad a aktarılıdı.
            t.SOYAD = TxtSoyad.Text;

            db.TBLOGRENCI.Add(t); //Ekleme için.
            db.SaveChanges();
            MessageBox.Show("İşlem Başarılı...");


            TBLDERSLER td = new TBLDERSLER();

            td.DERSAD = TxtDersAd.Text;

            db.TBLDERSLER.Add(td);
            db.SaveChanges();



        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(TxtOgrenciID.Text);   //Text box ta girilen veri

            var x = db.TBLOGRENCI.Find(id);  // id ye göre silme işlemi

            db.TBLOGRENCI.Remove(x); // x'ten gelen değeri kaldır.

            db.SaveChanges();

            MessageBox.Show("İşlem Başarılı...");

            // genellikle ilişkili tablolarda silme işlemi yapılmaz, onun yerine aktif pasif işlemi yapılır.
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(TxtOgrenciID.Text);

            var x = db.TBLOGRENCI.Find(id);

            x.AD = TxtAd.Text; // TxtAd.Text e girilen değer yeni ad olsun.
            x.SOYAD = TxtSoyad.Text;
            x.FOTOGRAF = TxtFoto.Text;
            db.SaveChanges();

            MessageBox.Show("İşlem Başarılı...");



        }

        private void BtnProsedur_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.NOTLISTESI(); // yazdığımız prosedürü ekrana yazdırma.
        }

        private void TxtFoto_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnBul_Click(object sender, EventArgs e)
        {
            // lambda ifade kullanımı
            dataGridView1.DataSource = db.TBLOGRENCI.Where(x => x.AD == TxtAd.Text | x.SOYAD == TxtSoyad.Text).ToList();
        }

        private void TxtAd_TextChanged(object sender, EventArgs e)
        {
            string aranan = TxtAd.Text; // textbox'a bir veri girildiği anda o isme ait olan kişileri getirme.
            var degerler = from s in db.TBLOGRENCI
                           where s.AD.Contains(aranan) // içerdiği değeri getir.
                           select s;
            dataGridView1.DataSource = degerler.ToList();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void BtnLinqEntity_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                List<TBLOGRENCI> liste1 = db.TBLOGRENCI.OrderBy(p => p.AD).ToList(); // radio butona basıldığında TBLOGRENCİ lerdeki değerleri ada göre sıralar.
                dataGridView1.DataSource = liste1;
            }

            if (radioButton2.Checked == true)
            {

                List<TBLOGRENCI> liste2 = db.TBLOGRENCI.OrderByDescending(p => p.AD).ToList();
                dataGridView1.DataSource = liste2;

            }

            if (radioButton3.Checked == true)
            {

                List<TBLOGRENCI> liste3 = db.TBLOGRENCI.OrderBy(x => x.ID).Take(3).ToList(); // ilk 3 elemanı al.
                dataGridView1.DataSource = liste3;

            }

            if (radioButton4.Checked == true)
            {

                List<TBLOGRENCI> liste4 = db.TBLOGRENCI.OrderByDescending(s => s.ID).Take(3).ToList();
                dataGridView1.DataSource = liste4;

            }

            if (radioButton5.Checked == true)
            {
                List<TBLOGRENCI> liste5 = db.TBLOGRENCI.Where(x => x.AD.StartsWith("A")).ToList();
                dataGridView1.DataSource = liste5;
            }

            if (radioButton6.Checked == true)
            {
                List<TBLOGRENCI> liste6 = db.TBLOGRENCI.Where(x => x.AD.EndsWith("A")).ToList();
                dataGridView1.DataSource = liste6;
            }

            if (radioButton7.Checked == true)
            {
                bool deger = db.TBLOGRENCI.Any();
                MessageBox.Show(deger.ToString(), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (radioButton8.Checked == true)
            {
                int toplam = db.TBLOGRENCI.Count();
                MessageBox.Show(toplam.ToString());
            }
            if (radioButton9.Checked == true)
            {
                var ortalama = db.TBLNOTLAR.Average(s => s.SINAV1);
                MessageBox.Show(ortalama.ToString());
            }
            if (radioButton10.Checked == true)
            {

                List<TBLNOTLAR> listOrt = db.TBLNOTLAR.Where(p => p.SINAV1 > 60).ToList();
                dataGridView1.DataSource = listOrt;

            }

            if (radioButton11.Checked == true)
            {

                List<TBLNOTLAR> listeY = db.TBLNOTLAR.OrderByDescending(x => x.SINAV1).Take(1).ToList();
                dataGridView1.DataSource = listeY;

            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void BtnJoin_Click(object sender, EventArgs e)
        {
            var sorgu = from d1 in db.TBLNOTLAR
                        join d2 in db.TBLOGRENCI
                        on d1.OGR equals d2.ID
                        select new
                        {
                            ÖĞRENCİ=d2.AD,
                            SOYAD=d2.SOYAD,
                            SINAV1 = d1.SINAV1,
                            SINAV2 = d1.SINAV2,
                            SINAV3 = d1.SINAV3,
                            ORTALAMA = d1.ORTALAMA,
                        };
            dataGridView1.DataSource = sorgu;
        }
    }
}
