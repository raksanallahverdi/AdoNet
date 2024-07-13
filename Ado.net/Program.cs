using Ado.net.Constants;
using Ado.net.Services;

namespace Adonet
{
    public static class Program
    {
        public static void Main()
        {
            ShowMenu();
            String choiseInput=Console.ReadLine();
            int choice;
            bool isSucceeded=int.TryParse( choiseInput, out choice );
            if (isSucceeded)
            {
                switch ((Operations)choice)
                {
                    case Operations.AllCountries:
                        CountryService.GetAllCountries();
                        break;
                    case Operations.AddCountry:
                        CountryService.AddCountry();
                        break;
                    case Operations.UpdateCountry:
                        CountryService.AddCountry();
                        break;
                    case Operations.DeleteCountry:
                        CountryService.AddCountry();
                        break;
                    case Operations.DetailsOfCountry:
                        CountryService.GetDetailsOfCountry();
                        break;
                    case Operations.Exit:
                        return;
                    default:
                        Messages.InvalidInputMessage("Choise");
                        break;
                }


            }
            else
            {


                Messages.InvalidInputMessage("Choise");
            }

        }
        public static void ShowMenu()
        {

            Console.WriteLine("menu");
            Console.WriteLine("1.All Countries");
            Console.WriteLine("2.Add Country");
            Console.WriteLine("3.Update Country");
            Console.WriteLine("4.Delete Country");
            Console.WriteLine("5.Get Details of Country");
            Console.WriteLine("0.Exit");
        }

    }

}