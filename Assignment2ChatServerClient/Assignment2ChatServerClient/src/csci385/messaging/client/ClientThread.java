package csci385.messaging.client;

import java.io.ByteArrayInputStream;
import java.io.DataInputStream;
import java.io.IOException;
import java.net.Socket;

public class ClientThread extends Thread{
	Socket socket;
	DataInputStream DIS;
	DataInputStream DIS1;
	ByteArrayInputStream BIS;
	public static final int registrationmessage     = 4;
	public static final int deregistrationmessage   = 5;
	public static final int responseregisteredusers = 7;
	public static final int broadcastmessage        = 8;
	public static final int whispermessage          = 9;
	public ClientThread(Socket sock, DataInputStream input) {
		this.socket = sock;
		DIS1 = input;
	}
	
	public void run() {
		while(true) {
			try {
				int length = DIS1.readInt();
				byte[] arr = DIS1.readNBytes(length);
				getByteArray(arr);
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
	}
	
	
	public void getByteArray(byte[] b) throws IOException {
		BIS = new ByteArrayInputStream(b);
		DIS = new DataInputStream(BIS);
		int messagetype = DIS.readInt();
		switch(messagetype) {
			case registrationmessage:
			    RecieveRegistrationMessage();
				break;
			case deregistrationmessage:
				System.out.println("case: deregistration responce");
				getDeregistrationResponce();
				break;
			case responseregisteredusers:
				System.out.println("case: recieve users");
				recieveUsers();
				break;
				
			case broadcastmessage:
				getBroadcastMessage();
				break;
			case whispermessage:
				getWhisper();
				break;
				
		}
	}
	public void getWhisper() throws IOException {
		
		int length = DIS.readInt();
		byte[] whisperTarget = new byte[length];
		DIS.readFully(whisperTarget);
		
		int length1 = DIS.readInt();		
		byte[] whisperMessage = new byte[length1];
		DIS.readFully(whisperMessage);
		
		String user = new String(whisperTarget);
		String wMessage = new String(whisperMessage);
		System.out.println(wMessage);
	}
	
	public void recieveUsers() throws IOException {
		System.out.println("Current users: ");
		int num = DIS.readInt();
		for(int i = 0; i < num; i++) {
			int size = DIS.readInt();
			byte[] message = new byte[size];
			DIS.readFully(message);
			String m = new String(message);
			System.out.println(m);
		}
	}
	
	public void RecieveRegistrationMessage() throws IOException {
		boolean t = DIS.readBoolean();
		if(!t) {
			System.out.println("Username Already used");
			socket.close();
		}
		else {
			int size = DIS.readInt();
			byte[] message = new byte[size];
			DIS.readFully(message);
			String m = new String(message);
			System.out.println(m);
		}
	}
	
	public void getDeregistrationResponce() throws IOException {
		int size = DIS.readInt();
		byte[] message = new byte[size];
		DIS.readFully(message);
		String m = new String(message);
		System.out.println(m);
	}
	
	public void getBroadcastMessage() throws IOException {
		int size = DIS.readInt();
		byte[] message = new byte[size];
		DIS.readFully(message);
		String m = new String(message);
		System.out.println(m);
	}
	
	
	
	
	
	
}
