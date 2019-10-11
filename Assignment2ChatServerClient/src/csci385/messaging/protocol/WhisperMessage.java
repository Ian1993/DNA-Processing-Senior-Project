package csci385.messaging.protocol;

import java.io.ByteArrayOutputStream;
import java.io.DataOutputStream;
import java.io.IOException;

public class WhisperMessage {
	private int mt = Messagetype.WHISPER_MESSAGE.getValue();
	private String message;
	private String ToUser;
	
	public byte[] toByteArray() throws IOException {
		ByteArrayOutputStream BAOS = new ByteArrayOutputStream();
		DataOutputStream DOS = new DataOutputStream(BAOS);
		DOS.writeInt(mt);
		
		byte[] user = ToUser.getBytes();
		DOS.writeInt(user.length);
		DOS.write(user, 0, user.length);
		
		
		byte[] id = message.getBytes();
		DOS.writeInt(id.length);
		DOS.write(id, 0, id.length);
		DOS.flush();
		return BAOS.toByteArray();
	}
	
	public void getRecipient(String s) {
		ToUser = s;
	}
	
	public void getWhisperMessage(String s) {
		message = s;
	}
	
	public String GetToUser() {
		return ToUser;
	}
}
