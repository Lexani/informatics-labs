package lab.itirod.test;

import lab.itirod.AbstractListMatrix;
import lab.itirod.ArrayListMatrix;
import lab.itirod.LinkedListMatrix;

public class MatrixTest {

    public static void main(String[] args) {
        multZerosArrayTest();
        multZerosLinkedTest();
        multZeroOneArrayTest();
        multZeroOneLinkedTest();
        multOnesArrayTest();
        multOnesLinkedTest();
        performanceTest();
    }

    private static void multZerosArrayTest() {
        try {
            System.out.println("Multiplying two zero matrices");
            int size = 4;
            ArrayListMatrix m1 = new ArrayListMatrix(size, size);
            ArrayListMatrix m2 = new ArrayListMatrix(size, size);
            AbstractListMatrix res = m1.multiply(m2);
            if (res.isZero())
                System.out.println("TEST SUCCEED.");
            else
                System.out.println("TEST FAILED.");
        } catch (Exception e) {
            System.out.println("Test failed. Exception occured.");
        }
    }

    private static void multZeroOneArrayTest() {
        try {
            System.out.println("Multiplying zero matrix with identity");
            int size = 4;
            ArrayListMatrix m1 = new ArrayListMatrix(size, size);
            for (int i = 0; i < size; i++)
                m1.set(1.0, i, i);
            ArrayListMatrix m2 = new ArrayListMatrix(size, size);
            AbstractListMatrix res = m1.multiply(m2);
            if (res.isZero())
                System.out.println("TEST SUCCEED.");
            else
                System.out.println("TEST FAILED.");
        } catch (Exception e) {
            System.out.println("Test failed. Exception occured.");
        }
    }

    private static void multOnesArrayTest() {
        try {
            System.out.println("Multiplying two identity matrices");
            int size = 4;
            ArrayListMatrix m1 = new ArrayListMatrix(size, size);
            for (int i = 0; i < size; i++)
                m1.set(1.0, i, i);
            ArrayListMatrix m2 = new ArrayListMatrix(size, size);
            for (int i = 0; i < size; i++)
                m2.set(1.0, i, i);
            AbstractListMatrix res = m1.multiply(m2);
            if (res.isOne())
                System.out.println("TEST SUCCEED.");
            else
                System.out.println("TEST FAILED.");
        } catch (Exception e) {
            System.out.println("Test failed. Exception occured.");
        }
    }

    private static void multZerosLinkedTest() {
        try {
            System.out.println("Multiplying two zero matrices");
            int size = 4;
            LinkedListMatrix m1 = new LinkedListMatrix(size, size);
            LinkedListMatrix m2 = new LinkedListMatrix(size, size);
            AbstractListMatrix res = m1.multiply(m2);
            if (res.isZero())
                System.out.println("TEST SUCCEED.");
            else
                System.out.println("TEST FAILED.");
        } catch (Exception e) {
            System.out.println("Test failed. Exception occured.");
        }
    }

    private static void multZeroOneLinkedTest() {
        try {
            System.out.println("Multiplying zero matrix with identity");
            int size = 4;
            LinkedListMatrix m1 = new LinkedListMatrix(size, size);
            for (int i = 0; i < size; i++)
                m1.set(1.0, i, i);
            LinkedListMatrix m2 = new LinkedListMatrix(size, size);
            AbstractListMatrix res = m1.multiply(m2);
            if (res.isZero())
                System.out.println("TEST SUCCEED.");
            else
                System.out.println("TEST FAILED.");
        } catch (Exception e) {
            System.out.println("Test failed. Exception occured.");
        }
    }

    private static void multOnesLinkedTest() {
        try {
            System.out.println("Multiplying two identity matrices");
            int size = 4;
            LinkedListMatrix m1 = new LinkedListMatrix(size, size);
            for (int i = 0; i < size; i++)
                m1.set(1.0, i, i);
            LinkedListMatrix m2 = new LinkedListMatrix(size, size);
            for (int i = 0; i < size; i++)
                m2.set(1.0, i, i);
            AbstractListMatrix res = m1.multiply(m2);
            if (res.isOne())
                System.out.println("TEST SUCCEED.");
            else
                System.out.println("TEST FAILED.");
        } catch (Exception e) {
            System.out.println("Test failed. Exception occured.");
        }
    }

    private static void performanceTest() {
        try {
            System.out.println("Multiplying two zero matrices");
            int size = 100;
            ArrayListMatrix ma1 = new ArrayListMatrix(size, size);
            ArrayListMatrix ma2 = new ArrayListMatrix(size, size);
            LinkedListMatrix ml1 = new LinkedListMatrix(size, size);
            LinkedListMatrix ml2 = new LinkedListMatrix(size, size);
            for (int i = 0; i < size; i++) {
                ml1.set(1.0, i, i);
                ml2.set(1.0, i, i);
                ma1.set(1.0, i, i);
                ma2.set(1.0, i, i);
            }
            long arrayTime, linkedTime;
            long start = System.currentTimeMillis();
            ma1.multiply(ma2);
            arrayTime = (System.currentTimeMillis() - start);
            start = System.currentTimeMillis();
            ml1.multiply(ml2);
            linkedTime = (System.currentTimeMillis() - start);
            System.out.println("ArrayList implementation time = " + arrayTime
                    + " ms");
            System.out.println("LinkedList implementation time = " + linkedTime
                    + " ms");
        } catch (Exception e) {
            System.out.println("Test failed. Exception occured.");
        }
    }

}
