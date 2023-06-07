using System;
using System.Net.Http;
using System.Threading.Tasks;

//API aanroepen

class Program
{
    static async Task Main()
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                // De URL van de API waar je verbinding mee wilt maken
                string apiUrl = "https://localhost:7004";

                // Stuur een GET-verzoek naar de API
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                // Controleer of het verzoek succesvol was
                if (response.IsSuccessStatusCode)
                {
                    // Lees de inhoud van het antwoord
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Doe iets met de inhoud van het antwoord
                    System.Diagnostics.Debug.WriteLine(responseBody);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Het verzoek was niet succesvol. Statuscode: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Er is een fout opgetreden tijdens het maken van het verzoek: " + ex.Message);
            }
        }
    }

    //inladen coordinaten van de API, -> ontvangen coordinaten in een 3vector list zetten
    //Zorgen dat de data overeenkomt met dataset
    //Model inladen
    //Prediction doen op basis van coordinaten
    //Prediction doorsdturen naar API

}
