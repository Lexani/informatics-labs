package students.repository;

import java.sql.*;
import java.util.ArrayList;
import java.util.List;

import org.apache.log4j.Logger;

import students.domain.Student;
import students.webapi.ValidationUtil;

public class JdbcStudentDao implements StudentDao {

	private final String URL = "jdbc:mysql://localhost:3306/test";
	private final String USER = "root";
	private final String PASSWORD = "root";
	private final String DRIVER_CLASS_NAME = "com.mysql.jdbc.Driver";
	private static boolean isDriverLoaded;

	private static Logger log = Logger.getLogger(JdbcStudentDao.class);

	private final String SELECT_STUDENT_QUERY = "SELECT NAME, AVGMARK, AGE, ID FROM STUDENTS WHERE ID = ?";
	private final String SELECT_STUDENTS_QUERY = "SELECT ID, NAME, AVGMARK, AGE FROM STUDENTS ORDER BY ID;";
	private final String INSERT_STUDENT_QUERY = "INSERT INTO STUDENTS (NAME, AGE, AVGMARK)"
			+ "VALUES (?, ?, ?);";
	private final String UPDATE_STUDENT_QUERY = "UPDATE STUDENTS " + "SET "
			+ "NAME = ?, AGE = ?, AVGMARK = ?" + "WHERE ID = ?;";

	private final String DELETE_STUDENT_QUERY = "DELETE FROM STUDENTS WHERE ID = ?";

	// static constructor
	public JdbcStudentDao() {
		if (!isDriverLoaded) {
			try {
				Class.forName(DRIVER_CLASS_NAME);
			} catch (ClassNotFoundException e) {
				e.printStackTrace();
				log.error(e.getMessage());
			}
		}
	}

	@Override
	public void insertStudent(Student student) {
		PreparedStatement pstmt = null;
		try {
			Connection con = DriverManager.getConnection(URL, USER, PASSWORD);
			pstmt = con.prepareStatement(INSERT_STUDENT_QUERY);
			String validName = student.getName().replaceAll(
					ValidationUtil.TRIM_REGEXP, "");
			pstmt.setString(1, validName);
			pstmt.setInt(2, student.getAge());
			pstmt.setDouble(3, student.getAverageMark());
			pstmt.execute();
		} catch (Exception ex) {
			log.error("JdbcStudentDao.insertStudent: " + ex.getMessage());
		} finally {
			try {
				if (pstmt != null && pstmt.isClosed()) {
					pstmt.close();
				}
			} catch (SQLException e) {
				e.printStackTrace();
				log.error(e.getMessage());
			}
		}
	}

	@Override
	public Student getStudent(int id) {
		PreparedStatement pstmt = null;
		try {
			Connection con = DriverManager.getConnection(URL, USER, PASSWORD);
			pstmt = con.prepareStatement(SELECT_STUDENT_QUERY);
			pstmt.setInt(1, id);
			pstmt.execute();
			ResultSet res = pstmt.getResultSet();
			Student student = new Student();
			while (res.next()) {
				student.setId(res.getInt("ID"));
				student.setAverageMark(res.getDouble("AVGMARK"));
				student.setName(res.getString("NAME"));
				student.setAge(res.getInt("AGE"));
			}
			return student;
		} catch (Exception ex) {
			log.error("JdbcStudentDao.getStudent: " + ex.getMessage());
		} finally {
			try {
				if (pstmt != null && pstmt.isClosed()) {
					pstmt.close();
				}
			} catch (SQLException e) {
				e.printStackTrace();
				log.error(e.getMessage());
			}
		}
		return null;
	}

	@Override
	public void updateStudent(Student student) {
		PreparedStatement pstmt = null;
		try {
			Connection con = DriverManager.getConnection(URL, USER, PASSWORD);
			pstmt = con.prepareStatement(UPDATE_STUDENT_QUERY);
			String validName = student.getName().replaceAll(
					ValidationUtil.TRIM_REGEXP, "");
			pstmt.setString(1, validName);
			pstmt.setInt(2, student.getAge());
			pstmt.setDouble(3, student.getAverageMark());
			pstmt.setInt(4, student.getId());
			pstmt.execute();
		} catch (Exception ex) {
			log.error("JdbcStudentDao.updateStudent: " + ex.getMessage());
		} finally {
			try {
				if (pstmt != null && pstmt.isClosed()) {
					pstmt.close();
				}
			} catch (SQLException e) {
				e.printStackTrace();
				log.error(e.getMessage());
			}
		}
	}

	@Override
	public void deleteStudent(int id) {
		PreparedStatement pstmt = null;
		try {
			Connection con = DriverManager.getConnection(URL, USER, PASSWORD);
			pstmt = con.prepareStatement(DELETE_STUDENT_QUERY);
			pstmt.setInt(1, id);
			pstmt.execute();
		} catch (Exception ex) {
			log.error("JdbcStudentDao.deleteStudent: " + ex.getMessage());
		} finally {
			try {
				if (pstmt != null && pstmt.isClosed()) {
					pstmt.close();
				}
			} catch (SQLException e) {
				e.printStackTrace();
				log.error(e.getMessage());
			}
		}
	}

	@Override
	public List<Student> getStudents() {
		ArrayList<Student> students = new ArrayList<Student>();
		Statement stmt = null;
		try {
			Connection con = DriverManager.getConnection(URL, USER, PASSWORD);
			stmt = con.createStatement();
			stmt.execute(SELECT_STUDENTS_QUERY);
			ResultSet res = stmt.getResultSet();
			while (res.next()) {
				int id = res.getInt("ID");
				double avgMark = res.getDouble("AVGMARK");
				String name = res.getString("NAME");
				int age = res.getInt("AGE");
				Student student = new Student(id, avgMark, name, age);
				students.add(student);
			}
			return students;
		} catch (Exception ex) {
			log.error("JdbcStudentDao.getStudents: " + ex.getMessage());
		} finally {
			try {
				if (stmt != null && stmt.isClosed()) {
					stmt.close();
				}
			} catch (Exception e) {
				e.printStackTrace();
				log.error(e.getMessage());
			}
		}

		return null;
	}
}
