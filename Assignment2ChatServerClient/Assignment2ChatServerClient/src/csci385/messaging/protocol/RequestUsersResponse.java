package csci385.messaging.protocol;

import java.io.ByteArrayOutputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.net.InetAddress;
import java.net.Socket;
import java.util.HashMap;
import java.util.Set;

public class RequestUsersResponse {
	private int mt = Messagetype.RESPONSE_REGISTERED_USERS.getValue();
	
	private HashMap<String, Socket> currentUsers;
	public byte[] toByteArray() throws IOException {
		ByteArrayOutputStream BAOS = new ByteArrayOutputStream();
		DataOutputStream DOS = new DataOutputStream(BAOS);
		DOS.writeInt(mt);
		int m = currentUsers.size();
		DOS.writeInt(m);
		for(String entry : currentUsers.keySet()) {
			DOS.writeInt(entry.getBytes().length);
			DOS.write(entry.getBytes());
		}
		
		DOS.flush();
		return BAOS.toByteArray();
	}
	public void getHashMap(HashMap<String,Socket> clients) {
		currentUsers = clients;
	}
}
