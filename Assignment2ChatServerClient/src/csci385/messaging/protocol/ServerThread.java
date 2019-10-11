package csci385.messaging.protocol;

import java.io.ByteArrayInputStream;
import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.net.InetAddress;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.HashMap;

import csci385.messaging.server.DeregistrationResponce;
import csci385.messaging.server.RegistrationResponce;
import csci385.messaging.server.RequestUsersResponce;
import csci385.messaging.*;


public class ServerThread extends Thread{
	private HashMap<String, InetAddress> Clients = new HashMap<>();
	private DataInputStream DIS;
	private DataOutputStream DOS;
	boolean keepGoing;
	private Socket sock;
	
	ServerThread(Socket sock, HashMap<String, InetAddress> h) throws IOException{
		this.sock = sock;
		keepGoing = true;
		DIS = new DataInputStream(this.sock.getInputStream());
		DOS = new DataOutputStream(this.sock.getOutputStream());
		Clients = h;
	}
	
	public void run() {
		 
		while(keepGoing) {
			try {
	             int msgLength = DIS.readInt();
	             byte[] fullMsg = new byte[msgLength];
	             DIS.readFully(fullMsg);
	             MessageSwitchBoard(fullMsg);
	         } catch (IOException ioe) {
	             System.out.println("ERROR in run()");
	             ioe.printStackTrace();
	             keepGoing = false;
	         }
		 }
		
	}
	
	
	
	public void MessageSwitchBoard(byte[] b) throws IOException {
		ByteArrayInputStream BIS = new ByteArrayInputStream(b);
		DataInputStream dis = new DataInputStream(BIS);
		int MessType = dis.readInt();
		System.out.println(MessType);
		

		switch(MessType) {
			case 3:
				int len = dis.readInt();
				byte[] RestOfMessage = new byte[len];
				dis.readFully(RestOfMessage);
				String themessage = new String(RestOfMessage);
				Clients.put(themessage, sock.getInetAddress());
				sendRegResponse();
				System.out.println(themessage + " Added");
				break;
				
			case 5:
				System.out.println("case 5");
				int len5 = dis.readInt();
				byte[] RestOfMessage1 = new byte[len5];
				dis.readFully(RestOfMessage1);
				String usertoRemove = new String(RestOfMessage1);
				Clients.remove(usertoRemove);
				sendDeRegistrationMessage(usertoRemove);
				break;
			case 6:
				System.out.println("case 6");
				sendRegUsers();
				break;
			case 8:
				System.out.println("case 8");
				break;
			case 9:
				System.out.println("case 9");
				break;
			default:
				break;
		}
		
	}
	
	public void sendRegResponse() throws IOException {
		RegistrationResponce regres = new RegistrationResponce();
		DOS = new DataOutputStream(sock.getOutputStream());
		regres.RegistrationSuccess();
		DOS.writeInt(regres.toByteArray().length);
		DOS.write(regres.toByteArray());
		DOS.flush();
	}	
		
	public void sendRegUsers() throws IOException {
		RequestUsersResponce RUR = new RequestUsersResponce();
		DOS = new DataOutputStream(sock.getOutputStream());
		RUR.getHashMap(Clients);
		DOS.writeInt(RUR.toByteArray().length);
		DOS.write(RUR.toByteArray());
		DOS.flush();
	}
	
	public void sendDeRegistrationMessage(String remove) throws IOException{
		DeregistrationResponce DEReg = new DeregistrationResponce();
		DOS = new DataOutputStream(sock.getOutputStream());
		DEReg.getUsername(remove + " has been removed");
		DOS.writeInt(DEReg.toByteArray().length);
		DOS.write(DEReg.toByteArray());
		DOS.flush();
	}
	
	
	
}
