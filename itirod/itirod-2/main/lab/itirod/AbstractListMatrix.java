package lab.itirod;

import java.util.List;

public abstract class AbstractListMatrix {
    private int rows, columns;
    protected List<List<Double>> data;

    public int getRows() {
        return rows;
    }

    public int getColumns() {
        return columns;
    }

    public AbstractListMatrix(int rows, int columns) {
        this.rows = rows;
        this.columns = columns;
        init(rows, columns);
    }

    private void validateRow(int row) {
        if (row >= rows || row < 0)
            throw new IllegalArgumentException("Illegal access to matrix item");
    }

    private void validateColumn(int column) {
        if (column >= columns || column < 0)
            throw new IllegalArgumentException("Illegal access to matrix item");
    }

    private void validate(int row, int column) {
        validateRow(row);
        validateColumn(column);
    }

    protected abstract void init(int rows, int columns);

    protected abstract List<Double> createVector(int dimension);

    protected abstract AbstractListMatrix newInstance(int row, int column);

    public List<Double> getRow(int rowIndex) {
        validateRow(rowIndex);
        return data.get(rowIndex);
    }

    public List<Double> getColumn(int column) {
        validateColumn(column);
        List<Double> result = createVector(rows);
        for (int i = 0; i < data.size(); i++)
            result.set(i, data.get(i).get(column));
        return result;
    }

    public void set(double value, int rowIndex, int columnIndex) {
        validate(rowIndex, columnIndex);
        List<Double> row = getRow(rowIndex);
        row.set(columnIndex, value);
    }

    public double get(int rowIndex, int columnIndex) {
        validate(rowIndex, columnIndex);
        return data.get(rowIndex).get(columnIndex);
    }

    public AbstractListMatrix multiply(AbstractListMatrix other) {
        if (this.getColumns() != other.getRows())
            throw new IllegalArgumentException("Matrices are not consistent");
        AbstractListMatrix result = newInstance(this.getColumns(),
                other.getRows());
        int len = this.getColumns();

        for (int i = 0; i < this.getRows(); i++)
            for (int j = 0; j < other.getColumns(); j++) {
                List<Double> row = this.getRow(i);
                List<Double> column = other.getColumn(j);
                double res = 0;
                for (int k = 0; k < len; k++)
                    res += row.get(k) * column.get(k);
                result.set(res, i, j);
            }
        return result;
    }

    @Override
    public String toString() {
        return data.toString();
    }

    public boolean isZero() {
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < columns; j++)
                if (get(i, j) != 0.0)
                    return false;
        return true;
    }

    public boolean isOne() {
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < columns; j++)
                if (i != j && get(i, j) != 0.0)
                    return false;
        return true;
    }

}
