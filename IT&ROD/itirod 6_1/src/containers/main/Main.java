package containers.main;

import org.apache.log4j.Logger;

import containers.util.TestUtil;

public class Main {
	
	private static Logger log = Logger.getLogger(Main.class);
	
	public static void main(String[] args) {
		try {
			TestUtil.testArrayListMul();
			TestUtil.testLinkedListMul();
			TestUtil.testArrayListFreq();
			TestUtil.testLinkedListFreq();		
		} catch (Exception ex) {
			log.error(ex.getMessage());
		}

	}

}
