package lab.itirod;

import java.util.LinkedList;
import java.util.List;

public class LinkedListMatrix extends AbstractListMatrix{

    private static final long serialVersionUID = -2017709725864982874L;

    public LinkedListMatrix(int rows, int columns) {
        super(rows, columns);
    }

    @Override
    protected void init(int rows, int columns) {
        this.rows = rows;
        this.columns = columns;
        data = new LinkedList<List<Double>>();
        for (int i = 0; i < rows; i++) {
            LinkedList<Double> column = new LinkedList<Double>();
            for (int j = 0; j < columns; j++)
                column.add(0.0);
            data.add(column);
        }
    }

    @Override
    protected List<Double> createVector(int dimension) {
        LinkedList<Double> result = new LinkedList<Double>();
        for (int j = 0; j < dimension; j++)
            result.add(0.0);
        return result;
    }

    @Override
    protected AbstractListMatrix newInstance(int row, int column) {
        return new LinkedListMatrix(row, column);
    }

}
