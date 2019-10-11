package csci385.messaging.protocol;

import java.io.ByteArrayOutputStream;
import java.io.DataOutputStream;
import java.io.IOException;

public class BroadcastMessage {
	private int mt = Messagetype.BROADCAST_MESSAGE.getValue();
	public String username;
	
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
	public void getMessage(String s) {
		username = s;
	}
}