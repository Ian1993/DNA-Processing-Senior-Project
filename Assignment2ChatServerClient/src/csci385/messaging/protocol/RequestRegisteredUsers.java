package csci385.messaging.protocol;

import java.io.ByteArrayOutputStream;
import java.io.DataOutputStream;
import java.io.IOException;

public class RequestRegisteredUsers {
	private int mt = Messagetype.REQUEST_REGISTERED_USERS.getValue();
	private String username;
	
	public byte[] toByteArray() throws IOException {
		ByteArrayOutputStream BAOS = new ByteArrayOutputStream();
		DataOutputStream DOS = new DataOutputStream(BAOS);
		DOS.writeInt(mt);
		DOS.flush();
		return BAOS.toByteArray();
	}
}
