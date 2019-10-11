package csci385.messaging.server;

import java.io.ByteArrayOutputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.net.InetAddress;
import java.util.HashMap;
import java.util.Set;

import csci385.messaging.protocol.Messagetype;

public class RequestUsersResponce {
	private int mt = Messagetype.RESPONSE_REGISTERED_USERS.getValue();
	
	private HashMap<String, InetAddress> currentUsers;
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
	public void getHashMap(HashMap<String, InetAddress> b) {
		currentUsers = b;
	}
}
