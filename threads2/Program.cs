using System;
using System.Threading;
using System.Collections.Generic;



namespace threads2
{


    class Program
    {


        public static void Main()
        {


            KatAsansor avmKur = new KatAsansor();//Asansörleri ve katları tanımlar +2

            avmKur.KatYap();
            avmKur.AsansorYap();
            
            avmKur.asansorler[1].AktifMi = true;//silincek

            ManualResetEvent _event = new ManualResetEvent(true);

            Kuyruk musteriKuyruguListesi = new Kuyruk();//Bütün katlardaki yolcu gruplarını listeler

            Musteri musteriOrnek = new Musteri
            {
                HedefKat = 4,
                KatNo = 0,                   //Program ilk çalıştırıldığında listede obje bulunması için initial değer.
                MusteriSayisi = 1
            };
            musteriKuyruguListesi.MusteriKuyrugu.Add(musteriOrnek);//silincek
            musteriKuyruguListesi.MusteriKuyrugu.Add(musteriOrnek);//silincek

            AsansorHareket hareket = new AsansorHareket();

            //=========================================================================================================================

            void loginYap()//Zemin kattan avm'ye giriş yapılmasını sağlar
            {


                for (int i = 0; i < 10; i++)
                {
                    Musteri gelen = new Musteri();
                    System.Random rnd = new System.Random();
                    gelen.MusteriSayisi = rnd.Next(1, 11);
                    gelen.HedefKat = rnd.Next(1, 5);
                    gelen.KatNo = 0;
                    musteriKuyruguListesi.KuyrugaEkle(gelen);
                    Thread.Sleep(100);//500 olcak


                }

            }

            //=========================================================================================================================
            void exitYap()//Diğer katlarda exit'e gidecek yolcuları kuyruğa ekler
            {


                for (int i = 0; i < 10; i++)
                {
                    Musteri gelen = new Musteri();
                    System.Random rnd = new System.Random();
                    gelen.MusteriSayisi = rnd.Next(1, 6);
                    gelen.HedefKat = 0;
                    gelen.KatNo = rnd.Next(1, 5);
                    musteriKuyruguListesi.KuyrugaEkle(gelen);
                    //Console.WriteLine(gelen.musteriSayisi + " " + gelen.hedefKat);
                    Thread.Sleep(1000);//1000 olcak

                }

            }

            //=========================================================================================================================
            void asansorHareket()
            {
                while (avmKur.asansorler[0].AktifMi == true && musteriKuyruguListesi.MusteriKuyrugu.Count > 0)
                {
                   
                        hareket.HedefBelirle(musteriKuyruguListesi, avmKur.asansorler[0]);
                  
                        hareket.MusteriAlmayaGit(avmKur.asansorler[0]);
                        hareket.MusteriAl(musteriKuyruguListesi, avmKur.asansorler[0]);
                    
                    //Console.WriteLine("Asansör No: 1" + "\nAsansör Hedef: " + avmKur.asansorler[0].HedefKat);
                    
                   
                    //Console.WriteLine("Kişi sayısı: " + avmKur.asansorler[0].MevcutSayi);
                    hareket.MusteriBirakmayaGit(avmKur.asansorler[0]);
                    hareket.MusteriBirak(musteriKuyruguListesi, avmKur.asansorler[0]);
                   // Console.WriteLine(musteriKuyruguListesi.KuyrukToplam());
                    // Console.WriteLine("Asansör bilgisi: "+avmKur.asansorler[0].mevcutSayi+" "+musteriKuyruguListesi.kuyrukToplam());
                    Thread.Sleep(500);
                }


            }
            //=========================================================================================================================
            //=========================================================================================================================
            void asansor1Hareket()
            {
                while (avmKur.asansorler[1].AktifMi == true && musteriKuyruguListesi.MusteriKuyrugu.Count > 0)
                {

                    hareket.HedefBelirle(musteriKuyruguListesi, avmKur.asansorler[1]);

                    hareket.MusteriAlmayaGit(avmKur.asansorler[1]);
                    hareket.MusteriAl(musteriKuyruguListesi, avmKur.asansorler[1]);

                    //Console.WriteLine("Asansör No: 1" + "\nAsansör Hedef: " + avmKur.asansorler[0].HedefKat);


                    //Console.WriteLine("Kişi sayısı: " + avmKur.asansorler[0].MevcutSayi);
                    hareket.MusteriBirakmayaGit(avmKur.asansorler[1]);
                    hareket.MusteriBirak(musteriKuyruguListesi, avmKur.asansorler[1]);
                   // Console.WriteLine(musteriKuyruguListesi.KuyrukToplam());
                    // Console.WriteLine("Asansör bilgisi: "+avmKur.asansorler[0].mevcutSayi+" "+musteriKuyruguListesi.kuyrukToplam());
                    Thread.Sleep(500);
                }

            }



            //===========================================================================================================https://ibb.co/gWk2B0z

            Thread asansor1 = new Thread(asansor1Hareket);
            //void kontrol()
            //{


            //    while (true)
            //    {
            //        if (musteriKuyruguListesi.KuyrukToplam() > 20 && asansor1.IsAlive == false)
            //        {
            //            asansor1.Start();
            //            Console.WriteLine("Bir numaralı asansör başlatıldı...");
            //        }
            //        else if (musteriKuyruguListesi.KuyrukToplam() > 20 && asansor1.IsAlive == true)
            //        {
            //            _event.Set();
            //            Console.WriteLine("Bir numaralı asansör aktif.");
            //        }
            //        else if (musteriKuyruguListesi.KuyrukToplam() <= 20 && asansor1.IsAlive == true)
            //        {
            //            _event.Reset();
            //            Console.WriteLine("Bir numaralı asansör pasif.");
            //        }
            //        else if(musteriKuyruguListesi.KuyrukToplam() <= 0)
            //        {
            //            asansor1.Abort();
            //        }
            //        Thread.Sleep(500);
            //    }
            //}


            //=========================================================================================================================
            void asansorDurum()
            {
                while (musteriKuyruguListesi.MusteriKuyrugu.Count>0)
                {
                    
                    foreach (var item in avmKur.asansorler)
                    {
                        if (item.AktifMi==true)
                        {
                            Console.WriteLine("Asansor no: "+item.AsansorNo);
                            Console.WriteLine("Asansor şu an kat: " + item.SuAnKat);
                            Console.WriteLine("Asansor yön: " + item.Yon);
                            Console.WriteLine("Asansor hedef: " + item.HedefKat);
                            Console.WriteLine("Asansor müsteri sayısı: " + item.MevcutSayi);
                        }
                    }
                    Thread.Sleep(20);
                }

            }


            //===========================================================================================================================


            //Thread kontrolT = new Thread(kontrol);

            Thread Login = new Thread(loginYap);
            Thread Exit = new Thread(exitYap);
            Thread asansor0 = new Thread(asansorHareket);
           
            Thread durum = new Thread(asansorDurum);


            //kontrolT.Start();
            Login.Start();
            Exit.Start();
            

            asansor0.Start();
            durum.Start();

            asansor1.Start();






            Console.ReadLine();


            foreach (var item in musteriKuyruguListesi.MusteriKuyrugu)
            {
                Console.WriteLine("musteri sayisi "+item.MusteriSayisi + "hedef kat " + item.HedefKat+"su anki kat"+item.KatNo);
            }
            Console.WriteLine(musteriKuyruguListesi.KuyrukToplam());




        }
    }
}
