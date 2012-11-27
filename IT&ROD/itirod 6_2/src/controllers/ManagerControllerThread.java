package controllers;

import java.util.Date;

import org.apache.log4j.Logger;

import beans.Customer;
import beans.DepositAccount;

public class ManagerControllerThread extends Thread {

	private final long WORK_TIMEOUT = 5000;
	private static Logger log = Logger.getLogger(ManagerControllerThread.class);
	private boolean isValidState = true;
	
	public boolean isValidState() {
		return isValidState;
	}

	public void setValidState(boolean isValidState) {
		this.isValidState = isValidState;
	}

	private QueueModelController context;

	public QueueModelController getContext() {
		return context;
	}

	public void setContext(QueueModelController context) {
		this.context = context;
	}

	public void run() {
		try {

			long start = (new Date()).getTime();
			String startLog = "Manager Daemon started: %d";
			log.debug(String.format(startLog, start));
			while (true) {
				this.doWork();
				sleep(WORK_TIMEOUT);
			}
		} catch (Exception ex) {
			log.error(ex.getMessage() + "\nManager error");
			this.isValidState = false;
			this.context.setShouldStop(true);
		}
	}

	private synchronized void doWork() {

		Customer[] customers = this.context.getCustomers();
		DepositAccount[] accounts = this.context.getDeposits();
		int currentMoney = 0;
		int startMoney = this.context.getTotalCashAmount();
		for (Customer customer : customers) {
			currentMoney += customer.getPurseState();
		}
		for (DepositAccount account : accounts) {
			currentMoney += account.getMoney();
		}
		log.debug("Manager does his work:");
		log.debug(String.format("Start cash: %d now: %d", startMoney,
				currentMoney));
		boolean auditResult = currentMoney != startMoney;
		String message = "Should stop: " + Boolean.toString(auditResult);
		log.debug(message);
		this.isValidState = auditResult;
		this.context.setShouldStop(auditResult);
	}
	
}