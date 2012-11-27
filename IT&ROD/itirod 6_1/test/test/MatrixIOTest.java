package test;

import static org.junit.Assert.*;

import org.junit.Test;

import containers.domain.AbstractMatrix;
import containers.domain.ArrayListMatrix;
import containers.util.MatrixReader;
import containers.util.MatrixUtil;
import containers.util.MatrixWriter;

public class MatrixIOTest {

	private final String TEST_PATH = "temp/test-containers.txt";

	@Test
	public void testSerialization() {
		MatrixWriter writer = new MatrixWriter();
		MatrixReader reader = new MatrixReader();
		AbstractMatrix out = MatrixUtil.generateFooMatrix();
		try {
			writer.writeMatrix(out, TEST_PATH);
			AbstractMatrix in = reader.readMatrix(TEST_PATH);
			assertNotNull("IN not null", in);
			assertEquals("IN and OUT values are equal in IOObjectStream", in, out);

		} catch (Exception e) {
			fail("Exception occurred: " + e.getMessage());
		}
	}

	@Test
	public void testFileIO() {
		MatrixWriter writer = new MatrixWriter();
		MatrixReader reader = new MatrixReader();
		AbstractMatrix out = MatrixUtil.generateFooMatrix();
		try {
			writer.write(out, TEST_PATH);
			int cols = reader.readColCount(TEST_PATH);
			int rows = reader.readRowCount(TEST_PATH);
			AbstractMatrix in = new ArrayListMatrix(rows, cols);
			reader.read(TEST_PATH, in);
			assertNotNull("IN not null", in);
			assertEquals("IN and OUT values are equal in IOFile", in, out);

		} catch (Exception e) {
			fail("Exception occurred: " + e.getMessage());
		}
	}

}
