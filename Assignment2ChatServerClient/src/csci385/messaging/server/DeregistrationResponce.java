package csci385.messaging.server;

import java.io.ByteArrayOutputStream;
import java.io.DataOutputStream;
import java.io.IOException;

import csci385.messaging.protocol.Messagetype;

public class DeregistrationResponce {
	private int mt = Messagetype.DEREGISTRATION_MESSAGE.getValue();
	private String username;
	
	public byte[] toByteArray() throws IOException {
		ByteArrayOutputStream BAOS = new ByteArrayOutputStream();
		DataOutputStream DOS = new DataOutputStream(BAOS);
		DOS.writeInt(mt);
		byte[] id = username.getBytes();
		DOS.writeInt(id.length);
		DOS.write(id, 0, id.length);
		DOS.flush();
		return BAOS.toByteArray();
	}
	public void getUsername(String s) {
		username = s;
	}
}
