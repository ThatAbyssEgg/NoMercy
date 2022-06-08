using System;
using NoMercy.Web.Requests;
using NoMercy.Web.Responses;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMercyClient
{
    public class Authorization
    {
        public static void RequestAuthorisation()
        {
            string charName, charClass;
            int selectionMainMenu, selectionClass;

            Console.WriteLine("Welcome. Select an option, creature:");
            Console.WriteLine("1: Register a new character");
            Console.WriteLine("2: Choose your character");
            Console.WriteLine("3: Leave the place, like a coward");

            selectionMainMenu = Console.ReadKey(true).KeyChar - 48;

            switch (selectionMainMenu)
            {
                case 1:
                    Console.WriteLine("Choose the name of your creature.");
                    charName = Console.ReadLine();

                    Console.WriteLine("Another soul decided to challenge the dunes. But who are you, exactly?");
                    Console.WriteLine("1: Bloodletter. Your attacks will be stronger!");
                    Console.WriteLine("2: Priest. You can heal!");
                    Console.WriteLine("3: Protector. Your HP is increased, and your defence can be empowered!");
                    selectionClass = Console.ReadKey(true).KeyChar - 48;

                    switch (selectionClass)
                    {
                        case 1:
                            Console.WriteLine($"A bloodletter you are, {charName}? Well, let's see where shall the path of bloodlust bring you...");
                            charClass = "bloodletter";
                            break;
                        case 2:
                            Console.WriteLine($"A priest? Let's see if The Only will save you in the dunes. Show me your true power, {charName}!");
                            charClass = "priest";
                            break;
                        case 3:
                            Console.WriteLine($"A protector? The thick armor rarely saves from the others' fury. Nevertheless, good luck, {charName}.");
                            charClass = "protector";
                            break;
                        default:
                            Console.WriteLine("Don't wanna choose a valid class? Want to put this work on the others? Fine then, your class is bloodletter from now on. There's no way to cancel this.");
                            charClass = "bloodletter";
                            break;
                        
                    }             

                    RegistrationRequest registrationRequest = new RegistrationRequest()
                    {
                        Name = charName,
                        Class = charClass
                    };

                    SocketClient socketClient = new SocketClient();
                    RegistrationResponse response = socketClient.MakeRequest<RegistrationRequest, RegistrationResponse>(registrationRequest);
                    Console.WriteLine($"Your special token is {response.Token}. Be sure to keep it, or you will lose your creature FOREVER.");

                    Console.ReadKey();
                    break;
                     
                case 2:
                    string charToken1, charToken2;
                    string jsonData1, jsonData2;
                    try
                    {
                        Console.WriteLine("Please, input the first character's token.");
                        charToken1 = Console.ReadLine();
                        AuthorizationRequest authorizationRequest1 = new AuthorizationRequest()
                        {
                            Token = charToken1
                        };
                        SocketClient socketClient1 = new SocketClient();
                        AuthorizationResponse response1 = socketClient1.MakeRequest<AuthorizationRequest, AuthorizationResponse>(authorizationRequest1);


                        Console.WriteLine("Please, input the second character's token.");

                        charToken2 = Console.ReadLine();
                        AuthorizationRequest authorizationRequest2 = new AuthorizationRequest()
                        {
                            Token = charToken2
                        };

                        SocketClient socketClient2 = new SocketClient();
                        AuthorizationResponse response2 = socketClient2.MakeRequest<AuthorizationRequest, AuthorizationResponse>(authorizationRequest2);

                        BattleMode battle = new BattleMode(response1, response2);


                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                    break;

                default:
                    Environment.Exit(1);
                    break;
            }
        }
    }
}
