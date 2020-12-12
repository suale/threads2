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
            
            //avmKur.asansorler[1].AktifMi = true;//silincek

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

            void loginYap()//Zemin kattan avm'ye giriş yapılmasını sağlar.
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
            void exitYap()//Zemin olmayan katlarda exit'e gidecek yolcuları kuyruğa ekler.
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
                hareket.YeniAsansorHareket(avmKur.asansorler[0], musteriKuyruguListesi, hareket);
            }
            void asansor1Hareket()
            {
                hareket.YeniAsansorHareket(avmKur.asansorler[1], musteriKuyruguListesi, hareket);

            }

            void asansor2Hareket()
            {
                hareket.YeniAsansorHareket(avmKur.asansorler[2], musteriKuyruguListesi, hareket);

            }
            void asansor3Hareket()
            {
                hareket.YeniAsansorHareket(avmKur.asansorler[3], musteriKuyruguListesi, hareket);

            }
            void asansor4Hareket()
            {
                hareket.YeniAsansorHareket(avmKur.asansorler[4], musteriKuyruguListesi, hareket);

            }

            //=========================================================================================================================
            //=========================================================================================================================
            Thread asansor0 = new Thread(asansorHareket);
            Thread asansor1 = new Thread(asansor1Hareket);
            Thread asansor2 = new Thread(asansor2Hareket);
            Thread asansor3 = new Thread(asansor3Hareket);
            Thread asansor4 = new Thread(asansor4Hareket);
            

            //===========================================================================================================


            void kontrol()
            {
                int aktifAsansorSayisi = 0;
                

                while (musteriKuyruguListesi.MusteriKuyrugu.Count > 0)
                {
                    Thread.Sleep(500);
                    Console.WriteLine(musteriKuyruguListesi.KuyrukToplam());
                    Console.WriteLine(aktifAsansorSayisi+"----------------");

                    if (musteriKuyruguListesi.KuyrukToplam() > 20 && (asansor1.IsAlive == false || asansor2.IsAlive == false || asansor3.IsAlive == false || asansor4.IsAlive == false))
                    {

                        aktifAsansorSayisi++;
                        if (aktifAsansorSayisi > 4)
                            aktifAsansorSayisi = 4;
                        switch (aktifAsansorSayisi)
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
                    else if (musteriKuyruguListesi.KuyrukToplam() > 20 && (asansor1.IsAlive == true || asansor2.IsAlive == true || asansor3.IsAlive == true || asansor4.IsAlive == true))
                    {



                        switch (aktifAsansorSayisi)
                        {
                            case 1:
                                _event.Set();
                                Console.WriteLine("Bir numaralı asansör aktif.");
                                aktifAsansorSayisi = 1;
                                break;
                            case 2:
                                _event.Set();
                                Console.WriteLine("İki numaralı asansör aktif.");
                                aktifAsansorSayisi = 2;
                                break;
                            case 3:
                                _event.Set();
                                Console.WriteLine("Üç numaralı asansör aktif.");
                                aktifAsansorSayisi = 3;
                                break;
                            case 4:
                                _event.Set();
                                Console.WriteLine("Dört numaralı asansör aktif.");
                                aktifAsansorSayisi = 4;
                                break;



                        }

                    }

                    else if (musteriKuyruguListesi.KuyrukToplam() <= 20 && (asansor1.IsAlive == true || asansor2.IsAlive == true || asansor3.IsAlive == true || asansor4.IsAlive == true))
                    {
                        switch (aktifAsansorSayisi)
                        {
                            case 1:
                                _event.Reset();
                                Console.WriteLine("Bir numaralı asansör pasif.");
                                aktifAsansorSayisi = 0;
                                break;
                            case 2:
                                _event.Reset();
                                Console.WriteLine("İki numaralı asansör pasif.");
                                aktifAsansorSayisi = 1;
                                break;
                            case 3:
                                _event.Reset();
                                Console.WriteLine("Üç numaralı asansör pasif.");
                                aktifAsansorSayisi = 2;
                                break;
                            case 4:
                                _event.Reset();
                                Console.WriteLine("Dört numaralı asansör pasif.");
                                aktifAsansorSayisi = 3;
                                break;



                        }

                    }
                    else if(musteriKuyruguListesi.KuyrukToplam() <= 0)
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
                        
                            Console.WriteLine("Asansor no: "+item.AsansorNo);
                            Console.WriteLine("Asansor şu an kat: " + item.SuAnKat);
                            Console.WriteLine("Asansor yön: " + item.Yon);
                            Console.WriteLine("Asansor hedef: " + item.HedefKat);
                            Console.WriteLine("Asansor müsteri sayısı: " + item.MevcutSayi);
                        
                    }
                    Thread.Sleep(400);
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
           // durum.Start();

            //asansor1.Start();






            Console.ReadLine();


            foreach (var item in musteriKuyruguListesi.MusteriKuyrugu)
            {
                Console.WriteLine("musteri sayisi "+item.MusteriSayisi + "hedef kat " + item.HedefKat+"su anki kat"+item.KatNo);
            }
            Console.WriteLine(musteriKuyruguListesi.KuyrukToplam());
            Console.ReadLine();



        }
    }
}
//if (musteriKuyruguListesi.KuyrukToplam() > 20 && asansor1.IsAlive == false)
//{
//    asansor1.Start();
//    avmKur.asansorler[1].AktifMi = true;
//    Console.WriteLine("Bir numaralı asansör başlatıldı...");
//}
//else if (musteriKuyruguListesi.KuyrukToplam() > 20 && asansor1.IsAlive == true)
//{
//    _event.Set();
//    Console.WriteLine("Bir numaralı asansör aktif.");
//}
//else if (musteriKuyruguListesi.KuyrukToplam() <= 20 && asansor1.IsAlive == true)
//{
//    _event.Reset();
//    Console.WriteLine("Bir numaralı asansör pasif.");
//    avmKur.asansorler[1].AktifMi = false;
//}
//else if (musteriKuyruguListesi.KuyrukToplam() <= 0)
//{
//    asansor1.Abort();
//}
//Thread.Sleep(500);
