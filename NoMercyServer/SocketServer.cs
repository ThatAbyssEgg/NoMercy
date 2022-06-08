using System;
using NoMercy.Web.Requests;
using NoMercy.Web.Responses;
using System.IO;
using System.Text.Json;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMercyServer
{
    public class SocketServer
    {
        public static readonly Dictionary<string, Type> RequestTypes = new Dictionary<string, Type>()
        {
            { "RegistrationRequest", typeof(RegistrationRequest) },
            { "AuthorizationRequest", typeof(AuthorizationRequest) }
        };
        // Incoming client data
        public static string data = null;
        public static void StartListening()
        {
            byte[] bytes = new byte[1024];

            // Getting IP address and establishing the endpoint
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            // Create a socket
            Socket listener = new Socket(ipAddress.AddressFamily,
            SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen();
                Console.WriteLine("WAITING FOR CONNECTION . . .");
                while (true)
                {
                    Socket handler = listener.Accept();
                    data = null;

                    while (true)
                    {
                        int bytesRec = handler.Receive(bytes);
                        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        Console.WriteLine($"Current index: {data.IndexOf("<EOF>")}");
                        if (data.IndexOf("<EOF>") > -1)
                        {
                            break;
                        }

                    }

                    int i = 0;
                    string jsonStr = "";
                    // THIS IS BAD but I have no time
                    while (data[i] != '<')
                    {
                        jsonStr += data[i];
                        i++;
                    }
                    Console.WriteLine(jsonStr.Length);
                    // Show the data on the console.  
                    Console.WriteLine("Text received : {0}", jsonStr);

                    string requestName = JsonSerializer.Deserialize<Request>(jsonStr).RequestName;
                    if (!RequestTypes.TryGetValue(requestName, out var requestType))
                    {
                        Console.WriteLine("UnnamedException");
                    }

                    if (requestName == "RegistrationRequest")
                    {
                        RegistrationRequest registrationRequest = (RegistrationRequest)JsonSerializer.Deserialize(jsonStr, requestType);
                        string token = Guid.NewGuid().ToString();
                        var character = new PlayerCharacter(registrationRequest.Name, token, registrationRequest.Class);
                        string jsonCharacterString = JsonSerializer.Serialize(character);
                        JsonFileManagement.Save(jsonCharacterString, token);
                        RegistrationResponse registrationResponse = new RegistrationResponse()
                        {
                            Token = token
                        };
                        SendMessage(handler, registrationResponse);
                    }
                    else if (requestName == "AuthorizationRequest")
                    {
                        AuthorizationRequest authorizationRequest = (AuthorizationRequest)JsonSerializer.Deserialize(jsonStr, requestType);
                        try
                        {
                            PlayerCharacter character = JsonFileManagement.Load(authorizationRequest.Token);
                            AuthorizationResponse authorizationResponse = MapToResponse(character);
                            SendMessage(handler, authorizationResponse);
                        }
                        catch (FileNotFoundException)
                        {
                            Console.WriteLine("FileNotFoundException");
                        }
                    }
                    
                    // PlayerCharacter playerData = JsonSerializer.Deserialize<PlayerCharacter>(jsonStr)!;
                    
                    


                    // Echo the data back to the client.  
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();

                        //All the stuff here was copypasted. Need to edit it all

                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static AuthorizationResponse MapToResponse(PlayerCharacter character)
        {
            return new AuthorizationResponse()
            {
                Name = character.Name,
                Token = character.Token,
                Class = character.Class
            };
        }

        private static void SendMessage<TResponse>(Socket handler, TResponse response)
        {
            string json = JsonSerializer.Serialize(response);
            byte[] msg = Encoding.ASCII.GetBytes(json);
            handler.Send(msg);
        }
    }
}
