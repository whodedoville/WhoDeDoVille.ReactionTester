using Newtonsoft.Json;

namespace WhoDeDoVille.ReactionTester.Logging;
public static class LoggingStaticTools
{
    public static string ObjectToString(object request)
    {
        return JsonConvert.SerializeObject(request);
    }
}
