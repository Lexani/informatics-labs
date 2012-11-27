package containers.util;

import java.io.FileOutputStream;
import java.io.FileWriter;
import java.io.IOException;
import java.io.ObjectOutputStream;
import java.io.Writer;

import containers.domain.AbstractMatrix;

public class MatrixWriter {

	public void write(AbstractMatrix matrix, String fileName)
			throws IOException {
		
		Writer writer = new FileWriter(fileName);
		writer.append(String.format("%d\n", matrix.getRowCount()));
		writer.append(String.format("%d\n", matrix.getColCount()));
		writer.append(matrix.toString());
		writer.close();
	}

	public void writeMatrix(AbstractMatrix matrix, String fileName)
			throws IOException {
		
		ObjectOutputStream oos = null;
		FileOutputStream fos = new FileOutputStream(fileName);
		oos = new ObjectOutputStream(fos);
		oos.writeObject(matrix);
		oos.close();
	}

}
