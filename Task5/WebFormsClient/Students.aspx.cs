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

        private IList<Student> _students;

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
            _students = _studentAccessor.GetAll();
            DropDownList1.SelectedIndex = 0;
        }

        protected void ChangeSelection(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public IEnumerable GetStudentGroups()
        {
            return _studentGroups.Select(x=>x.Name);
        }

        public void DeleteStudent(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void UpdateStudent(object sender, EventArgs e)
        {
            DetailsView1.ChangeMode(DetailsViewMode.Edit);
        }

        protected void AddStudent(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public IEnumerable GetStudents()
        {

            return _students;
        }

        protected void SelectStudent(object sender, EventArgs e)
        {
           this.SelectSt();
            DetailsView1.DataBind();
        }

        public object SelectSt()
        {
            if (GridView3.SelectedValue != null)
            {
                var id = (int)GridView3.SelectedValue;
                return _students.SingleOrDefault(x => x.Id == id);
            }

            return null;
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void InsertStudent()
        {
            var lastName = ((TextBox)DetailsView1.Rows[0].Cells[1].Controls[0]).Text;
            var firstName = ((TextBox)DetailsView1.Rows[1].Cells[1].Controls[0]).Text;
            var middleName = ((TextBox)DetailsView1.Rows[2].Cells[1].Controls[0]).Text;
            var birthday = DateTime.Parse(((TextBox)DetailsView1.Rows[3].Cells[1].Controls[0]).Text);
            var student = new Student(firstName, lastName, middleName, birthday, 13);
            _students.Add(student);
            _studentAccessor.Insert(student);
            GridView3.DataBind();

        }

        protected void InsertSt(object sender, DetailsViewInsertedEventArgs e)
        {
           
        }
    }
}