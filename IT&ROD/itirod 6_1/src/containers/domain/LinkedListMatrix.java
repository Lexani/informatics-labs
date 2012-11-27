package containers.domain;

import java.util.LinkedList;
import java.util.List;

public class LinkedListMatrix extends AbstractMatrix {
	
	private static final long serialVersionUID = 8134826036245803828L;

	public LinkedListMatrix() {
		super();
	}
	
	public LinkedListMatrix(int rowCount, int colCount) {
		super(rowCount, colCount);
		this.data = new LinkedList<List<Double>>();
		for(int i = 0; i < rowCount; i++) {
			this.data.add(this.initRow(colCount));
		}
	}
	
	private LinkedList<Double> initRow(int count) {
		LinkedList<Double> result = new LinkedList<Double>();
		for(int i = 0; i < count; i++) {
			result.add(0.0);
		}
		return result;
	}
	
	
}
