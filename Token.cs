using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace Sample
{
    [TestClass]
    public class Token
    {
        [TestMethod]
        public void GetToken()
        {
            var client = new RestClient("https://ca-ds-authentication-int.pwc.com/login/token");
            var request = new RestRequest();
            request.Method = Method.GET;
            request.AddParameter("Email", "test@pwc.com");
            request.AddParameter("Guid", "utestidi001");
            request.AddParameter("password", "Welcomepwc1");
            IRestResponse response = client.Execute(request);
            var content = response.Content;
        }

        //https://ca-ds-authentication-{}.pwc.com/login/token
    }
}
