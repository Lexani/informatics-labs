package beans;

public class DepositAccount extends Unit {

	private int money;
	private int accountId;

	public DepositAccount() {

	}

	public DepositAccount(int money, int accountId) {
		this.money = money;
		this.accountId = accountId;
	}

	public int getAccountId() {
		return accountId;
	}

	public void setAccountId(int accountId) {
		this.accountId = accountId;
	}

	public synchronized int getMoney() {
		return money;
	}

	public synchronized void setMoney(int money) {
		this.money = money;
	}

	@Override
	public String toString() {
		String placeholder = "Account%d Cash: %d";
		return String.format(placeholder, this.accountId, this.money);
	}
}
