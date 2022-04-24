using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SkoBingo
{
    public class LinkGenerator
    {
        public static string GetUniqueLink(int size)
        {
            byte[] data = new byte[4*size];
            char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            using (var crypto = RandomNumberGenerator.Create())
            {
                crypto.GetBytes(data);
            }
            StringBuilder uniqueLink = new(4*size);
            for (int i = 0; i < 10; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % chars.Length;

                uniqueLink.Append(chars[idx]);
            }

            return uniqueLink.ToString();
        }
    }
}
