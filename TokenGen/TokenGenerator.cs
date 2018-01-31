using System;
using System.Collections.Generic;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;

namespace TokenGen
{
    public class TokenGenerator
    {
        public static string AccessGenerator(string apikey, string secret, int expdateY, int expdateM, int expdateD)
        {
            var dateTime = new DateTime(expdateY, expdateM, expdateD, 0, 0, 0, DateTimeKind.Local);
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var unixDateTime = Convert.ToInt32((dateTime.ToUniversalTime() - epoch).TotalSeconds);

            Console.WriteLine("The epoch time you entered is: " + unixDateTime);

            var payload = new Dictionary<string, object>
            {
                { "iss", apikey},
                { "exp", unixDateTime}
            };

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var token = encoder.Encode(payload, secret);
            Console.WriteLine(token);
            Console.ReadLine();
            return token;
        }
    }
}
