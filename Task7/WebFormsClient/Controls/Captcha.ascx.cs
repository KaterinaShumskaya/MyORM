using System;

using WebFormsClient.Common;

public delegate void CaptchaEventHandler();

/// <summary>
/// Операции с БД.
/// </summary>
public enum Actions
{
    /// <summary>
    /// Удаление.
    /// </summary>
    Delete,

    /// <summary>
    /// Добавление.
    /// </summary>
    Add,

    /// <summary>
    /// Редактирование.
    /// </summary>
    Edit
}

public partial class CaptchaControl : System.Web.UI.UserControl
{
    private string color = "#ffffff";
    protected string style;
    private event CaptchaEventHandler success;
    private event CaptchaEventHandler failure;

    private Actions _action;

    public Actions Action
    {
        get
        {
            return _action;
        }
    }

    public void SetDelBtnEnabled(bool isEnabled)
    {
        btnDelete.Visible = isEnabled;
    }

    public void SetUpdateBtnEnabled(bool isEnabled)
    {
        btnDelete.Visible = isEnabled;
    }

    public string Message
    {
        // We don't set message in page_load, because it prevents us from changing message in failure event
        set { lblCMessage.Text = value; }
        get { return lblCMessage.Text;  }
    }

    public string BackgroundColor
    {
        set { color = value.Trim("#".ToCharArray()); }
        get { return color; }
    }

    public string Style
    {
        set { style = value; }
        get { return style; }
    }

    public event CaptchaEventHandler Success
    {
        add { this.success += value; }
        remove { this.success += null; }
    }

    public event CaptchaEventHandler Failure
    {
        add { failure += value; }
        remove { failure += null; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetCaptcha();
            txtCaptcha.Text = "";
        }
    }

    private void SetCaptcha()
    {
        // Set image
        string s = new Captcha().GenerateSolution(null);

        // Save to session
        Session["captcha"] = s.ToLower();
      

        /* if (context.Session["Captcha"].ToString() != null)
         {
             strCaptcha = context.Session["Captcha"].ToString();
         }*/

        imgCaptcha.ImageUrl = "~/Handlers/CaptchaHandler.ashx?c=" + s;
    }

    private void CheckCaptcha()
    {
        if (Session["captcha"] != null && txtCaptcha.Text.ToLower() == Session["captcha"].ToString())
        {
            if (this.success != null)
            {
                this.success();
            }
        }
        else
        {
            if (failure != null)
            {
                failure();
            }
        }
        txtCaptcha.Text = "";
        SetCaptcha();
    }

    protected void btnAdd_Click(object s, EventArgs e)
    {
        _action = Actions.Add;
        this.CheckCaptcha();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        _action = Actions.Delete;
        this.CheckCaptcha();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        _action = Actions.Edit;
        this.CheckCaptcha();
    }
}