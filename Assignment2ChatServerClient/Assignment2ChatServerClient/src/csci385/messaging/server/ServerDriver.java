package csci385.messaging.server;

import java.io.ByteArrayInputStream;
import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.net.InetAddress;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.HashMap;

public class ServerDriver {
	
	private HashMap<String, Socket> ClientList = new HashMap<>();
	private DataInputStream DIS;
	private DataOutputStream DOS;
	private ServerSocket listener;
	private Socket sock;
	private InetAddress hostname;
	private int PortNumber;
	
	public static void main(String[] args) throws IOException
	{
		ServerDriver ServerDriver = new ServerDriver();
		ServerDriver.CreateStartupObjects();
		
	}

	public void CreateStartupObjects() throws IOException 
	{
		listener = new ServerSocket(0);
		PortNumber = listener.getLocalPort();
		hostname = InetAddress.getLocalHost();
		System.out.println("Host name: " + hostname);
		System.out.println("Port number: " + PortNumber);
		
		while(true) {
			sock = listener.accept();
			System.out.println("New client: " + sock);
			Thread t = new ServerThread(sock, ClientList);
			t.start();
		}
	}
	
	
}
