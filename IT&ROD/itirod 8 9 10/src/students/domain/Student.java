package students.domain;

import java.io.Serializable;

public class Student implements Serializable {
	/**
	 * 
	 */
	private static final long serialVersionUID = -878176809212768792L;
	private int id;
	private Double averageMark;
	private String name;
	private int age;

	public Student() {
		id = -1;
		averageMark = -5.0;
		age = -2;
	}

	public Student(int id, double avgMark, String name, int age) {
		this.id = id;
		this.averageMark = avgMark;
		this.name = name;
		this.age = age;
	}

	public int getAge() {
		return age;
	}

	public void setAge(int age) {
		this.age = age;
	}

	public int getId() {
		return id;
	}

	public void setId(int id) {
		this.id = id;
	}

	public String getName() {
		return this.name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public Double getAverageMark() {
		return this.averageMark;
	}

	public void setAverageMark(Double averageMark) {
		this.averageMark = averageMark;
	}

	@Override
	public String toString() {
		String placeholder = "ID: %d Name: %s Age: %d Average Mark: %f";
		return String.format(placeholder, this.id, this.name, this.age,
				this.averageMark);
	}

	@Override
	public boolean equals(Object another) {
		boolean result = false;
		if (another instanceof Student) {
			Student anotherStudent = (Student) another;
			result = anotherStudent.getId() == this.getId()
					&& this.getName().equals(anotherStudent.getName())
					&& anotherStudent.getAge() == this.getAge()
					&& anotherStudent.getAverageMark().equals(
							this.getAverageMark());
		}
		return result;
	}

	public boolean compareStudentInfo(Student another) {
		boolean result = true;

		result &= this.getAge() == another.getAge();
		result &= this.getName().equals(another.getName());
		result &= this.getAverageMark().equals(another.getAverageMark());
		return result;
	}

}
