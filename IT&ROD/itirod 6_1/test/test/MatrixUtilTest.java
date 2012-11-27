package test;
import static org.junit.Assert.*;

import java.io.IOException;

import org.junit.Test;

import containers.domain.AbstractMatrix;
import containers.domain.ArrayListMatrix;
import containers.util.MatrixReader;
import containers.util.MatrixUtil;


public class MatrixUtilTest {

	private final String PATH_TEST_M_A = "test/test-values/test-mul-a";
	
	@Test
	public void testMultiply() {
		MatrixReader reader = new MatrixReader();
		try {
			AbstractMatrix a = reader.readMatrix(PATH_TEST_M_A);
			int rows = a.getRowCount();
			int cols = a.getColCount();
			
			AbstractMatrix z = new ArrayListMatrix(rows, cols);
			MatrixUtil.initWithValueMatrix(0, z);
			assertNotNull("Zero matrix initialized", z);
			
			AbstractMatrix i = new ArrayListMatrix(rows, cols);
			MatrixUtil.initIdentityMatrix(i);
			assertNotNull("Identity matrix initialized", z);
			
			ArrayListMatrix result = new ArrayListMatrix(rows, cols);
			MatrixUtil.multiply(a, z, result);
			assertEquals("M x 0 == 0", z, result);
			
			MatrixUtil.multiply(a, i, result);
			assertEquals("M x 1 == M", a, result);
			
			MatrixUtil.multiply(z, z, result);
			assertEquals("0 x 0 == 0", z, result);
			
			MatrixUtil.multiply(i, i, result);
			assertEquals("1 x 1 == 1", i, result);
			
		} catch (Exception ex) {
			System.out.println(ex.getMessage());
			ex.printStackTrace();
			fail("Exception occurred");
		}
	}

}
