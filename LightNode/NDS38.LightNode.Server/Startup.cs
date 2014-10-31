using System;
using System.Threading.Tasks;

using Owin;
using Microsoft.Owin;

using LightNode.Server;
using LightNode.Formatter;

[assembly: OwinStartup(typeof(NDS38.LightNode.Server.Startup))]

namespace NDS38.LightNode.Server
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			app.UseLightNode(new LightNodeOptions(
				AcceptVerbs.Get | AcceptVerbs.Post,
				new JsonNetContentFormatter()));
		}
	}
}
