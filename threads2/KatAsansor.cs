using System;
using System.Collections.Generic;
using System.Text;

namespace threads2
{
    class KatAsansor
    {
        public List<Kat> katlar = new List<Kat>();

        public void KatYap()
        {
            for (int i = 0; i <= 4; i++)
            {
                Kat yeniKat = new Kat
                {                                                     
                    KatNo = i
                };
                katlar.Add(yeniKat);
            }
        }

        public List<Asansor> asansorler = new List<Asansor>();

        public void AsansorYap()
        {
            for (int i = 0; i <= 4; i++)
            {
                Asansor yeniAsansor = new Asansor
                {
                    AktifMi = false,
                    MevcutSayi = 0,
                    AsansorNo = i
                };
                if (i == 0)
                {
                    yeniAsansor.AktifMi = true;
                }
                asansorler.Add(yeniAsansor);
            }
        }





    }
}
