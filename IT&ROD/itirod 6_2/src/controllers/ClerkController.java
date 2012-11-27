package controllers;

import java.util.Date;
import org.apache.log4j.Logger;

import beans.Customer;
import beans.DepositAccount;
import beans.Request;

public class ClerkController implements Runnable {

	private Request request;
	private final long CLERK_TIMEOUT = 8000;
	private static Logger log = Logger.getLogger(ClerkController.class);

	public ClerkController() {

	}

	public ClerkController(Request request) {
		this.request = request;
	}

	public Request getRequest() {
		return request;
	}

	public void setRequest(Request request) {
		this.request = request;
	}

	public void processRequest(Request request) {
		int sum = request.getSum();
		Customer customer = request.getCustomer();
		DepositAccount deposit = request.getAccount();

		int customerCash = customer.getPurseState();
		int depositCash = deposit.getMoney();

		if (sum < 0) {
			customerCash += sum;
			depositCash -= sum;
		} else {
			customerCash -= sum;
			depositCash += sum;
		}
		if (customerCash < 0 || depositCash < 0)
			return;
		customer.setPurseState(customerCash);
		deposit.setMoney(depositCash);
		long timeoutStart = (new Date()).getTime();

		do {
		} while ((new Date()).getTime() - timeoutStart < CLERK_TIMEOUT);

		log.debug("# Processed: \n" + request.toString() + "\n#");
		customer.setBusy(false);
	}

	@Override
	public void run() {
		this.processRequest(request);
	}

}
