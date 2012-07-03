using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;

/// <summary>
///PasswordUtil 的摘要说明
/// </summary>
public class PasswordUtil
{
    public static string EncryptPwd(String userName, String passWord)
    {
        string _returnString = string.Empty;
        try
        {
            //修改原数据
            string _sql = "SELECT  * FROM " + DataModelUtility.Table_Manager + " WHERE " + DataModelUtility.Field_Id + "='" + userName + "'";
            DataTable dt = DbHelperSQL.DoQueryEx("c", _sql, true);
            if (dt.Rows.Count == 0)
            {
                return "";//用户不存在
            }

            DataRow row = dt.Rows[0];
            //得到密匙
            string salt = row[DataModelUtility.Field_Salt].ToString();
            //验证密码是否正确
            if (EncryptPassWord.EncryptPwd(passWord, salt) == row[DataModelUtility.Field_Password].ToString())
            {
                //登录成功
                _returnString = "ok_check";
            }
        }
        catch (Exception)
        {

            _returnString = "error_check";
        }
        return _returnString;
    }

    public static string EditPassword(String userName, String newPassword)
    {
        string _returnString = string.Empty;
        try
        {
            //获得密匙
            string salt = EncryptPassWord.CreateSalt();
            //得到经过加密后的"密码"
            string password = EncryptPassWord.EncryptPwd(newPassword, salt);
            //修改原数据
            string _sql = "UPDATE " + DataModelUtility.Table_Manager + " SET  " + DataModelUtility.Field_Password + "='" + password+"'," + DataModelUtility.Field_Salt +"='" + salt + "' WHERE " + DataModelUtility.Field_Id + "='" + userName + "'";
            if (DbHelperSQL.ExecuteSql(_sql) > 0)
            {
                _returnString = "ok_edit";
            }
            else
            {
                _returnString = "error_edit";
            }
        }
        catch
        {
            _returnString = "error_edit";
        }
        return _returnString;
    }
}