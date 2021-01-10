using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace customer_action
{
    public partial class CustomerList : System.Web.UI.Page
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

            // ［フィルター］ボタンをこのWebフォームの既定ボタンにする
            this.Form.DefaultButton = FilterButton.UniqueID;
        }

        protected void FilterButton_Click(object sender, EventArgs e)
        {
            // ［自分の顧客のみ］チェックボックスの判定
            if (MyCustomerCheckBox.Checked)
            {
                // オンのとき
                // グリッドビューのデータソースとしてSqlDataSouce2を設定する
                GridView1.DataSourceID = SqlDataSource2.ID;
            }
            else
            {
                // オフのとき
                // グリッドビューのデータソースとしてSqlDataSouce1を設定する
                GridView1.DataSourceID = SqlDataSource1.ID;
            }
        }
    }
}