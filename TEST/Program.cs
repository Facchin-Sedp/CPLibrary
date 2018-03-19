using System;
using System.Collections;

using CPLibrary;
using NetDialerProviderInterface;
using Phones.Messaging;

namespace TEST
{
    class Program
    {
        public static String CampagnaId = "2";
        public static String NomeCampagna = "BIFIS1701C1";

        static void Main(string[] args)
        {
            ContactProvider cp = new ContactProvider();
            TDictionary dic = new TDictionary();// qui abbiamo gli stati
            // simulo il dialer.exe
            String callMode = String.Empty;// ritorna il tipo di chiamata predictive o power
            Boolean eof = false;// identifica se è finita la scansione della campagna allora restituisco true
            PhonesCallData CcData = new PhonesCallData();// ritornano i dati dell'anagrafica che sto chiamando che pubblicherò nel phonebar
            ArrayList phoneNums = new ArrayList();// lista di numeri da chiamare per noi sempre 1



            Campagna(cp, dic, ref callMode, ref eof, CcData, phoneNums);

            //CampagnaId = "3";
            //NomeCampagna = "XXXXXXXXXX";

            //Campagna(cp, dic, ref callMode, ref eof, CcData, phoneNums);

            //cp.RefreshDB();
        }

        private static void Campagna(ContactProvider cp, TDictionary dic, ref string callMode, ref bool eof, PhonesCallData CcData, ArrayList phoneNums)
        {
            Boolean res = cp.Init(CampagnaId, NomeCampagna, dic);
            if (res)
            {

                Int32 contatti = cp.Contacts();
                Console.WriteLine("CONTATTI: " + contatti);

            }


            int count = 0;
            eof = false;
            while (!eof)// finchè nn indico la fine
            {

                count++;
                res = cp.GetContact(CcData, phoneNums, out callMode, out eof);




                if (res)
                {
                    // chiamata KO
                    if (count == 1)
                        res = cp.SetContact(CcData, "2", "P");
                    else if (count == 2)
                        // chiamata OK
                        res = cp.SetContact(CcData, "3", "P");
                    else
                        res = cp.SetContact(CcData, "2", "P");
                }



            }


        }
    }
      
}
