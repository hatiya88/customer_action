using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace customer_action
{
    public partial class ActionEdit : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["StaffID"] == null)
            {
                // ユーザーが認証されていないため、［ログオン］フォームに戻る
                Response.Redirect("Logon.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // このWebページをキャッシュしないように設定する
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
        }

        protected void FormView1_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
            // ［顧客情報］フォームに戻る
            Response.Redirect("~/CustomerCard.aspx?id=" + Request.QueryString["customerid"]);
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            // ［顧客情報］フォームに戻る
            Response.Redirect("~/CustomerCard.aspx?id=" + Request.QueryString["customerid"]);
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            // 日付の判定のカスタムエラー
            args.IsValid = false;

            DateTime d;
            if (DateTime.TryParse((string)args.Value, out d))
            {
                if (d.Year >= 1753 && d.Year <= 9999)
                {
                    args.IsValid = true;
                }
            }
        }
    }
}