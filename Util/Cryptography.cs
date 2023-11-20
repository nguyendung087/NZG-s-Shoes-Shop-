using System.Security.Cryptography;
using System.Text;

namespace QuanLyBanGiay.Util
{
    public class Cryptography
    {
        //Tạo lớp Cryptography gồm 2 phương thức GetHash (để tạo mật khẩu) và VerifyHash
        //(để kiểm tra mật khẩu có khớp với mã băm hay không)
        
        //Compute hash the string
        public static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {
            //Chuyển đầu vào từ string thành byte và compute the hash
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            //Tạo StringBuilder để thu nhập bytes và tạo một chuỗi 
            var sBuilder = new StringBuilder();

            //Lặp từng byte của dữ liệu được mã hóa và format each one as a hexademical string
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            //Return the hexademical string 
            return sBuilder.ToString();
        }

        //Verify a hash against a string
        public static bool VerifyHash(HashAlgorithm hashAlgorithm, string input, string hash)
        {
            //Hash the input 
            var hashOfInput = GetHash(hashAlgorithm, input);

            //Create a StringComparer a compare the hashes
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return comparer.Compare(hashOfInput, hash) == 0;
        }
    }
}
