package domain;

public class Student {
	private int id;
	private Double averageMark;
	private String name;
	private int age;

	public Student() {
		
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
		String placeholder = "ID: %d Name: %s Age: %d";
		return String.format(placeholder, this.id, this.name, this.age);
	}
	
	
}
