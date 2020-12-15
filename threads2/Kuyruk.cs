using System;
using System.Collections.Generic;
using System.Text;

namespace threads2
{
    class Kuyruk
    {
        public List<Musteri> MusteriKuyrugu = new List<Musteri>();


        public int KuyrukToplam()
        {
            int toplam = 0;
            lock (MusteriKuyrugu)
            {
                foreach (var item in MusteriKuyrugu)
                {
                    toplam += item.MusteriSayisi;
                }


                return toplam;
            }

        }

        public void KuyrugaEkle(Musteri musteri)
        {
            lock (MusteriKuyrugu)
            {
                MusteriKuyrugu.Add(musteri);
            }

        }

        public void KuyruktanCikar(Musteri musteri)
        {
            lock (MusteriKuyrugu)
            {
                //MusteriKuyrugu.RemoveAt(MusteriKuyrugu.IndexOf(musteri));
                MusteriKuyrugu.Remove(musteri);
            }

        }
     

    }
}
