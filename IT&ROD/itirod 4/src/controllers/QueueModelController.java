package controllers;

import java.util.*;
import java.util.concurrent.*;

import beans.Customer;
import beans.DepositAccount;
import beans.Request;
import beans.Unit;

public class QueueModelController {

	private long keepAliveTime = 10;
	private long time = 1000000;

	private ThreadPoolExecutor threadPool = null;
	private ArrayBlockingQueue<Runnable> queue;

	private int defaultAccountCapacity = 5000;
	private int accountCapacityLimit = 15000;
	private int defaultPurseState = 150;
	private int purseStateLimit = 3000;
	private int totalCashAmount;
	private Random rand;

	private int depositCount;
	private int clerkCount;
	private int customerCount;
	private DepositAccount[] deposits;
	private boolean shouldStop;
	
	public boolean isShouldStop() {
		return shouldStop;
	}

	public void setShouldStop(boolean shouldStop) {
		this.shouldStop = shouldStop;
	}

	public DepositAccount[] getDeposits() {
		return deposits;
	}

	public void setDeposits(DepositAccount[] deposits) {
		this.deposits = deposits;
	}

	private Customer[] customers;

	public Customer[] getCustomers() {
		return customers;
	}

	public void setCustomers(Customer[] customers) {
		this.customers = customers;
	}

	public QueueModelController(int clerkCount, int depositCount,
			int customerCount) {
		this.depositCount = depositCount;
		this.clerkCount = clerkCount;
		this.customerCount = customerCount;
	}

	public long getTime() {
		return time;
	}

	public void init(boolean randomized) {

		queue = new ArrayBlockingQueue<Runnable>(customerCount);
		threadPool = new ThreadPoolExecutor(clerkCount, clerkCount,
				keepAliveTime, TimeUnit.SECONDS, queue);

		rand = new Random();
		if (randomized) {
			this.defaultAccountCapacity = rand.nextInt(accountCapacityLimit);
			this.defaultPurseState = rand.nextInt(purseStateLimit);
		}
		this.initCustomers(customerCount);
		this.initDepositAccounts(depositCount);
	}

	private void initCustomers(int customerCount) {
		this.customers = new Customer[customerCount];
		String placeholder = "Customer%d";
		for (int i = 0; i < customerCount; i++) {
			String customerName = String.format(placeholder, i);
			this.customers[i] = new Customer(customerName, defaultPurseState);
			this.setTotalCashAmount(this.getTotalCashAmount()
					+ defaultPurseState);
		}
	}

	private void initDepositAccounts(int depositCount) {
		this.deposits = new DepositAccount[depositCount];
		for (int i = 0; i < depositCount; i++) {
			this.deposits[i] = new DepositAccount(defaultAccountCapacity, i);
			setTotalCashAmount(getTotalCashAmount() + defaultAccountCapacity);
		}
	}

	private void logStart(long time) {
		String placeholder = "Model started at %d ms";
		System.out.println(String.format(placeholder, time));
	}

	private void logAddedRequest(Request request) {
		System.out.println("# Added: \n" + request.toString() + "\n#");
	}

	public void start() {

		long start = new Date().getTime();
		this.logStart(start);
		try {
			ManagerControllerThread manager = new ManagerControllerThread();
			manager.setContext(this);
			manager.setDaemon(true);
			manager.start();
		} catch (Exception ex) {
			System.err.println("Daemon init failed: " + ex.getMessage());
		}

		do {
			Customer customer = (Customer) findUnused(customers);
			customer.setBusy(true);
			int index = rand.nextInt(deposits.length);
			DepositAccount deposit = (DepositAccount) deposits[index];
			int sum = rand.nextInt(defaultPurseState);
			sum = rand.nextInt() % 2 == 0 ? sum : -sum;

			Request request = new Request(sum, customer, deposit);
			ClerkController controller = new ClerkController(request);
			this.logAddedRequest(request);

			try {
				threadPool.execute(controller);
			} catch (RejectedExecutionException ex) {
				System.err
						.println("Thread execution error: " + ex.getMessage());
			}

			System.out.println("Queue size: " + Integer.toString(queue.size()));
		} while(!this.shouldStop || (new Date()).getTime() - start < time);
		while (!queue.isEmpty()) {
		
		}
		
		threadPool.shutdown();

	}

	private Unit findUnused(Unit[] units) {
		Unit result = null;
		int unitCount = units.length;
		do {
			int index = rand.nextInt(unitCount);
			result = units[index];
		} while (result.isBusy());
		return result;
	}

	public DepositAccount getDepositAccount(int index) {
		if (index < 0 || index > this.depositCount)
			throw new IndexOutOfBoundsException();

		return this.deposits[index];
	}

	public void setDepositAccount(int index, DepositAccount account) {
		if (account == null || index > depositCount || index < 0)
			throw new IllegalArgumentException();

		this.deposits[index] = account;
	}

	public Customer getCustomer(int index) {
		if (index < 0 || index > this.clerkCount)
			throw new IndexOutOfBoundsException();

		return this.customers[index];
	}

	public void setCustomer(int index, Customer customer) {
		if (customer == null || index > customerCount || index < 0)
			throw new IllegalArgumentException();

		this.customers[index] = customer;
	}

	public int getTotalCashAmount() {
		return totalCashAmount;
	}

	public void setTotalCashAmount(int totalCashAmount) {
		this.totalCashAmount = totalCashAmount;
	}

}
