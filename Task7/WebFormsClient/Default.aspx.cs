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

        private int _selectedIndex;
 
        public _Default(IDataAccessor<StudentGroup> studentGroupAccessor)
        {
            _groupAccessor = studentGroupAccessor;
        }

        protected _Default()
        {
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            EnableViewState = true;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            _studentGroups = this._groupAccessor.GetAll();
            GridView1.SelectedIndex = ViewState["SelectedGroup"] == null ? -1 : (int)ViewState["SelectedGroup"];
            _selectedIndex = GridView1.SelectedIndex;
        }

        public IEnumerable GetStudentGroups()
        {
            return _studentGroups;
        }

        private void Init()
        {
            if (GridView1.SelectedValue != null)
            {
                var id = (int)GridView1.SelectedValue;
                NameTextBox.Text = _groupAccessor.GetById(id).Name;
                _selectedIndex = GridView1.SelectedIndex;
                Captcha1.
                Session["IsRowSelected"] = true;
            }
            else
            {
                NameTextBox.Text = "";
                Session["IsRowSelected"] = false;
            }
        }

        private void SelectGroup()
        {
            if (GridView1.SelectedIndex == _selectedIndex)
            {
                GridView1.SelectedIndex = -1;
            }
            this.Init();
            ViewState["SelectedGroup"] = GridView1.SelectedIndex;
        }

        protected void Select(object sender, EventArgs e)
        {
            this.SelectGroup();
        }

        public void DeleteGroup()
        {
            var id = (int)GridView1.SelectedValue;
            _studentGroups.Remove(_studentGroups.Single(x => x.Id == id));
            _groupAccessor.DeleteById(id);
            GridView1.SelectedIndex = -1;
            NameTextBox.Text = "";
            GridView1.DataBind();
        }

        protected void AddGroup()
        {
            var studentGroup = new StudentGroup(NameTextBox.Text);

            studentGroup.Id = _groupAccessor.Insert(studentGroup);

            _studentGroups.Add(studentGroup);
            NameTextBox.Text = "";
            GridView1.SelectedIndex = -1;
            GridView1.DataBind();
        }

        public void UpdateGroup()
        {
            throw new NotImplementedException();
        }

        protected void OnSuccess()
        {
            if (Captcha1.Action == Actions.Add)
            {
                this.AddGroup();
            }
            else if (Captcha1.Action == Actions.Delete)
            {
                this.DeleteGroup();
            }
            else
            {
                this.UpdateGroup();
            }
            Captcha1.Message = "Операция выполнена успешно";
        }
        protected void OnFailure()
        {
            Captcha1.Message = "Введён неверный текст";
        }
    }
}