using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private static List<StudentData> studentList = new List<StudentData>()
        {
                new StudentData{
                    No= 671,
                    name="aylin",
                    surname="açıkgöz",
                    classLevel=5
                },
                new StudentData{
                    No= 2,
                    name="emre",
                    surname="kara",
                    classLevel=3
                },
                new StudentData{
                    No= 30,
                    name="ayşe",
                    surname="canan",
                    classLevel=1
                },
                new StudentData{
                    No= 534,
                    name="rojda",
                    surname="alagöl",
                    classLevel=4
                },
                new StudentData{
                    No= 65,
                    name="baran",
                    surname="erdoğmuş",
                    classLevel=2
                }
        };

        [HttpGet("getAll")]
        public List<StudentData> get1()
        {
            var students = studentList.OrderBy(x => x.No).ToList<StudentData>();
            return students;
        }

        [HttpGet("{No}")]             //fromRoute
        public StudentData getByNo(int No)
        {
            var students = studentList.Where(students => students.No == No).SingleOrDefault();
            return students;
        }

        [HttpGet]        //fromQuery
        [Route("getByNo")]
        public StudentData get2([FromQuery] int No)
        {
            var students = studentList.Where(StudentData => StudentData.No == No).SingleOrDefault();
            return students;
        }

        [HttpGet]      //isme göre listeleme
        [Route("listing")]
        public StudentData list([FromQuery] string Name)
        {
            var student=studentList.Where(x => x.name==Name).SingleOrDefault();
            return student;
        }

        [HttpGet]     //azalan düzende sıralama
        [Route("Arrangement")]
        public List<StudentData> getArrangement(int No) 
        {
            var studentAr=studentList.OrderByDescending(x=>x.No).ToList<StudentData>();
            return studentAr;
        }

        [HttpPut("{No}")]

        public IActionResult updateStudent(int No, [FromBody] StudentData updatedStudent)
        {
            var student = studentList.SingleOrDefault(x => x.No == No);
            if (student is null)
                return BadRequest();

            student.No = updatedStudent.No != default ? updatedStudent.No : student.No;
            student.name = updatedStudent.name != default ? updatedStudent.name : student.name;
            student.surname = updatedStudent.surname != default ? updatedStudent.surname : student.surname;
            student.classLevel = updatedStudent.classLevel != default ? updatedStudent.classLevel : student.classLevel;
            return Ok();
        }


        [HttpDelete("{No}")]

        public IActionResult delStudent(int No)
        {
            var student = studentList.SingleOrDefault(x => x.No == No);
            if (student == null)
                return BadRequest();

            studentList.Remove(student);
            return Ok();
        }

        [HttpPost]
        public IActionResult AddStudent([FromBody] StudentData newStudentData)
        {
            var Student = studentList.SingleOrDefault(x => x.No == newStudentData.No);
            if (Student != null)
                return BadRequest();


            studentList.Add(newStudentData);
            return Ok();
        }

        [HttpPatch("{No}")]
        public IActionResult Patch(int No, StudentData newStudentData)
        {
            var student = studentList.SingleOrDefault(x => x.No == No);
            if (student is null)
                return BadRequest();

            student.No = newStudentData.No != default ? newStudentData.No : student.No;
            student.name = newStudentData.name != default ? newStudentData.name : student.name;
            student.surname = newStudentData.surname != default ? newStudentData.surname : student.surname;
            student.classLevel = newStudentData.classLevel != default ? newStudentData.classLevel : student.classLevel;
            return Ok();
        }
    }
}

