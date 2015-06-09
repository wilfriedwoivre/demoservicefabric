using Owin;

namespace DemoBlog.ServiceFabric.Gateway
{
    public interface IOwinAppBuilder
    {
        void Configuration(IAppBuilder app);
    }
}
