package csci385.messaging.protocol;

import java.io.ByteArrayInputStream;
import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.net.Socket;

import csci385.messaging.client.BroadcastMessage;
import csci385.messaging.client.DeregistrationMessage;
import csci385.messaging.client.RegistrationRequest;
import csci385.messaging.client.RequestRegisteredUsers;

public class ClientThread extends Thread{
	Socket socket;
	DataInputStream DIS;
	DataInputStream DIS1;
	ByteArrayInputStream BIS;
	
	public ClientThread(Socket sock, DataInputStream input) {
		this.socket = sock;
		DIS1 = input;
	}
	
	public void run() {
		while(true) {
			try {
				int length = DIS1.readInt();
				System.out.println(length);
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
		System.out.println("messageType: " + messagetype);
		switch(messagetype) {
			case 4:
				//System.out.println("case: registration responce");
			    RecieveRegistrationMessage();
				
				break;
			case 5:
				System.out.println("case: deregistration responce");
				getDeregistrationResponce();
				break;
			case 7:
				System.out.println("case: recieve users");
				recieveUsers();
				break;
			case 8:
				// broadcast message
				break;
			case 9:
				//whisper message
				break;
		}
	}
	
	public void recieveUsers() throws IOException {
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
		int size = DIS.readInt();
		byte[] message = new byte[size];
		DIS.readFully(message);
		String m = new String(message);
		System.out.println(m);
	}
	
	public void getDeregistrationResponce() throws IOException {
		int size = DIS.readInt();
		byte[] message = new byte[size];
		DIS.readFully(message);
		String m = new String(message);
		System.out.println(m);
	}
	
	
	
	
	
	
	
	
}
