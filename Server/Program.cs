using SuperSimpleTcp;
using System.Text;

SimpleTcpServer server = new SimpleTcpServer("*", 8000);

server.Events.ClientConnected += Connect;
server.Events.ClientDisconnected += Disconnect;
server.Events.DataReceived += DataReceived;
server.Start();

Console.ReadKey();

void DataReceived(object? sender, DataReceivedEventArgs e)
{
	Console.WriteLine(Encoding.UTF8.GetString(e.Data));
}

void Disconnect(object? sender, ConnectionEventArgs e)
{
	Console.WriteLine(e.IpPort + " Disconnected");
}


void Connect(object? sender, ConnectionEventArgs e)
{
	foreach (var client in server.GetClients())
	{
		if (client != e.IpPort)
		{
			server.Send(client, "connected");
		}
	}
}