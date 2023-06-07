﻿using System;
using System.Net.Http;
using System.Threading.Tasks;

public class Class1
{
	public Class1()
	{
	}
}
//API aanroepen
//inladen coordinaten van de API,
//Zorgen dat de data overeenkomt met dataset
//Model inladen
//Prediction doen op basis van coordinaten
//Prediction doorsdturen naar API


class Program
{
    static async Task Main()
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                // De URL van de API waar je verbinding mee wilt maken
                string apiUrl = "https://api.example.com/endpoint";

                // Stuur een GET-verzoek naar de API
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                // Controleer of het verzoek succesvol was
                if (response.IsSuccessStatusCode)
                {
                    // Lees de inhoud van het antwoord
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Doe iets met de inhoud van het antwoord
                    Console.WriteLine(responseBody);
                }
                else
                {
                    Console.WriteLine("Het verzoek was niet succesvol. Statuscode: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Er is een fout opgetreden tijdens het maken van het verzoek: " + ex.Message);
            }
        }
    }
}
