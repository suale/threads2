using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace threads2
{
    class AsansorHareket
    {

        public void HedefBelirle(Kuyruk aktifKuyruk, Asansor asansor)
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
                       
                    }
                }
               
                Min = 5;               
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
                    }
                    else if (asansor.HedefMusteri.KatNo > asansor.SuAnKat)
                    {
                        asansor.SuAnKat++;
                        asansor.Yon = "Asansör müşteri almak için yukarı çıkıyor";                        
                    }
                    Thread.Sleep(200);
                }               
                if (asansor.HedefMusteri.KatNo == asansor.SuAnKat)
                {
                    asansor.Yon = "Asansör müşteri almak için durdu.";
                    asansor.MevcutSayi = asansor.HedefMusteri.MusteriSayisi;
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
                    aktifKuyruk.KuyruktanCikar(asansor.HedefMusteri);                    
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
                     
                    }
                    else if (asansor.IcindekiMusteri.HedefKat > asansor.SuAnKat)
                    {
                        asansor.SuAnKat++;
                        asansor.Yon = "Asansör müşteri bırakmak için yukarı çıkıyor";

                    }
                    Thread.Sleep(200);
                }
               
                if (asansor.IcindekiMusteri.HedefKat == asansor.SuAnKat)
                {
                    asansor.Yon = "Asansör müşteri bırakmak için durdu.";
                    asansor.MevcutSayi = 0;
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
                    }
                    else
                    {
                        asansor.IcindekiMusteri.HedefKat = 0;
                        asansor.IcindekiMusteri.KatNo = asansor.SuAnKat;
                        asansor.IcindekiMusteri.AsansorCagirdimi = false;
                        aktifKuyruk.KuyrugaEkle(asansor.IcindekiMusteri);
                    }
                }               
            }

        }

        public void YeniAsansorHareket(Asansor asansor, Kuyruk musteriKuyruguListesi, AsansorHareket hareket)
        {
            while (asansor.AktifMi == true && musteriKuyruguListesi.MusteriKuyrugu.Count > 0)
            {

                hareket.HedefBelirle(musteriKuyruguListesi, asansor);
                hareket.MusteriAlmayaGit(asansor);
                hareket.MusteriAl(musteriKuyruguListesi, asansor);
                hareket.MusteriBirakmayaGit(asansor);
                hareket.MusteriBirak(musteriKuyruguListesi, asansor);
                Thread.Sleep(500);
            }
        }

    }
}
