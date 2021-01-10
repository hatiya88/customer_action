using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace customer_action
{
    public partial class Menu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 管理者権限によって、パネルの表示を切り替える

            // ユーザーレベルによる処理の分岐
            switch (GetUserLevel())
            {
                case 1:
                    // 管理者のとき
                    AdminPanel.Visible = true;
                    UserPanel.Visible = false;
                    break;
                case 2:
                    // 一般ユーザーのとき
                    AdminPanel.Visible = false;
                    UserPanel.Visible = true;
                    break;
                default:
                    // その他（ログオンされていないときなど）
                    // ユーザーが認証されていないため、[ログオン]フォームに戻る
                    Response.Redirect("Logon.aspx");
                    break;
            }

            // このWebページをキャッシュしないように設定する
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
        }

        private int GetUserLevel()
        {
            // ユーザーレベルを取得する
            // 管理者＝1、一般ユーザー＝2、その他（ログオンされていないときなど）＝0
            
            // セッション変数から取得した値の判定
            if (Session["AdminFlag"] == null)
            {
                // セッション変数の値が存在しない場合
                return 0;
            }
            else if (Convert.ToBoolean(Session["AdminFlag"]))
            {
                // セッション変数AdminFlagがtrueの場合
                return 1;
            }
            else
            {
                // セッション変数AdminFlagがfalseの場合
                return 2;
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            // セッション変数をクリアする
            Session.Clear();
            // ［ログオン］フォームに遷移する
            Response.Redirect("Logon.aspx");
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            // セッション変数をクリアする
            Session.Clear();
            // ［ログオン］フォームに遷移する
            Response.Redirect("Logon.aspx");
        }
    }
}