using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace threads2
{
    class AsansorHareket
    {

        public void HedefBelirle(Kuyruk aktifKuyruk, Asansor asansor)//mainde yazılacak fonksiyona musteriTasi fonksiyonu ile aynı id'li asansor parametre olarak veilecek
        {
            int Min = 5;

            lock (aktifKuyruk.MusteriKuyrugu)
            {

                foreach (var item in aktifKuyruk.MusteriKuyrugu)
                {
                    if (item.AsansorCagirdimi == false && Min > Math.Abs(asansor.SuAnKat - item.KatNo))
                    {

                        asansor.HedefMusteri = item;
                        Min = Math.Abs(asansor.SuAnKat - item.KatNo);
                    }

                }

                foreach (var item in aktifKuyruk.MusteriKuyrugu)
                {
                    if (asansor.HedefMusteri == item)
                    {
                        item.AsansorCagirdimi = true;
                       // break;
                    }
                }
               // Console.WriteLine("Asansörün hedefi {0}. kattaki {1} kişi",asansor.HedefMusteri.KatNo,asansor.HedefMusteri.MusteriSayisi);
                Min = 5;

                //asansor.HedefMusteri = aktifKuyruk.MusteriKuyrugu[asansor.AsansorNo];
                //asansor.HedefKat = aktifKuyruk.MusteriKuyrugu[asansor.AsansorNo].HedefKat;



            }

        }
        //----------------------------------------------------------------------------------------------------------------
        public void MusteriAlmayaGit(Asansor asansor)
        {
            lock (asansor)
            {
                while (asansor.HedefMusteri.KatNo != asansor.SuAnKat)
                {
                    if (asansor.HedefMusteri.KatNo < asansor.SuAnKat)
                    {
                        asansor.SuAnKat--;
                        asansor.Yon = "Asansör müşteri almak için aşağı iniyor";
                        //Console.WriteLine(asansor.Yon);

                    }
                    else if (asansor.HedefMusteri.KatNo > asansor.SuAnKat)
                    {
                        asansor.SuAnKat++;
                        asansor.Yon = "Asansör müşteri almak için yukarı çıkıyor";
                        //Console.WriteLine(asansor.Yon);

                    }
                    Thread.Sleep(200);
                }
                //Console.WriteLine(asansor.Yon);
                if (asansor.HedefMusteri.KatNo == asansor.SuAnKat)
                {
                    asansor.Yon = "Asansör müşteri almak için durdu.";
                    //Console.WriteLine(asansor.Yon);

                }

            }
        }

        //---------------------------------------------------------------------------------------------------------------------

       
        public void MusteriAl(Kuyruk aktifKuyruk, Asansor asansor)
        {
            lock (aktifKuyruk)
            {
                if (asansor.HedefMusteri.KatNo == asansor.SuAnKat)
                {
                    asansor.IcindekiMusteri = asansor.HedefMusteri;
                    asansor.MevcutSayi = asansor.IcindekiMusteri.MusteriSayisi;
                    asansor.HedefKat = asansor.IcindekiMusteri.HedefKat;
                    aktifKuyruk.KuyruktanCikar(asansor.HedefMusteri);//bu satır çok önemli
                    //Console.WriteLine("Asansör şu katta: " + asansor.SuAnKat);
                    //Console.WriteLine("Asansör müşteri alıyor");
                    //Console.WriteLine("Asansörün içinde {0}. kattan aldığı {1} kişi var.",asansor.IcindekiMusteri.KatNo,asansor.IcindekiMusteri.MusteriSayisi);
                    
                }
               
            }


        }

        //---------------------------------------------------------------------------------------------------------------------
        public void MusteriBirakmayaGit(Asansor asansor)
        {
            lock (asansor)
            {
                while (asansor.IcindekiMusteri.HedefKat != asansor.SuAnKat)
                {
                    if (asansor.IcindekiMusteri.HedefKat < asansor.SuAnKat)
                    {
                        asansor.SuAnKat--;
                        asansor.Yon = "Asansör müşteri bırakmak için aşağı iniyor";
                      //  Console.WriteLine(asansor.Yon);

                    }
                    else if (asansor.IcindekiMusteri.HedefKat > asansor.SuAnKat)
                    {
                        asansor.SuAnKat++;
                        asansor.Yon = "Asansör müşteri bırakmak için yukarı çıkıyor";
                        //Console.WriteLine(asansor.Yon);

                    }
                    Thread.Sleep(200);
                }
                //Console.WriteLine(asansor.Yon);
                if (asansor.IcindekiMusteri.HedefKat == asansor.SuAnKat)
                {
                    asansor.Yon = "Asansör müşteri bırakmak için durdu.";
                    //Console.WriteLine(asansor.Yon);

                }

            }
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void MusteriBirak(Kuyruk aktifKuyruk, Asansor asansor)
        {
            lock (aktifKuyruk.MusteriKuyrugu)
            {
                if (asansor.IcindekiMusteri.HedefKat == asansor.SuAnKat)
                {
                    if (asansor.IcindekiMusteri.HedefKat == 0)
                    {

                        aktifKuyruk.KuyruktanCikar(asansor.IcindekiMusteri);
                        //aktifKuyruk.KuyruktanCikar(asansor.HedefMusteri);//şimdi ekledin deneme

                      //  Console.WriteLine("Asansör {0}. katta müşteri bıraktı.", asansor.SuAnKat);
                    }
                    else
                    {
                        
                        asansor.IcindekiMusteri.HedefKat = 0;
                        asansor.IcindekiMusteri.KatNo = asansor.SuAnKat;
                        asansor.IcindekiMusteri.AsansorCagirdimi = false;
                        aktifKuyruk.KuyrugaEkle(asansor.IcindekiMusteri);
                        //Console.WriteLine("Asansör {0}. katta müşteri bıraktı.", asansor.SuAnKat);
                    }



                }
               
            }


        }

















    }
}
