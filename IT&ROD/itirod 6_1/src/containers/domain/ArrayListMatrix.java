package containers.domain;

import java.util.ArrayList;
import java.util.List;

public class ArrayListMatrix extends AbstractMatrix {

	private static final long serialVersionUID = 8182791078788275515L;

	public ArrayListMatrix() {
		super();
	}
	
	public ArrayListMatrix(int rowCount, int colCount) {
		super(rowCount, colCount);
		this.data = new ArrayList<List<Double>>();
		for(int i = 0; i < rowCount; i++) {
			this.data.add(this.initRow(colCount));
		}
	}
	
	private ArrayList<Double> initRow(int count) {
		ArrayList<Double> result = new ArrayList<Double>();
		for(int i = 0; i < count; i++) {
			result.add(0.0);
		}
		return result;
	}
	
}
