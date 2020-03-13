/**************************************************************************
 * Copyright: Copyright 2019 Cofradinn, LLC. All Rights reserved.
 * Module: SettingsPanel
 * ScriptType: Helper
 * Created by: Andrés Romero,andresraulrg@gmail.com
 * Created on: 27/12/19
 * Description: ...Add any description
 * Notes: ...Add any note
 **************************************************************************/

namespace Cofradinn.Utilities.ScriptTemplates
{
    public static class KeywordsReplacer
    {
        public static string __Replace(string text, string scriptName)
        {
            string newText = text;
            newText = newText.Replace(TemplateKeyWords.COMPANY, AuthorData.COMPANY);
            newText = newText.Replace(TemplateKeyWords.AUTHOR, AuthorData.AUTHOR);
            newText = newText.Replace(TemplateKeyWords.EMAIL, AuthorData.EMAIL);
            newText = newText.Replace(TemplateKeyWords.SCRIPT_NAME, scriptName);
            newText = newText.Replace(TemplateKeyWords.CREATION_YEAR, AuthorData.CREATION_YEAR);
            newText = newText.Replace(TemplateKeyWords.CREATION_DATE, AuthorData.CREATION_DATE);

            //Debug.Log("newText: " + newText);
            return newText;
        }
    }
}