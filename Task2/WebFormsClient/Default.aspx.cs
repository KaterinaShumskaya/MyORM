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

    public partial class _Default : Page
    {
        private IDataAccessor<StudentGroup> _groupAccessor;

        private IList<StudentGroup> _studentGroups;

        private IDataAccessor<Student> _studentAccessor;

        private IList<Student> _students;

        private int _selectedIndex;
 
        protected void Page_Load(object sender, EventArgs e)
        {
            _groupAccessor = new MyORM<StudentGroup>();
            _studentGroups = this._groupAccessor.GetAll();
            _studentAccessor = new MyORM<Student>();
            _students = _studentAccessor.GetAll();
            LinkButton2.Visible = false;
            _selectedIndex = GridView1.SelectedIndex;
        }

        public IEnumerable GetStudentGroups()
        {
            return _studentGroups;
        }

        public IEnumerable GetStudents()
        {
            return _students;
        }

        protected void ShowStudents(object sender, EventArgs e)
        {
            if (GridView1.SelectedIndex == _selectedIndex)
            {
                GridView1.SelectedIndex = -1;
            }

            if (GridView1.SelectedValue != null)
            {
                LinkButton1.Text = "Редактировать группу";
                LinkButton2.Visible = true;
                var id = (int)GridView1.SelectedValue;
                TextBox1.Text = _groupAccessor.GetById(id).Name;
                GridView2.DataSource = _students.Where(x => x.StudentGroupId == id).ToList();
                _selectedIndex = GridView1.SelectedIndex;
            }
            else
            {
                LinkButton1.Text = "Добавить группу";
                TextBox1.Text = "";
                LinkButton2.Visible = false;
            }

            GridView2.DataBind();
        }

        public void DeleteGroup(Object sender, EventArgs e)
        {
            var id = (int)GridView1.SelectedValue;
            _studentGroups.Remove(_studentGroups.Single(x => x.Id == id));
            _groupAccessor.DeleteById(id);
            GridView1.SelectedIndex = -1;
            LinkButton1.Text = "Добавить группу";
            TextBox1.Text = "";
            LinkButton2.Visible = false;
            GridView1.DataBind();
        }

        protected void AddGroup(object sender, EventArgs e)
        {
            if (TextBox1.Text == "")
            {
                ErrorLabel.Text = "Не задано название группы";
                return;
            }
            if(TextBox1.Text.Length > 10)
            {
                ErrorLabel.Text = "Название группы не должно превышать 10 символов";
                return;
            }
            
                ErrorLabel.Text = "";

            var studentGroup = new StudentGroup(TextBox1.Text);
            _studentGroups.Add(studentGroup);

            _groupAccessor.Insert(studentGroup);
            TextBox1.Text = "";
            GridView1.DataBind();
        }

        public void UpdateGroup(Object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        
    }
}