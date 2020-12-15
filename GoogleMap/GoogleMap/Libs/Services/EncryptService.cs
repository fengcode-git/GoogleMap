using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FengCode.Libs.Utils.Security;
using FengCode.Libs.Utils.Text;
using GoogleMap.Libs.Utils;

namespace GoogleMap.Libs.Services
{
    /// <summary>
    /// 加密服务
    /// </summary>
    public class EncryptService
    {
        /// <summary>
        /// 生成密码哈希值
        /// </summary>
        public string PasswordHash(string password)
        {
            return PasswordEncryptor.Hash(password);
        }

        /// <summary>
        /// 每次运行都生成一个新的HASH值
        /// </summary>
        public string CreateNewHash()
        {
            return Md5Encryptor.Hash(Guid.NewGuid().ToString());
        }
    }
}
