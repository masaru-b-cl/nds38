using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LightNode.Server;

namespace NDS38.LightNode.Server
{
	public class Greeting: LightNodeContract
	{
		public string Greet(string name)
		{
			return String.Format("こんにちは、{0}さん", name);
		}
	}
}