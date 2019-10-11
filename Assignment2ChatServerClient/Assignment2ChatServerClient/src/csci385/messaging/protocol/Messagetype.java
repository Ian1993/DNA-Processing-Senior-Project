package csci385.messaging.protocol;

public enum Messagetype {
	REGISTRATION_MESSAGE(3),
	REGISTRATION_RESPONSE(4),
	DEREGISTRATION_MESSAGE(5),
	REQUEST_REGISTERED_USERS(6),
	RESPONSE_REGISTERED_USERS(7),
	BROADCAST_MESSAGE(8),
	WHISPER_MESSAGE(9);
	
	private final int value;
	
	Messagetype(int newValue){
		value = newValue;
	}
	public int getValue() {return value;}
}
