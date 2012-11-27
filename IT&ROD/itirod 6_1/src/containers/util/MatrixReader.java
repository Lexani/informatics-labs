package containers.util;

import java.io.BufferedReader;
import java.io.DataInputStream;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.ObjectInputStream;

import org.apache.log4j.Logger;

import containers.domain.AbstractMatrix;

public class MatrixReader {

	private Logger log = Logger.getLogger(MatrixReader.class);
	
	public int readColCount(String fileName) throws IOException {
		DataInputStream in = null;
		try {
			//int i = 1/0;
			FileInputStream fstream = new FileInputStream(fileName);
			in = new DataInputStream(fstream);
			BufferedReader br = new BufferedReader(new InputStreamReader(in));
			String strLine;
			strLine = br.readLine();
			strLine = br.readLine();
			int cols = Integer.parseInt(strLine);
			return cols;
		} catch (Exception ex) {
			log.error(ex);
		} finally {
			if (in != null)
				in.close();
		}
		return -1;

	}

	public int readRowCount(String fileName) throws IOException {
		DataInputStream in = null;
		try {
			FileInputStream fstream = new FileInputStream(fileName);
			in = new DataInputStream(fstream);
			BufferedReader br = new BufferedReader(new InputStreamReader(in));
			String strLine;
			strLine = br.readLine();
			int rows = Integer.parseInt(strLine);
			return rows;
		} catch (Exception ex) {
			log.error(ex.getMessage());
		} finally {
			if (in != null)
				in.close();
		}
		return -1;

	}

	public void read(String fileName, AbstractMatrix matrix) throws IOException {
		DataInputStream in = null;
		try {
			FileInputStream fstream = new FileInputStream(fileName);
			in = new DataInputStream(fstream);
			BufferedReader br = new BufferedReader(new InputStreamReader(in));
			String strLine;
			strLine = br.readLine();
			strLine = br.readLine();
			int i = 0;
			while ((strLine = br.readLine()) != null) {
				String[] elements = strLine.split(" ");
				for (int j = 0; j < elements.length; j++) {
					Double value = Double.parseDouble(elements[j]);
					matrix.setValue(value, i, j);
				}
				i++;
			}
			in.close();
		} catch (Exception e) {
			log.error("Error: " + e.getMessage());
		} finally {
			if (in != null)
				in.close();
		}
	}

	public AbstractMatrix readMatrix(String fileName) 
			throws IOException, ClassNotFoundException {
		FileInputStream fis = new FileInputStream(fileName);
		ObjectInputStream ois = new ObjectInputStream(fis);

		AbstractMatrix matrix = (AbstractMatrix) ois.readObject();

		ois.close();
		return matrix;
	}

}