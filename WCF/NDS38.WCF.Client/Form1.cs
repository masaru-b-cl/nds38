using NDS38.WCF.Client.GreetingServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NDS38.WCF.Client
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			GreetingServiceClient client = null;
			try
			{
				client = new GreetingServiceClient();

				client.Open();

				var name = NameTextBox.Text;
				var greeting = client.Greet(name);

				MessageBox.Show(greeting);

				client.Close();
			}
			catch (EndpointNotFoundException epnfe)
			{
				throw;
			}
			catch (CommunicationException ce)
			{
				if (client != null) client.Abort();
				throw;
			}
		}
	}
}
