using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace arkoDT
{
    public class Firebase_Config
    {
        private IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "9LIEv3oM8IkGzdbhvL6949CXXFAD86pu2v2ISD1r",
            BasePath = "https://arko-uno-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };

        private IFirebaseClient client;

        // Public method to get the Firebase client
        public IFirebaseClient GetClient()
        {
            // Initialize the client if it hasn't been created yet
            if (client == null)
            {
                client = new FireSharp.FirebaseClient(config);
            }

            return client;
        }


    }
}

