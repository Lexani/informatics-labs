package main;

import java.util.List;

import repository.JdbcStudentDao;
import repository.StudentDao;
import domain.Student;


public class Main {

	public static void main(String[] args) {
		StudentDao dao = new JdbcStudentDao();
		
		List<Student> students = dao.getStudents();
		printStudents(students);
		Student s = dao.getStudent(5);
		System.out.println("FOUND: " + s.toString());
		
		s.setName("IVANOV");
		s.setAge(120);
		dao.updateStudent(s);
		students = dao.getStudents();
		printStudents(students);
		
		dao.deleteStudent(5);
		students = dao.getStudents();
		printStudents(students);
		
		dao.insertStudent(s);
		students = dao.getStudents();
		printStudents(students);
	}
	
	private static void printStudents(List<Student> students) {
		System.out.println("Student List: ");
		for(Student student : students) {
			System.out.println(student.toString());
		}
	}

}