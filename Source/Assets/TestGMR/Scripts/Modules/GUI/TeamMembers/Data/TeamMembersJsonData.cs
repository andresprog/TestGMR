/**************************************************************************
 * Copyright: Copyright 2020 Cofradinn, LLC. All Rights reserved.
 * Module: TeamMebers
 * ScriptType: Data
 * Created by: Andrés Romero, andresraulrg@gmail.com
 * Created on: viernes, 7 de febrero de 2020
 * Description: ...Add any description
 * Notes: ...Add any note
 **************************************************************************/

using System;
using System.Collections.Generic;

namespace Cofradinn.Modules.Gui.TeamMembers
{
    [Serializable]
    public class TeamMembersJsonData
    {
        public string Title;
        public List<string> ColumnHeaders;
        public List<Dictionary<string, string>> Data;

        public TeamMembersJsonData()
        {
            Title = "";
            ColumnHeaders = new List<string>();
            Data = new List<Dictionary<string,string>>();
        }
    }
}