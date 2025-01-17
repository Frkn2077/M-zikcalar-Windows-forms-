using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace çift_yönlü_bağlı_liste_uygulaması_Müzikçalar_
{
    public partial class Form1 : Form
    {
       
        static  Bagliliste bl=new Bagliliste();//sarkılar için
        Bagliliste bl1 =new Bagliliste();//playlist için
        static Bagliliste bl2 =new Bagliliste();//aktif kullanılacak olan playlist için
        
        public Form1()
        {
            InitializeComponent();
           
            comboBox1.Items.Add("1-Playlistlerden sil.");
            comboBox1.Items.Add("2- Müziklerden sil.");
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(10, 10);
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(280, 280);
            timer1.Start();
            getır();
        }

      
        public class Dugum
        {
            public string veri;
            public Dugum sonraki;
            public Dugum önceki;
            public Dugum(string veri)
            {
                this.veri = veri;
                this.sonraki = null;
                this.önceki = null;

            }
        }
        //Ana metotlar 
        public class Bagliliste
        {
            private Dugum bas;
            private Dugum son;
            public Bagliliste()
            {
               this.bas = null;
               this.son = null;
            }
           
            public void basaekle(Dugum yeni)
            {
                if (bas == null)
                {
                    bas = yeni;
                    son = yeni;
                }
                else
                {
                    bas.önceki = yeni;
                    yeni.sonraki = bas;
                    bas = yeni;
                    bas.önceki = null;
                }
            }
            public void ortayaekle(Dugum yeni)
            {
                Dugum temp = bas;
                Dugum d1 = bas;
              
                if (bas == null)
                {
                    bas = yeni;
                    son = yeni;
                    yeni.sonraki = null;
                    yeni.önceki = null;
                    return;
                }
                else
                {
                    int orta = 1;
                    int luzunluk = 0;
                    while (temp != null) 
                    {
                        luzunluk++;
                        temp = temp.sonraki;
                    }
                    orta = luzunluk / 2;
                 
                    
                    for (int i = 0; i < orta-1; i++) 
                    {
                        d1 = d1.sonraki;
                        orta1 = d1;
                    }
                    yeni.sonraki = d1.sonraki;
                    if (d1.sonraki != null) 
                    {
                        d1.sonraki.önceki = yeni;
                       
                       

                    }
                   
                    d1.sonraki = yeni;
                    yeni.önceki = d1;
                    
                }


            }
            public void sonaekle(Dugum yeni)
                {
                if (bas == null)
                {
                    bas = yeni;
                    son = yeni;
                }
                else
                {
                    son.sonraki = yeni;
                    yeni.önceki = son;
                    son = yeni;
                    son.sonraki = null;
                }
            }

            public string döndür() 
            {
                Dugum y = bas;
                if (bas == null)
                {
                    return null; 
                }
                string m3=y.veri;
                return m3;
            }
            public string döndür1()
            {
                Dugum y = son;
                if (son == null)
                {
                    return null;
                }
                string m3 = y.veri;
                return m3;
            }
          
            public void bastansil()
            {
                if (bas == null) 
                {
                    return;
                }
                    bas = bas.sonraki;
                    if (bas != null) 
                    {
                    bas.önceki = null;
                    }
            }
            Dugum orta1;
            public string dondur2() 
            {
                string m2=orta1.veri;
                if (orta1 == null)
                {
                    return null; 
                }
                return m2;
            }
            public void ortadansil()
            {
                if (bas == null) 
                {
                    return;
                }   
                Dugum temp = bas;
                Dugum d1 = bas;

                if (bas == null)
                {
                    bas = bas.sonraki;
                    if (bas != null)
                    {
                        bas.önceki = null;
                    }
                    return;
                }
                
                int orta = 1;
                    int luzunluk = 0;
                    while (temp != null)
                    {
                        luzunluk++;
                        temp = temp.sonraki;
                    }
                   
                if (luzunluk == 1)
                {
                    bastansil();
                    return;
                }
                orta = luzunluk / 2;

                for (int i = 0; i < orta - 1; i++)
                {
                        d1 = d1.sonraki;
                      
                }
                if (d1.sonraki != null) 
                {
                    d1.sonraki.önceki = d1.önceki;
                }
                   if (d1.önceki != null) 
                    {
                        d1.önceki.sonraki = d1.sonraki;
                    }
                   

            } 
                public void sondansil()
                {
                if (son == null) 
                {
                    return;
                }
                if (son==bas)
                {
                    bas = null;
                    son = null;

                }
                else 
                {
                    son = son.önceki;
                    if (son != null) 
                    {
                        son.sonraki = null;
                    }
                   
                }
               
               
            }       
            public void yazdir(ListBox listBox) ///playlist
            {
               listBox.Items.Clear();
               Dugum yeni = bas;
                while(yeni != null) 
                {
                    listBox.Items.Add(yeni.veri);
                    yeni = yeni.sonraki;
                }
            }
            public void D_oku(ListBox listBox, string h)
            {
                listBox.Items.Clear();
                string dy1 = "C:\\Users\\Furkan\\Desktop\\My Playlists"+"\\"+ h + ".txt";

                if (File.Exists(dy1))
                {
                    string[] x = File.ReadAllLines(dy1);
                    foreach (string s1 in x)
                    {
                        listBox.Items.Add(s1);
                    }
                }
                else
                {
                    MessageBox.Show("Seçilen playlist dosyası bulunamadı.");
                }
            }


            public void Dyazdir(string pl)//şarkı
            {
                Dugum yeni = bas;

                string m1 = " ";
                string dy = "C:\\Users\\Furkan\\Desktop\\My Playlists"+"\\" +pl + ".txt";

                while (yeni != null)
                {
                    m1+=yeni.veri+Environment.NewLine;
                    yeni = yeni.sonraki;
                }
                File.WriteAllText(dy,m1);
               
                
               



            }
            
            public void Temizle() 
            {
                Dugum temp = bas;

                while (temp != null)
                {
                    Dugum temp1 = temp.sonraki;
                    temp.sonraki = null; 
                    temp = temp1;
                }

                bas = null;
            }
            public void ListeyiKopyala(Bagliliste kaynakListe)
            {
                Dugum mevcut = kaynakListe.bas;
                this.Temizle();
                while (mevcut != null)
                {
                    Dugum yeniDugum = new Dugum(mevcut.veri); // Kaynaktaki veri ile yeni bir düğüm oluştur
                    this.sonaekle(yeniDugum); // Hedef listeye ekle (bu örnekte sona ekleme yapıyoruz)

                    mevcut = mevcut.sonraki; // Bir sonraki düğüme geç
                }
            }

            //metot sonu
        }
        public void kontrol() 
        {
            string m2 = textBox2.Text;
            string dy1 = "C:\\Users\\Furkan\\Desktop\\My Playlists\\" + m2 + ".txt";
            if (!File.Exists(dy1))
            {
                FileStream fs = File.Create(dy1);
                fs.Close();
            }
          
        }
       
        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && string.IsNullOrEmpty(textBox2.Text))
            {
                /// şarkı ekleme 
              
                string m2 = textBox1.Text;
                string dy = "C:\\Users\\Furkan\\Desktop\\deneme\\" + m2 + ".mp3";

                if (File.Exists(dy) == false)
                {
                    MessageBox.Show("Dosyanız bulunamadı lütfen tekrar deneyin");
                }
                else
                {
                    Dugum d1 = new Dugum(dy);
                    bl.basaekle(d1);
                  

                }
            }
            else if (string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
            {
                //playlist ekleme
                kontrol();
                string m2 = textBox2.Text;
                string dy1 = "C:\\Users\\Furkan\\Desktop\\My Playlists\\" + m2 + ".txt";
                Dugum d1 = new Dugum(m2);
                bl1.basaekle(d1);
            }
            else 
            {
                MessageBox.Show("iki değer aynı anda gönderilemez.");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {

            ///şarkı ekleme
            if (!string.IsNullOrEmpty(textBox1.Text) && string.IsNullOrEmpty(textBox2.Text))
            {
               
                string m2 = textBox1.Text;
                string dy = "C:\\Users\\Furkan\\Desktop\\deneme\\" + m2 + ".mp3";

                if (File.Exists(dy) == false)
                {
                    MessageBox.Show("Dosyanız bulunamadı lütfen tekrar deneyin");
                }
                else
                {
                    Dugum d1 = new Dugum(dy);
                    bl.ortayaekle(d1);
                    
                }
            }
            else if (string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
            {
                kontrol();
                string y = textBox2.Text;
                Dugum d1 = new Dugum(y);
              
                bl1.ortayaekle(d1);
            }
            else 
            {
                MessageBox.Show("Hata var");
            }
           }

        private void button5_Click(object sender, EventArgs e)
        {
            //şarkı ekleme
            if (!string.IsNullOrEmpty(textBox1.Text) && string.IsNullOrEmpty(textBox2.Text))
            {
                if (listBox2.SelectedItems==null) 
                {
                    MessageBox.Show("Lütfen şarkı eklemek için ya playlist seçin veya playlist ekleyin");
                }
                string m2 = textBox1.Text;
                string dy = "C:\\Users\\Furkan\\Desktop\\deneme\\" + m2 + ".mp3";
                if (File.Exists(dy) == false)
                {
                    MessageBox.Show("Dosyanız bulunamadı lütfen tekrar deneyin");
                }
                else
                {
                    Dugum d1 = new Dugum(dy);
                    bl.sonaekle(d1);
                    

                    
                }
            }
            else if (string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
            {
                kontrol();
                string y = textBox2.Text;
                Dugum d1 = new Dugum(y);
                bl1.sonaekle(d1);
                
            }
            else 
            {
                MessageBox.Show("hata var");
            }
        }

       
        private void button6_Click(object sender, EventArgs e)
        {
            ///düzenle
           
            
            if (comboBox1.SelectedItem==null)
            {
                MessageBox.Show("Lütfen bir seçenek seçin!");
                return;
            }
            String m3 = comboBox1.SelectedItem.ToString();
            if (m3== "2- Müziklerden sil.")
            {

            
                bl.bastansil();
                //bl.Dyazdir(y);


            }
            else if (m3== "1-Playlistlerden sil.")
            {
                string y = bl1.döndür();
                string dy2 = "C:\\Users\\Furkan\\Desktop\\My Playlists\\" + y + ".txt";
                File.Delete(dy2);
                bl1.bastansil();
            }
            
            else
            {
                MessageBox.Show("hata-404");
            }

        }
       
        private void button7_Click(object sender, EventArgs e)
        {
            
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Lütfen bir seçenek seçin!");
                return;
            }
            String m3 = comboBox1.SelectedItem.ToString();
            if (m3 == "2- Müziklerden sil.")
            {

                
                bl.ortadansil();
                //bl.Dyazdir(y);
            }
            else if (m3 == "1-Playlistlerden sil.")
            {
                
                string y = bl1.dondur2();
                string dy2 = "C:\\Users\\Furkan\\Desktop\\My Playlists\\" + y + ".txt";
                File.Delete(dy2);
                bl1.ortadansil();
            }

            else
            {
                MessageBox.Show("hata-404");
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {

            
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Lütfen bir seçenek seçin!");
                return;
            }
            String m3 = comboBox1.SelectedItem.ToString();
            if (m3 == "2- Müziklerden sil.")
            {
                bl.sondansil();
               
                //bl.sondansil();
                //string s = listBox2.SelectedItem?.ToString();
                //bl.Dyazdir(s);
            }
            else if (m3 == "1-Playlistlerden sil.")
            {
               
                string y = bl1.döndür1();
                string dy2 = "C:\\Users\\Furkan\\Desktop\\My Playlists\\" + y + ".txt";
                File.Delete(dy2);
                bl1.sondansil();
            }

            else
            {
                MessageBox.Show("hata-404");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            bl1.yazdir(listBox2);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > 0)
            {
                listBox1.SelectedIndex--; // Önceki şarkıya geç
                string m1 = listBox1.SelectedItem.ToString();
                axWindowsMediaPlayer1.URL = m1; // Seçilen şarkıyı oynat
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < listBox1.Items.Count - 1)// listbox index kontrolü
            {
                listBox1.SelectedIndex++; // sonraki şarkıya geç
                string m1 = listBox1.SelectedItem.ToString();
                axWindowsMediaPlayer1.URL = m1; // Seçilen şarkıyı oynat
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string m1 = listBox1.SelectedItem.ToString();
            axWindowsMediaPlayer1.URL = m1;
        
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                bl.Temizle();
                string selectedPlaylist = listBox2.SelectedItem.ToString();

                bl.D_oku(listBox1, selectedPlaylist);

            }


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
       
        private void button10_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem!= null)
            {
               
                String m3 = listBox2.SelectedItem.ToString();
                listBox1.Items.Clear();
                //bl2.ListeyiKopyala(bl);
                bl.Dyazdir(m3);
                bl.D_oku(listBox1, m3);


            }
            else 
            {
                MessageBox.Show("Lütfen bir playlist seçin");
            }
            

        }

        private void button11_Click(object sender, EventArgs e)
        {
            int a = axWindowsMediaPlayer1.settings.volume;
            a= a+15;
            axWindowsMediaPlayer1.settings.volume = a;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            int a = axWindowsMediaPlayer1.settings.volume;
            a = a - 15;
            axWindowsMediaPlayer1.settings.volume = a;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void listBox2_Click(object sender, EventArgs e)
        {
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label7.Text = DateTime.Now.ToString("HH:mm:ss");
        }
        public void getır() 
        {
            String[] d1 = { "s1", "s2", "s3", "s4", "s5", "s6", "s7", "s8", "s9", "s10", "s11", "s12", "s13", "s14", "s15", "s16" };
            Random rnd = new Random();
            int s1 = rnd.Next(d1.Length);
            int sayaç = 0;
            string m1;
            string dy;
            string dy1 = "C:\\Users\\Furkan\\Desktop\\My Playlists\\" + "ozelplaylist " + ".txt";
            FileStream fs = File.Create(dy1);
            fs.Close();
          
            for (int i = d1.Length - 1; i > 0; i--)
            {
                int j = rnd.Next(0, i + 1);
               
                string temp = d1[i];
                d1[i] = d1[j];
                d1[j] = temp;
            }
           for (int i = 0; i < 5; i++)
            {
                m1 = d1[i];
                dy = "C:\\Users\\Furkan\\Desktop\\deneme\\" + m1 + ".mp3";
                File.AppendAllText(dy1, dy+Environment.NewLine);
                sayaç++;
            }
        }
        
        private void button13_Click(object sender, EventArgs e)
        {
            
            string dy1 = "C:\\Users\\Furkan\\Desktop\\My Playlists" + "\\" + "ozelplaylist " + ".txt";
            string[] x = File.ReadAllLines(dy1);
            listBox1.Items.Clear();
            foreach (string a in x)
            {
                listBox1.Items.Add(a);
            }



        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

    }
}