package csci385.messaging.client;

import java.io.BufferedReader;
import java.io.ByteArrayInputStream;
import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.InetAddress;
import java.net.Socket;
import csci385.messaging.client.*;
import csci385.messaging.protocol.BroadcastMessage;
import csci385.messaging.protocol.DeregistrationMessage;
import csci385.messaging.protocol.RegistrationRequest;
import csci385.messaging.protocol.RequestRegisteredUsers;
import csci385.messaging.protocol.WhisperMessage;
public class ClientDriver {
	boolean stay = true;
	Socket Sock;
	private int ipAddress;
	private InetAddress hostname;
	public DataOutputStream DOS;
	public DataInputStream dis;
	String username;
	BufferedReader BR;
	
	public static void main(String[] args) throws IOException {
	
		ClientDriver ClientDriver = new ClientDriver();
		ClientDriver.initiateObjects(args);
		
	}
	public void initiateObjects(String[] args) throws IOException {
		String host = null;
		String temp = null;
		
		if(args.length > 0) {
			host = args[0];
			if(args[1] != null) {temp = args[1];}
			if(args[2] != null) {username = args[2];}
			
		}
		else {
			BR = new BufferedReader(new InputStreamReader(System.in));
			System.out.println("Enter Host Name: ");
			host = BR.readLine();
			System.out.println("Enter Host IP: ");
			temp = BR.readLine();
			System.out.println("Enter Username: ");
			username = BR.readLine();
			
		}
		ipAddress = Integer.parseInt(temp);
		Sock = new Socket(host, ipAddress);
		Driver();
		
	}
	public void Driver() throws IOException {
		SendRegistrationMessage();
		DataInputStream dis = new DataInputStream(Sock.getInputStream());
		ClientThread listenerThread = new ClientThread(Sock, dis);
		listenerThread.start();
		while(stay) {
			String UserInput = BR.readLine();
			switch(UserInput) {
				case "client-menu":
					System.out.println("client-menu: Print out this list of commands");
					System.out.println("client-deregister: sends a deregistration message to the server, and cleanly closes the program");
					System.out.println("client-query: request a list of currently connected users from the server");
					System.out.println("@<username>: send a whisper to the specified user.");
					System.out.println("@ALL: send a broadcast to everyone currently connected to the server");
					break;
				case "client-deregister":
					SendDeregistrationRequest();
					break;
				case "client-query":
					RequestRegisteredUsers();
					break;
				case "@<username>":
					createWhisper();
					break;
				case "@All":
					toAll();
					break;
				default:
					break;
			}
			
		}
		Sock.close();
	}
	
	public String getreciever() throws IOException {
		System.out.println("Enter user: ");
		String theMessage = BR.readLine();
		return theMessage;
	}
	public String getWMessage() throws IOException {
		System.out.println("Enter your message: ");
		String theMessage = "WhisperFrom<" + username + "> " + BR.readLine();
		return theMessage;
	}
	
	public void createWhisper() throws IOException {
		DataOutputStream DOS = new DataOutputStream(Sock.getOutputStream());
		WhisperMessage WM = new WhisperMessage();
		WM.getRecipient(getreciever());
		WM.getWhisperMessage(getWMessage());
		
		DOS.writeInt(WM.toByteArray().length);
		DOS.write(WM.toByteArray());
		DOS.flush();
	}
	public String getAllmessage() throws IOException {
		System.out.println("Enter your message: ");
		String theMessage = "<" + username + "> " + BR.readLine();
		return theMessage;
	}
	
	public void toAll() throws IOException {
		DataOutputStream DOS = new DataOutputStream(Sock.getOutputStream());
		BroadcastMessage BM = new BroadcastMessage();
		BM.getMessage(getAllmessage());
		DOS.writeInt(BM.toByteArray().length);
		DOS.write(BM.toByteArray());
		DOS.flush();
	}
	public void SendRegistrationMessage() throws IOException {
		DataOutputStream DOS = new DataOutputStream(Sock.getOutputStream());
		RegistrationRequest RegRequest = new RegistrationRequest();
		RegRequest.setUsername(username);
		DOS.writeInt(RegRequest.toByteArray().length);
		DOS.write(RegRequest.toByteArray());
		DOS.flush();
	}
	
	public void RequestRegisteredUsers() throws IOException {
		DataOutputStream DOS = new DataOutputStream(Sock.getOutputStream());
		RequestRegisteredUsers RRU = new RequestRegisteredUsers();
		DOS.writeInt(RRU.toByteArray().length);
		DOS.write(RRU.toByteArray());
		DOS.flush();
	}
	
	public void SendDeregistrationRequest() throws IOException {
		DataOutputStream DOS = new DataOutputStream(Sock.getOutputStream());
		DeregistrationMessage DEREG = new DeregistrationMessage();
		DEREG.setUsername(username);
		DOS.writeInt(DEREG.toByteArray().length);
		DOS.write(DEREG.toByteArray());
		DOS.flush();
	}
	
	
}
