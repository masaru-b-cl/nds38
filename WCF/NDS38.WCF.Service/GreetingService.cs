using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace NDS38.WCF.Service
{
	public class GreetingService : IGreetingService
	{
		public string Greet(string name)
		{
			return String.Format("こんにちは、{0}さん", name);
		}
	}
}
