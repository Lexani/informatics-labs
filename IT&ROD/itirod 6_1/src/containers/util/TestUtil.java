package containers.util;

import java.util.Date;

import org.apache.log4j.Logger;

import containers.domain.AbstractMatrix;
import containers.domain.ArrayListMatrix;
import containers.domain.LinkedListMatrix;

public class TestUtil {

	private static Logger log = Logger.getLogger(TestUtil.class);
	
	private static final int MUL_TEST_SIZE = 4;
	private static final int FREQ_TEST_SIZE = 100;

	public static void testLinkedListFreq() {
		LinkedListMatrix llm1 = 
				new LinkedListMatrix(FREQ_TEST_SIZE, FREQ_TEST_SIZE);
		LinkedListMatrix llm2 = 
				new LinkedListMatrix(FREQ_TEST_SIZE, FREQ_TEST_SIZE);
		LinkedListMatrix llmRes = 
				new LinkedListMatrix(FREQ_TEST_SIZE, FREQ_TEST_SIZE);
		
		log.debug("LinkedList Freq. Mul. Test");
		testFrequency(llm1, llm2, llmRes);
	}
	
	public static void testArrayListFreq() {
		ArrayListMatrix alm1 = 
				new ArrayListMatrix(FREQ_TEST_SIZE, FREQ_TEST_SIZE);
		ArrayListMatrix alm2 = 
				new ArrayListMatrix(FREQ_TEST_SIZE, FREQ_TEST_SIZE);
		ArrayListMatrix almRes = 
				new ArrayListMatrix(FREQ_TEST_SIZE, FREQ_TEST_SIZE);
		
		log.debug("ArrayList Freq. Mul. Test");
		testFrequency(alm1, alm2, almRes);
	}
	
	private static void testFrequency(AbstractMatrix a, AbstractMatrix b, 
													AbstractMatrix res) {
			
		Date start = new Date();
		MatrixUtil.multiply(a, b, res);
		long timeDiff = new Date().getTime() - start.getTime();
		
		String placeholder = "Matrix Multiplication %d x %d took %d ms";
		String formatted = String.format(placeholder, FREQ_TEST_SIZE, FREQ_TEST_SIZE, timeDiff);
		log.debug(formatted);
	}
	
	public static void testLinkedListMul() {
		
		LinkedListMatrix llm1 = 
				new LinkedListMatrix(MUL_TEST_SIZE, MUL_TEST_SIZE);
		LinkedListMatrix llm2 = 
				new LinkedListMatrix(MUL_TEST_SIZE, MUL_TEST_SIZE);
		LinkedListMatrix llmRes = 
				new LinkedListMatrix(MUL_TEST_SIZE, MUL_TEST_SIZE);
		log.debug("LinkedListMatrices mul.");
		testMul(llm1, llm2, llmRes);
	}
	
	public static void testArrayListMul() {
		
		ArrayListMatrix alm1 = 
				new ArrayListMatrix(MUL_TEST_SIZE, MUL_TEST_SIZE);
		ArrayListMatrix alm2 = 
				new ArrayListMatrix(MUL_TEST_SIZE, MUL_TEST_SIZE);
		ArrayListMatrix almRes = 
				new ArrayListMatrix(MUL_TEST_SIZE, MUL_TEST_SIZE);
		log.debug("ArrayListMatrices mul.");
		testMul(alm1, alm2, almRes);
	}
	
	private static void testMul(AbstractMatrix a, AbstractMatrix b, AbstractMatrix res) {
		MatrixUtil.initWithValueMatrix(0.0, a);
		MatrixUtil.initWithValueMatrix(0.0, b);
		MatrixUtil.multiply(a, b, res);
		Boolean result = MatrixUtil.hasAllZeroes(res);
		String placeholder = "0x0 mul. result: %s";
		String formatted = String.format(placeholder, result.toString());
		log.debug(formatted);
		
		MatrixUtil.initIdentityMatrix(a);
		MatrixUtil.initIdentityMatrix(b);
		MatrixUtil.multiply(a, b, res);
		result = MatrixUtil.isIdentityMatrix(res);
		placeholder = "1x1 mul. result: %s";
		formatted = String.format(placeholder, result.toString());
		log.debug(formatted);
		
		MatrixUtil.initIdentityMatrix(a);
		MatrixUtil.initWithValueMatrix(0.0, b);
		MatrixUtil.multiply(a, b, res);
		result = MatrixUtil.hasAllZeroes(res);
		placeholder = "1x0 mul. result: %s";
		formatted = String.format(placeholder, result.toString());
		log.debug(formatted);
	}
		
}
