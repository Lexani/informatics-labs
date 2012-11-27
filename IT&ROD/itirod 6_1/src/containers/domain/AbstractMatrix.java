package containers.domain;

import java.io.Serializable;
import java.util.List;
import java.util.Random;

public abstract class AbstractMatrix implements Serializable{
	
	private static final long serialVersionUID = 5938769546818262492L;
	private int rowCount;
	private int colCount;
	
	protected List<List<Double>> data;
	
	public List<List<Double>> getData() {
		return data;
	}

	public void setData(List<List<Double>> data) {
		this.data = data;
	}

	public AbstractMatrix() {
		
	}
	
	public AbstractMatrix(int rowCount, int colCount) {
		this.rowCount = rowCount;
		this.colCount = colCount;
	}
	
	public int getRowCount() {
		return this.rowCount;
	}
	
	public int getColCount() {
		return this.colCount;
	}
	
	public Double getValue(int rowIndex, int colIndex) 
							throws IndexOutOfBoundsException {
		
		if(validateIndeces(rowIndex, colIndex))
			return data.get(rowIndex).get(colIndex);
		throw new IndexOutOfBoundsException();
	}
	
	public void setValue(double value, int rowIndex, int colIndex) 
									throws IndexOutOfBoundsException {
		
		if(!validateIndeces(rowIndex, colIndex))
			throw new IndexOutOfBoundsException();
		data.get(rowIndex).set(colIndex, value);
	}
	
	private boolean validateIndeces(int rowIndex, int colIndex) {
		boolean isValidRow = rowIndex >= 0 && rowIndex < this.rowCount;
		boolean isValidCol = colIndex >= 0 && colIndex < this.colCount;
		return isValidCol && isValidRow;
	}
	
	@Override
	public boolean equals(Object another) {
		
		if(!(another instanceof AbstractMatrix))
			return false;
		
		AbstractMatrix cmp = (AbstractMatrix)another;
		
		if(this.colCount != cmp.getColCount() || this.rowCount != cmp.getRowCount())
			return false;
		
		for(int i = 0; i < this.rowCount; i++) {
			for(int j = 0; j < this.colCount; j++) {
				if(this.getValue(i, j).compareTo(cmp.getValue(i, j)) != 0)
					return false;
			}
		}
		return true;
	}
	
	@Override
	public int hashCode() {
		Random rand = new Random();
		int rowIndex = rand.nextInt(this.rowCount);
		int colIndex = rand.nextInt(this.colCount);
		return this.getValue(rowIndex, colIndex).intValue();
	}
	
	@Override
	public String toString() {
		StringBuilder sb = new StringBuilder();
		for(int i = 0; i < this.rowCount; i++) {
			for(int j = 0; j < this.colCount; j++) {
				Double value = this.getValue(i, j);
				sb.append(value.toString() + " ");
			}
			sb.append("\n");
		}
		return sb.toString();
	}

}
