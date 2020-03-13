
/**************************************************************************
 * Copyright: Copyright 2020 Cofradinn, LLC. All Rights reserved.
 * Module: TeamMebers
 * ScriptType: Element
 * Created by: Andrés Romero, andresraulrg@gmail.com
 * Created on: viernes, 7 de febrero de 2020
 * Description: ...Add any description
 * Notes: ...Add any note
 **************************************************************************/

using UnityEngine;
using UnityEngine.UI;

namespace Cofradinn.Modules.Gui.TeamMembers
{
    public class TeamMemberItem : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Text _txtFileName;
        [SerializeField] private Text _txtCreateAt;

        public void __SetData(int id, TeamMemberData fileInfo)
        {
            _txtFileName.text = fileInfo._fieldName;
            _txtCreateAt.text = fileInfo._key;
        }
    }
}

/**************************************************************************
 * Copyright: Copyright 2020 Cofradinn, LLC. All Rights reserved.
 * Module: TeamMebers
 * ScriptType: Data
 * Created by: Andrés Romero, andresraulrg@gmail.com
 * Created on: Friday, January 17, 2020
 * Description: ...Add any description
 * Notes: ...Add any note
 **************************************************************************/

//using System;
//using UnityEngine;

namespace Cofradinn.Modules.Gui.TeamMembers
{
    [System.Serializable]
    public class TeamMemberData
    {
        public string _key;
        public string _fieldName;

        public TeamMemberData() { }
        public TeamMemberData(string name, string key)
        {
            _key = key;
            _fieldName = name;
        }
    }
}