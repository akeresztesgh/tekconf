namespace TekConf.UI.Api.UrlResolvers.v1
{
  public class SessionsSpeakersUrlResolver : BaseUrlResolver
  {
    private readonly string _conferenceSlug;

    public SessionsSpeakersUrlResolver(string conferenceSlug)
    {
      _conferenceSlug = conferenceSlug;
    }

    public string ResolveUrl(string sessionSlug)
    {
      return CombineUrl("/v1/conferences/" + _conferenceSlug + "/sessions/" + sessionSlug + "/speakers");
    }
  }
}