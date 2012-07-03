using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Security.Cryptography;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
/// <summary>
/// Summary description for DESEncrypt
/// </summary>
public class DESEncrypt
{
    private string iv = "12345678";
    private string key = "12345678";
    private Encoding encoding = new UnicodeEncoding();
    private DES des;

    public DESEncrypt()
    {
        des = new DESCryptoServiceProvider();
    }

    /// <summary>
    /// 设置加密密钥
    /// </summary>
    public string EncryptKey
    {
        get { return this.key; }
        set
        {
            this.key = value;
        }        
    }

    /// <summary>
    /// 要加密字符的编码模式
    /// </summary>
    public Encoding EncodingMode
    {
        get { return this.encoding; }
        set { this.encoding = value; }
    }

    /// <summary>
    /// 加密字符串并返回加密后的结果
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public string EncryptString(string str)
    {
        byte[] ivb = Encoding.ASCII.GetBytes(this.iv);
        byte[] keyb = Encoding.ASCII.GetBytes(this.EncryptKey);//得到加密密钥
        byte[] toEncrypt = this.EncodingMode.GetBytes(str);//得到要加密的内容
        byte[] encrypted;
        ICryptoTransform encryptor = des.CreateEncryptor(keyb, ivb);
        MemoryStream msEncrypt = new MemoryStream();
        CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
        csEncrypt.Write(toEncrypt, 0, toEncrypt.Length);
        csEncrypt.FlushFinalBlock();
        encrypted = msEncrypt.ToArray();
        csEncrypt.Close();
        msEncrypt.Close();
        return this.EncodingMode.GetString(encrypted);
    }
}