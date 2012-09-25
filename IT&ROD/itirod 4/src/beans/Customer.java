package beans;

public class Customer extends Unit {
	
	private String name;
	private int purseState;

	public Customer() {
		
	}
	
	public Customer(String name, int purseState) {
		this.name = name;
		this.purseState = purseState;
	}

	public synchronized int getPurseState() {
		return purseState;
	}
	
	public synchronized void setPurseState(int purseState) {
		this.purseState = purseState;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}
	
	@Override
	public String toString() {
		String placeholder = "%s Cash: %d";
		return String.format(placeholder, this.name, this.purseState);
	}
	
}
