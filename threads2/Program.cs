using System;
using System.Threading;
using System.Collections.Generic;



namespace threads2
{


    class Program
    {


        public static void Main()
        {


            KatAsansor avmKur = new KatAsansor();

            avmKur.KatYap();
            avmKur.AsansorYap();

            ManualResetEvent asansor1_event = new ManualResetEvent(true);
            ManualResetEvent asansor2_event = new ManualResetEvent(true);
            ManualResetEvent asansor3_event = new ManualResetEvent(true);
            ManualResetEvent asansor4_event = new ManualResetEvent(true);

            Kuyruk musteriKuyruguListesi = new Kuyruk();

            Musteri musteriOrnek = new Musteri
            {
                HedefKat = 4,
                KatNo = 0,                   //Program ilk çalıştırıldığında listede obje bulunması için initial değer.
                MusteriSayisi = 1
            };
            musteriKuyruguListesi.MusteriKuyrugu.Add(musteriOrnek);
            musteriKuyruguListesi.MusteriKuyrugu.Add(musteriOrnek);

            AsansorHareket hareket = new AsansorHareket();

            //=========================================================================================================================

            void loginYap()
            {


                for (int i = 0; i < 10; i++)
                {
                    Musteri gelen = new Musteri();
                    System.Random rnd = new System.Random();
                    gelen.MusteriSayisi = rnd.Next(1, 11);
                    gelen.HedefKat = rnd.Next(1, 5);
                    gelen.KatNo = 0;
                    musteriKuyruguListesi.KuyrugaEkle(gelen);
                    Thread.Sleep(500);//500 olcak


                }

            }

            //=========================================================================================================================
            void exitYap()
            {


                for (int i = 0; i < 10; i++)
                {
                    Musteri gelen = new Musteri();
                    System.Random rnd = new System.Random();
                    gelen.MusteriSayisi = rnd.Next(1, 6);
                    gelen.HedefKat = 0;
                    gelen.KatNo = rnd.Next(1, 5);
                    musteriKuyruguListesi.KuyrugaEkle(gelen);
                    Thread.Sleep(1000);

                }

            }

            //=========================================================================================================================
          

            void asansorHareket()
            {
                hareket.YeniAsansorHareket(avmKur.asansorler[0], musteriKuyruguListesi, hareket);
            }
            void asansor1Hareket()
            {
                while(true)
                {
                    hareket.YeniAsansorHareket(avmKur.asansorler[1], musteriKuyruguListesi, hareket);
                    asansor1_event.WaitOne();

                }

            }
            void asansor2Hareket()
            {
                while (true)
                {
                    hareket.YeniAsansorHareket(avmKur.asansorler[2], musteriKuyruguListesi, hareket);
                    asansor2_event.WaitOne();
                }
                

            }
            void asansor3Hareket()
            {
                while (true)
                {
                    hareket.YeniAsansorHareket(avmKur.asansorler[3], musteriKuyruguListesi, hareket);
                    asansor3_event.WaitOne();
                }
                

            }
            void asansor4Hareket()
            {
                while(true)
                {
                    hareket.YeniAsansorHareket(avmKur.asansorler[4], musteriKuyruguListesi, hareket);
                    asansor4_event.WaitOne();
                }
                

            }

            //=========================================================================================================================
      
            Thread asansor0 = new Thread(asansorHareket);
            Thread asansor1 = new Thread(asansor1Hareket);
            Thread asansor2 = new Thread(asansor2Hareket);
            Thread asansor3 = new Thread(asansor3Hareket);
            Thread asansor4 = new Thread(asansor4Hareket);
            
            //===========================================================================================================


            void kontrol()
            {
                int calisanAsansorSayisi = 0;
                int aktifAsansorSayisi = 0;
                

                while (musteriKuyruguListesi.MusteriKuyrugu.Count > 0)
                {
                    Thread.Sleep(500);
                    
                    if (musteriKuyruguListesi.KuyrukToplam() > 20 && (asansor1.IsAlive == false || asansor2.IsAlive == false || asansor3.IsAlive == false || asansor4.IsAlive == false))
                    {

                        calisanAsansorSayisi++;
                        aktifAsansorSayisi++;
                        
                        switch (calisanAsansorSayisi)
                        {
                            case 1:
                                asansor1.Start();
                                avmKur.asansorler[1].AktifMi = true;
                                Console.WriteLine("Bir numaralı asansör başlatıldı...");
                                break;
                            case 2:
                                asansor2.Start();
                                avmKur.asansorler[2].AktifMi = true;
                                Console.WriteLine("İki numaralı asansör başlatıldı...");
                                break;
                            case 3:
                                asansor3.Start();
                                avmKur.asansorler[3].AktifMi = true;
                                Console.WriteLine("Üç numaralı asansör başlatıldı...");
                                break;
                            case 4:
                                asansor4.Start();
                                avmKur.asansorler[4].AktifMi = true;
                                Console.WriteLine("Dört numaralı asansör başlatıldı...");
                                break;



                        }


                    }

                    else if (musteriKuyruguListesi.KuyrukToplam() <= 20 && (asansor1.IsAlive == true || asansor2.IsAlive == true || asansor3.IsAlive == true || asansor4.IsAlive == true))
                    {

                        switch (aktifAsansorSayisi)
                        {
                            case 1:
                                asansor1_event.Reset();
                                aktifAsansorSayisi--;
                                Console.WriteLine("1 nolu asansör pasif");
                                break;
                            case 2:
                                asansor2_event.Reset();
                                aktifAsansorSayisi--;
                                Console.WriteLine("2 nolu asansör pasif");
                                break;
                            case 3:
                                asansor3_event.Reset();
                                aktifAsansorSayisi--;
                                Console.WriteLine("3 nolu asansör pasif");
                                break;
                            case 4:
                                asansor4_event.Reset();
                                aktifAsansorSayisi--;
                                Console.WriteLine("4 nolu asansör pasif");
                                break;


                        }                        

                    }





                    else if (musteriKuyruguListesi.KuyrukToplam() > 20 && (asansor1.IsAlive == true || asansor2.IsAlive == true || asansor3.IsAlive == true || asansor4.IsAlive == true))
                    {



                        switch (aktifAsansorSayisi)
                        {
                            case 0:
                                Console.WriteLine("Bir numaralı asansör aktif.");
                                asansor1_event.Set();
                                
                                aktifAsansorSayisi++;
                                break;
                            case 1:
                                asansor2_event.Set();
                                Console.WriteLine("İki numaralı asansör aktif.");
                                aktifAsansorSayisi++;
                                break;
                            case 2:
                                asansor3_event.Set();
                                Console.WriteLine("Üç numaralı asansör aktif.");
                                aktifAsansorSayisi++;
                                break;
                            case 3:
                                asansor4_event.Set();
                                Console.WriteLine("Dört numaralı asansör aktif.");
                                aktifAsansorSayisi++;
                                break;



                        }

                    }

                   
                  else   if(musteriKuyruguListesi.KuyrukToplam() <= 0)
                    {
                        asansor1.Abort();
                        asansor2.Abort();
                        asansor3.Abort();
                        asansor4.Abort();
                       
                    }

                    
                }
                
            }


            //=========================================================================================================================
            void asansorDurum()
            {
                while (musteriKuyruguListesi.MusteriKuyrugu.Count>0)
                {
                    
                    foreach (var item in avmKur.asansorler)
                    {

                        Console.WriteLine("----------------------------------------");
                        Console.WriteLine("Asansor no: " + item.AsansorNo);
                        Console.WriteLine("Asansor şu an kat: " + item.SuAnKat);
                        Console.WriteLine("Asansor yön: " + item.Yon);
                        Console.WriteLine("Asansor müsteri sayısı: " + item.MevcutSayi);
                        Console.WriteLine("----------------------------------------");

                    }
                    Thread.Sleep(200);
                }

            }


            //===========================================================================================================================


            Thread kontrolT = new Thread(kontrol);
            Thread Login = new Thread(loginYap);
            Thread Exit = new Thread(exitYap);           
            Thread durum = new Thread(asansorDurum);
            kontrolT.Start();
            Login.Start();
            Exit.Start();            
            asansor0.Start();
            durum.Start();

            Console.ReadLine();


            foreach (var item in musteriKuyruguListesi.MusteriKuyrugu)
            {
                Console.WriteLine("musteri sayisi "+item.MusteriSayisi + "hedef kat " + item.HedefKat+"su anki kat"+item.KatNo);
            }
            Console.WriteLine(musteriKuyruguListesi.KuyrukToplam());

            foreach (var item in avmKur.asansorler)
            {
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("Asansor no: " + item.AsansorNo);
                Console.WriteLine("Asansor şu an kat: " + item.SuAnKat);
                Console.WriteLine("Asansor yön: " + item.Yon);
                Console.WriteLine("Asansor hedef: " + item.HedefKat);
                Console.WriteLine("Asansor müsteri sayısı: " + item.MevcutSayi);
                Console.WriteLine("----------------------------------------");                    
            }

            Console.ReadLine();
        }
    }
}
