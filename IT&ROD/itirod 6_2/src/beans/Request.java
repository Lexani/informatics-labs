package beans;

public class Request {

	private int sum;
	private Customer customer;
	private DepositAccount account;

	public Request() {

	}

	public Request(int sum, Customer customer, DepositAccount account) {
		this.sum = sum;
		this.customer = customer;
		this.account = account;
	}

	public DepositAccount getAccount() {
		return account;
	}

	public void setAccount(DepositAccount account) {
		this.account = account;
	}

	public Customer getCustomer() {
		return customer;
	}

	public void setCustomer(Customer customer) {
		this.customer = customer;
	}

	public int getSum() {
		return sum;
	}

	public void setSum(int sum) {
		this.sum = sum;
	}

	@Override
	public String toString() {
		String placeholder = "Request\nCustomer: %s\nAccount: %s\nSum: %d";
		return String.format(placeholder, this.customer.toString(),
				this.account.toString(), this.sum);
	}
}
