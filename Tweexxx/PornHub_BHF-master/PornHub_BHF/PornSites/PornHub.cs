using System.Net;
using Newtonsoft.Json;

namespace PornHub_BHF.PornSites
{
    public class PornHub : Site
    {
        public PornHub(string api, string name) : base(api, name)
        {
            apiPath = @"/webmasters/";
        }

        public override Categories GetCategories()
        {
            string getCategories = string.Format("{0}{1}categories", Api, apiPath);
            string json = null;
            using (WebClient webClient = new WebClient())
            {
                json = webClient.DownloadString(getCategories);
            }

            return (Categories)JsonConvert.DeserializeObject(json, typeof(Categories));
        }

        public override Videos GetVideos(string category)
        {
            string getVideo = string.Format("{0}{1}search?category={2}&thumbsize=medium", Api, apiPath, category);
            string json = null;
            using (WebClient webClient = new WebClient())
            {
                json = webClient.DownloadString(getVideo);
            }

            return (Videos)JsonConvert.DeserializeObject(json, typeof(Videos));            
        }
    }
}
