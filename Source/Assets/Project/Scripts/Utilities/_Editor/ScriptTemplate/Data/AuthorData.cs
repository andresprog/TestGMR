/**************************************************************************
 * Copyright: Copyright 2019 Cofradinn, LLC. All Rights reserved.
 * Module: ScriptTemplates
 * ScriptType: Data
 * Created by: Andrés Romero,andresraulrg@gmail.com
 * Created on: 27/12/19
 * Description: ...Add any description
 * Notes: ...Add any note
 **************************************************************************/

using System;

namespace Cofradinn.Utilities.ScriptTemplates
{
    public static class AuthorData
    {
        public static string COMPANY = "Cofradinn";
        public static string AUTHOR = "Andrés Romero";
        public static string EMAIL = "andresraulrg@gmail.com";
        public static string CREATION_DATE => string.Format("{0:D}", DateTime.Now);
        public static string CREATION_YEAR => DateTime.Now.Year.ToString();
    }
}