using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormsClient
{
    using System.Collections;

    using Persons.DataAccessors;

    using WebFormsClient.Model;

    public partial class Students : Page
    {
        private IDataAccessor<StudentGroup> _groupAccessor;

        private IList<StudentGroup> _studentGroups;

        private IDataAccessor<Student> _studentAccessor;

        private IList<Student> _students, _allStudents;

        public Students(IDataAccessor<Student> studentAccessor, IDataAccessor<StudentGroup> studentGroupAccessor)
        {
            _groupAccessor = studentGroupAccessor;
            _studentAccessor = studentAccessor;
        }

        protected Students()
        {
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _studentGroups = this._groupAccessor.GetAll();
            _allStudents = _studentAccessor.GetAll();
            
            if (!IsPostBack)
            {             
                DropDownList1.DataSource = _studentGroups;
                DropDownList1.DataTextField = "Name";
                DropDownList1.DataValueField = "Id";
                DropDownList1.DataBind();             
            }
            _students = _allStudents.Where(x => x.StudentGroupId == int.Parse(DropDownList1.SelectedValue)).ToList();
        }

        protected void ChangeSelection(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void DeleteStudent(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void UpdateStudent(object sender, EventArgs e)
        {
   
        }

        protected void AddStudent(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _students = _allStudents.Where(x => x.StudentGroupId == int.Parse(DropDownList1.SelectedValue)).ToList();
            GridView3.DataBind();
        }

        public IEnumerable GetStudents()
        {

            return _students;
        }

        protected void SelectStudent(object sender, EventArgs e)
        {
            if (GridView3.SelectedValue != null)
            {
                var id = (int)GridView3.SelectedValue;
                var selectedStudent = _students.SingleOrDefault(x => x.Id == id);
                tbLastName.Text = selectedStudent.LastName;
                tbFirstName.Text = selectedStudent.FirstName;
                tbMiddleName.Text = selectedStudent.MiddleName;
                tbBirthday.Text = selectedStudent.DateOfBirth.ToString();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void InsertStudent()
        {
            var lastName = tbLastName.Text;
            var firstName = tbFirstName.Text;
            var middleName = tbMiddleName.Text;
            var birthday = DateTime.Parse(tbBirthday.Text);
            var student = new Student(firstName, lastName, middleName, birthday, int.Parse(DropDownList1.SelectedValue));
            _students.Add(student);
            _studentAccessor.Insert(student);
            GridView3.DataBind();

        }

        protected void OnSuccess()
        {
            if (captcha.Action == Actions.Add)
            {
                this.InsertStudent();
            }
            else if (captcha.Action == Actions.Delete)
            {
               // this.DeleteGroup();
            }
            else
            {
                //.UpdateGroup();
            }
            captcha.Message = "Операция выполнена успешно";
        }

        protected void OnFailure()
        {
            captcha.Message = "Введён неверный текст"; ;
        }
    }
}