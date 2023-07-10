using Newtonsoft.Json;
using System.Collections.Specialized;

namespace WhoDeDoVille.ReactionTester.AFApi.Helpers;

// TODO: Needs tests
public static class RequestParameterProvider
{
    /// <summary>
    ///     Combines sent data into a name value collection.
    ///     Includes url query and request body.
    /// </summary>
    /// <param name="req"></param>
    public static async Task<NameValueCollection> ReturnReqParameters(HttpRequestData req)
    {
        var returnData = System.Web.HttpUtility.ParseQueryString(req.Url.Query);

        string requestBody = String.Empty;
        using (var streamReader = new StreamReader(req.Body))
        {
            requestBody = await streamReader.ReadToEndAsync();
        }
        if (requestBody != "")
        {
            var jsonData = JsonConvert.DeserializeObject<Dictionary<string, string>>(requestBody);

            foreach (string key in jsonData.Keys)
            {
                returnData[key] = jsonData[key];
            }
        }

        return returnData;
    }
}
