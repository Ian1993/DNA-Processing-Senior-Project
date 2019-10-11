package csci385.messaging.protocol;

import java.io.ByteArrayOutputStream;
import java.io.DataOutputStream;
import java.io.IOException;

public class RegistrationResponse {
	private int mt = Messagetype.REGISTRATION_RESPONSE.getValue();
	private String greeting = "failed to register";
	Boolean success = false;
	
	public byte[] toByteArray() throws IOException {
		ByteArrayOutputStream BAOS = new ByteArrayOutputStream();
		DataOutputStream DOS = new DataOutputStream(BAOS);
		DOS.writeInt(mt);
		DOS.writeBoolean(success);
		byte[] id = greeting.getBytes();
		DOS.writeInt(id.length);
		DOS.write(id, 0, id.length);
		DOS.flush();
		return BAOS.toByteArray();
	}
	
	public void RegistrationSuccess() {
		success = true;
		greeting = "You have been registered.";
	}
	
}
