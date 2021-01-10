using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace customer_action
{
    public partial class StaffReplace : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Convert.ToBoolean(Session["AdminFlag"]))
            {
                // セッション変数をクリアする
                Session.Clear();
                // 管理者として認証されていないため、［ログオン］フォームに戻る
                Response.Redirect("Logon.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // このWebページをキャッシュしないように設定する
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
        }

        protected void ExecuteButton_Click(object sender, EventArgs e)
        {
            // 変更前の担当者を表す変数の定義と値のセット
            string beforeID = BeforeStaffDropDownList.SelectedValue;
            // 変更後の担当者を表す変数の定義と値のセット
            string afterID = AfterStaffDropDownList.SelectedValue;

            // 変更前と変更後の担当者が選択されているかどうかを確認する
            if (beforeID == "-1" || afterID == "-1")
            {
                // 担当者が選択されていない場合には、メッセージを表示して処理を終了する
                MessageLabel.Text = "変更前と変更後の担当者を選択してください。";
                MessageLabel.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // 変更前と変更後の担当者が同じであるかどうかを確認する
            if (beforeID == afterID)
            {
                // 同じ担当者を選択している場合には、メッセージを表示して処理を終了する
                MessageLabel.Text = "変更前と変更後の担当者が同じであるため、処理を実行できません。";
                MessageLabel.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                // 接続文字列を取得する
                string connectionString = System.Configuration.ConfigurationManager.
                    ConnectionStrings["customer_actionConnectionString"].ConnectionString;
                
                // コネクションを定義する
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // コマンドを定義する
                    SqlCommand command = connection.CreateCommand();
                
                    // コネクションを開く
                    connection.Open();
                    
                    // コマンドにSQLステートメントを指定する
                    command.CommandText = "UPDATE tbl_customer SET staffID=" + afterID + " WHERE staffID=" + beforeID;
                    
                    // コマンドに定義したSQLステートメントを実行して、処理結果の件数を取得する
                    int lines = command.ExecuteNonQuery();
                    
                    // ラベルに結果のメッセージを表示する
                    MessageLabel.Text = lines + "件の顧客で担当者を変更しました。";
                }
            }
            catch (Exception ex)
            {
                // 不明なエラーが発生したとき
                MessageLabel.Text = "エラーが発生したため、処理を中止します。<br>" + ex.Message;
            }
        }
    }
}