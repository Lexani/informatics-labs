package containers.util;

import containers.domain.AbstractMatrix;
import containers.domain.ArrayListMatrix;

public class MatrixUtil {

	public static void multiply(AbstractMatrix a, AbstractMatrix b,
			AbstractMatrix outResult) throws IllegalArgumentException {
		
		if (!validateMutliplyOperands(a, b, outResult))
			throw new IllegalArgumentException();
		
		for(int i = 0; i < a.getRowCount(); i++) {
			for(int j = 0; j < b.getColCount(); j++) {
				double value = 0.0;
				for(int k = 0; k < b.getRowCount(); k++) {
					value += a.getValue(i, k) * b.getValue(k, j);
				}
				outResult.setValue(value, i, j);
			}
		}
	}

	public static void initWithValueMatrix(double value, AbstractMatrix outMatrix) {
		for(int i = 0; i < outMatrix.getRowCount(); i++) {
			for(int j = 0; j < outMatrix.getColCount(); j++) {
				outMatrix.setValue(value, i, j);
			}
		}
	}
	
	public static void initIdentityMatrix(AbstractMatrix outMatrix) 
											throws IllegalArgumentException {
		
		if(outMatrix.getColCount() != outMatrix.getRowCount())
			throw new IllegalArgumentException();
		
		MatrixUtil.initWithValueMatrix(0.0, outMatrix);
		for(int i = 0; i < outMatrix.getRowCount(); i++) {
			outMatrix.setValue(1.0, i, i);
		}
	}

	public static boolean hasAllZeroes(AbstractMatrix matrix) {
		
		for(int i = 0; i < matrix.getRowCount(); i++) {
			for(int j = 0; j < matrix.getColCount(); j++) {
				if(matrix.getValue(i, j).compareTo(0.0) != 0)
					return false;
			}
		}
		return true;
	}
	
	public static boolean isIdentityMatrix(AbstractMatrix matrix) {
		
		for(int i = 0; i < matrix.getRowCount(); i++) {
			for(int j = 0; j < matrix.getColCount(); j++) {
				Double value = matrix.getValue(i, j);
				if(i != j) {
					if(value.compareTo(0.0) == 0)
						continue;
					return false;
				}
				if(value.compareTo(1.0) != 0)
					return false;
			}
		}
		return true;
	}
	
	public static ArrayListMatrix generateFooMatrix() {
		ArrayListMatrix m0 = new ArrayListMatrix(3, 3);
		
		m0.setValue(1.0, 0, 0);
		m0.setValue(2.0, 0, 1);
		m0.setValue(-1.0, 0, 2);
		m0.setValue(3.0, 1, 0);
		m0.setValue(4.0, 1, 1);
		m0.setValue(0.0, 1, 2);
		m0.setValue(-1.0, 2, 0);
		m0.setValue(2.0, 2, 1);
		m0.setValue(-2.0, 2, 2);
		return m0;
	}
	
	private static boolean validateMutliplyOperands(AbstractMatrix a,
			AbstractMatrix b, AbstractMatrix c) {
		
		return a.getColCount() == b.getRowCount() 
				&& a.getRowCount() == c.getRowCount()
				&& b.getColCount() == c.getColCount();
		
	}
	
	
	
}
