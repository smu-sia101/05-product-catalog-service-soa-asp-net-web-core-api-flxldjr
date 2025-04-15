using Firebase.Database;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

namespace ProductC.Services
{
    public class FirebaseService
    {
        public FirebaseClient Client { get; }

        public FirebaseService(IConfiguration configuration)
        {
            var credential = GoogleCredential.FromFile("serviceAccountKey.json");
            FirebaseApp.Create(new AppOptions()
            {
                Credential = credential
            });

            Client = new FirebaseClient(
                configuration["Firebase:DatabaseUrl"],
                new FirebaseOptions
                {
                    AuthTokenAsyncFactory = async () =>
                    {
                        var token = await FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance
                            .CreateCustomTokenAsync("some-uid");
                        return token;
                    }
                });
        }
    }
}