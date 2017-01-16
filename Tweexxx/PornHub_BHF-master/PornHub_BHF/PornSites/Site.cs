namespace PornHub_BHF.PornSites
{
    // In future easy to add new website
    public abstract class Site
    {
        private readonly string api;
        private readonly string name;
        protected string apiPath;

        public string Api { get { return api; } }
        public string Name { get { return name; } }

        public Site(string api, string name)
        {
            this.api = api;
            this.name = name;
        }

        public abstract Categories GetCategories();
        public abstract Videos GetVideos(string category);
    }
}
