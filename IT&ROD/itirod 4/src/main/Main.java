package main;

import controllers.QueueModelController;
// synchronize this shit
public class Main {

	private static final int CLERK_ORDER = 0;
	private static final int DEPOSIT_ORDER = 1;
	private static final int CUSTOMER_ORDER = 2;

	public static void main(String[] args) {
		if (args == null || args.length < 3) {
			System.out.println("No arguments.");
			return;
		}
		int clerkCount = Integer.parseInt(args[CLERK_ORDER]);
		int depositCount = Integer.parseInt(args[DEPOSIT_ORDER]);
		int customerCount = Integer.parseInt(args[CUSTOMER_ORDER]);
		QueueModelController model = new QueueModelController(clerkCount,
				depositCount, customerCount);
		model.init(true);
		model.start();
	}

}
