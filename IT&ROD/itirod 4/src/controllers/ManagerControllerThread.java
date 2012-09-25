package controllers;

import java.util.Date;

import beans.Customer;
import beans.DepositAccount;

public class ManagerControllerThread extends Thread {

	private final long WORK_TIMEOUT = 5000;

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
			System.out.println(String.format(startLog, start));
			while (true) {
				this.doWork();
				sleep(WORK_TIMEOUT);
			}
		} catch (Exception ex) {
			System.err.println(ex.getMessage() + "\nManager error");
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
		System.out.println("Manager does his work:");
		System.out.println(String.format("Start cash: %d now: %d", startMoney,
				currentMoney));
		boolean auditResult = currentMoney != startMoney;
		String message = "Should stop: " + Boolean.toString(auditResult);
		System.out.println(message);
		this.context.setShouldStop(auditResult);
	}
}