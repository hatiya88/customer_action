using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace customer_action
{
    public partial class StaffManage : System.Web.UI.Page
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

        protected void InsertButton_Click(object sender, EventArgs e)
        {
            // 新しいスタッフIDを取得する（最大値+1）
            int newStaffID = GetNewStaffID();

            if (newStaffID == -1)
            {
                // スタッフIDの取得に失敗したとき
                MessageLabel.Text = "スタッフIDの取得に失敗しました。データベースを確認してください。";
                return;
            }

            // 新規行挿入用のSQLステートメントを定義する
            string queryString = "INSERT INTO tbl_staff" +
                " (staffID, staff_name, userID, password, admin_flag, delete_flag)" +
                " VALUES (" + newStaffID + ", '（新規）', '', '', 0, 0)";

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
                    MessageLabel.Text = "新しいスタッフを追加しました。";
                }
            }
            catch (Exception ex)
            {
                // エラーが発生したとき
                MessageLabel.Text = "エラーが発生したため、処理を中止します。<br />" + ex.Message;
            }
        }

        private int GetNewStaffID()
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
                    // SQLステートメントを定義する（現在のスタッフIDの最大値+1を取得）
                    string queryString = "SELECT ISNULL(MAX(staffID), 0)+1 FROM tbl_staff";
                
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
            // 新規のスタッフIDを返す
            return ret;
        }

        // グリッドビューのレコード更新前にスタッフ名の入力をチェックする
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
           if (e.NewValues["staff_name"] == null)
            {
                MessageLabel.Text = "スタッフ名は必須入力です";
                e.Cancel = true;
            }
        }
    }
}