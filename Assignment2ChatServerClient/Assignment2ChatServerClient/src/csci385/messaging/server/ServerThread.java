package csci385.messaging.server;

import java.io.ByteArrayInputStream;
import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.net.Socket;
import java.util.HashMap;
import java.util.Map;

import csci385.messaging.protocol.BroadcastMessage;
import csci385.messaging.protocol.DeregistrationResponse;
import csci385.messaging.protocol.RegistrationResponse;
import csci385.messaging.protocol.RequestUsersResponse;
import csci385.messaging.protocol.WhisperMessage;



public class ServerThread extends Thread{
	private HashMap<String, Socket> Clients = new HashMap<>();
	private DataInputStream DIS;
	private DataOutputStream DOS;
	boolean keepGoing;
	private Socket sock;
	public static final int RegistrationMessage = 3;
	public static final int DeregistrationMessage = 5;
	public static final int RequestRegisteredUsers = 6;
	public static final int BroadcastMessage = 8;
	public static final int WhisperMessage = 9;
	ServerThread(Socket sock, HashMap<String, Socket> h) throws IOException{
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
		
		switch(MessType) {
			case RegistrationMessage:
				int len = dis.readInt();
				byte[] RestOfMessage = new byte[len];
				dis.readFully(RestOfMessage);
				String themessage = new String(RestOfMessage);
				for(Map.Entry<String, Socket> mapElement : Clients.entrySet() ) {
					if(mapElement.getKey().contentEquals(themessage)) {
						System.out.println("Username Duplicate");
						sendRegResponseDeny();
					}
				}
				Clients.put(themessage, sock);
				sendRegResponse();
				System.out.println(themessage + " Added");
				break;
				
			case DeregistrationMessage:
				System.out.println("case 5");
				int len5 = dis.readInt();
				byte[] RestOfMessage1 = new byte[len5];
				dis.readFully(RestOfMessage1);
				String usertoRemove = new String(RestOfMessage1);
				Clients.remove(usertoRemove);
				sendDeRegistrationMessage(usertoRemove);
				break;
			case RequestRegisteredUsers:
				System.out.println("case 6");
				sendRegUsers();
				break;
			case BroadcastMessage:
				System.out.println("case 8");
				int len3 = dis.readInt();
				byte[] broadcastMessage = new byte[len3];
				dis.readFully(broadcastMessage);
				String bMessage = new String(broadcastMessage);
				SendToAll(bMessage);
				break;
			case WhisperMessage:
				System.out.println("case 9");
				int length = dis.readInt();
				byte[] whisperTarget = new byte[length];
				dis.readFully(whisperTarget);
				int length1 = dis.readInt();		
				byte[] whisperMessage = new byte[length1];
				dis.readFully(whisperMessage);
				String user = new String(whisperTarget);
				String wMessage = new String(whisperMessage);
				sendWhisper(user, wMessage);
				break;
			default:
				break;
		}
		
	}
	
	public void sendWhisper(String user, String message) throws IOException {
		for(Map.Entry<String, Socket> mapElement : Clients.entrySet() ) {
			WhisperMessage WM = new WhisperMessage();
			if(mapElement.getKey().contentEquals(user))  {
				DataOutputStream DOS = new DataOutputStream(mapElement.getValue().getOutputStream());
				WM.getRecipient(user);
				WM.getWhisperMessage(message);
				DOS.writeInt(WM.toByteArray().length);
				DOS.write(WM.toByteArray());
				DOS.flush();
			}
			
		}
	}
	
	public void SendToAll(String s) throws IOException {
		for(Map.Entry<String, Socket> mapElement : Clients.entrySet() ) {
			DataOutputStream DOS = new DataOutputStream(mapElement.getValue().getOutputStream());
			BroadcastMessage BM = new BroadcastMessage();
			BM.getMessage(s);
			DOS.writeInt(BM.toByteArray().length);
			DOS.write(BM.toByteArray());
			DOS.flush();
		}
		
	}
	
	public void sendRegResponseDeny() throws IOException {
		RegistrationResponse regres = new RegistrationResponse();
		DOS = new DataOutputStream(sock.getOutputStream());
		DOS.writeInt(regres.toByteArray().length);
		DOS.write(regres.toByteArray());
		DOS.flush();
		sock.close();
	}
	public void sendRegResponse() throws IOException {
		RegistrationResponse regres = new RegistrationResponse();
		DOS = new DataOutputStream(sock.getOutputStream());
		regres.RegistrationSuccess();
		DOS.writeInt(regres.toByteArray().length);
		DOS.write(regres.toByteArray());
		DOS.flush();
	}	
		
	public void sendRegUsers() throws IOException {
		RequestUsersResponse RUR = new RequestUsersResponse();
		DOS = new DataOutputStream(sock.getOutputStream());
		RUR.getHashMap(Clients);
		DOS.writeInt(RUR.toByteArray().length);
		DOS.write(RUR.toByteArray());
		DOS.flush();
	}
	
	public void sendDeRegistrationMessage(String remove) throws IOException{
		DeregistrationResponse DEReg = new DeregistrationResponse();
		DOS = new DataOutputStream(sock.getOutputStream());
		DEReg.getUsername(remove + " has been removed");
		DOS.writeInt(DEReg.toByteArray().length);
		DOS.write(DEReg.toByteArray());
		DOS.flush();
	}
	
	
	
}
