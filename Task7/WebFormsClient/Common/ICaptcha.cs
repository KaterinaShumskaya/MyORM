using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFormsClient.Common
{
    using System.IO;

    public interface ICaptcha
    {
        string GenerateSolution(string allowedChars);

        Stream GenerateImage(string solution);
    }
}