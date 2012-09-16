package lab.itirod;

import java.util.ArrayList;
import java.util.List;

public class ArrayListMatrix extends AbstractListMatrix{

    public ArrayListMatrix(int rows, int columns) {
        super(rows, columns);
    }

    @Override
    protected void init(int rows, int columns) {
        data = new ArrayList<List<Double>>(rows);
        for (int i = 0; i < rows; i++) {
            data.add(new ArrayList<Double>(columns));
            for (int j = 0; j < columns; j++)
                data.get(i).add(0.0);
        }
    }

    @Override
    protected List<Double> createVector(int dimension) {
        ArrayList<Double> result = new ArrayList<Double> (dimension);
        for (int j = 0; j < dimension; j++)
            result.add(0.0);
        return result;
    }

    @Override
    protected AbstractListMatrix newInstance(int row, int column) {
        return new ArrayListMatrix(row, column);
    }

}
