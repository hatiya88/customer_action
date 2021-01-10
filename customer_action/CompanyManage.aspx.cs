using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace customer_action
{
    public partial class CompanyManage : System.Web.UI.Page
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

        protected void InsertButton_Click(object sender, EventArgs e)
        {
            // 新しい会社IDを取得する（最大値+1）
            int newCompanyID = GetNewCompanyID();

            if (newCompanyID == -1)
            {
                // 会社IDの取得に失敗したとき
                MessageLabel.Text = "会社IDの取得に失敗しました。データベースを確認してください。";
                return;
            }

            // 新規行挿入用のSQLステートメントを定義する
            string queryString = "INSERT INTO tbl_company (companyID, company_name, delete_flag)" + 
                " VALUES (" + newCompanyID + ", '（新規）', 0)";

            try
            {
                // 接続文字列を取得する
                string connectionString = System.Configuration.ConfigurationManager.
                  ConnectionStrings["customer_actionConnectionString"].ConnectionString;

                // コネクションを定義する
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // コマンドを定義する
                    SqlCommand command = new SqlCommand(queryString, connection);

                    // コネクションを開く
                    connection.Open();

                    // コマンドに定義したSQLステートメントを実行する
                    command.ExecuteNonQuery();

                    // グリッドビューを再バインドしてデータを読み込み直す
                    GridView1.DataBind();

                    // 結果のメッセージを表示する
                    MessageLabel.Text = "新しい会社を追加しました。";
                }
            }
            catch (Exception ex)
            {
                // エラーが発生したとき
                MessageLabel.Text = "エラーが発生したため、処理を中止します。<br />" + ex.Message;
            }
        }

        private int GetNewCompanyID()
        {
            // 戻り値用の変数を定義する（-1は失敗したときの値として設定）
            int ret = -1;
            try
            {
                // 接続文字列を取得する
                string connectionString = System.Configuration.ConfigurationManager.
                  ConnectionStrings["customer_actionConnectionString"].ConnectionString;

                // コネクションを定義する
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // SQLステートメントを定義する（現在の会社IDの最大値+1を取得）
                    string queryString = "SELECT ISNULL(MAX(companyID), 0)+1 FROM tbl_company";

                    // コマンドを定義する
                    SqlCommand command = new SqlCommand(queryString, connection);

                    // コネクションを開く
                    connection.Open();

                    // SQLステートメントの実行結果を取得する
                    Object result = command.ExecuteScalar();

                    // 結果を正しく取得できたときには、戻り値を設定する
                    if (result != null)
                    {
                        ret = Convert.ToInt32(result.ToString());
                    }
                }
            }
            catch (Exception)
            {
                // 何らかのエラーが発生した
            }
            // 新規の会社IDを返す
            return ret;
        }

        // グリッドビューのレコード更新前に会社名の入力をチェックする
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (e.NewValues["company_name"] == null)
            {
                MessageLabel.Text = "会社名は必須入力です";
                e.Cancel = true;
            }
        }
    }
}