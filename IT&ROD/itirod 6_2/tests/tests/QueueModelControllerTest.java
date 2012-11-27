package tests;

import static org.junit.Assert.*;

import org.junit.Test;

import controllers.ManagerControllerThread;
import controllers.QueueModelController;

public class QueueModelControllerTest {

	@Test
	public void test() {
		try {

			QueueModelController model = new QueueModelController(3, 9, 12);
			model.setTime(1);
			model.init(true);
			model.start();
			ManagerControllerThread manager = model.getManager();
			while (!model.isShouldStop()) {
				
			}
			assertTrue("State is valid", manager.isValidState());
		} catch (Exception ex) {
			fail(ex.getMessage());
		}
	}

}
