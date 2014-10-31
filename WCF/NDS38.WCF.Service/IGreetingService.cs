using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace NDS38.WCF.Service
{
	[ServiceContract]
	public interface IGreetingService
	{
		[OperationContract]
		string Greet(string name);
	}
}
