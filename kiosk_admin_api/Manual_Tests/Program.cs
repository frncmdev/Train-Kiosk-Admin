using System.Reflection.PortableExecutable;
using System.Net.Security;
using System;
using core.services;
using core.models.RequestResults;
class Program
{
    static async Task Main(string[] args)
    {
        AuthService _service = new AuthService();
        await _service.addUser();
        if(await _service.Login(new LoginRequest("Kiosk_Admin", "TrainKioskPassword")))
        {
            System.Console.WriteLine("it works");
        } else 
        {
            System.Console.WriteLine("is no work");

        }
    }
}