namespace WebApiGoodPracticesSample.Web.Helpers
{
    public static class UrlBuilderHelper
    {
        public static string UrlResourceCreated(string controller, object id)
        {
            return $"/api/v1/{controller}/{id}";
        }
    }
}
