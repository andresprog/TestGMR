/**************************************************************************
 * Module: TeamMembers
 * ScriptType: Controller
 * Created by: Andrés Romero, andresraulrg@gmail.com
 * Created on: viernes, 7 de febrero de 2020
 * Description: ...Add any description
 * Notes: ...Add any note
 **************************************************************************/

using UnityEngine;
using Cofradinn.Modules.Gui.TeamMembers;
using System.Collections.Generic;
using Cofradinn.Modules.Feedback;
using Cofradinn.Data.Gui.Loading.Utilities;
using HandlerTag = Cofradinn.Constants.HandlerTag;
using System.Linq;
using Cofradinn.Systems;

namespace Cofradinn.Controllers.TeamMembers
{
    /// <summary>
    /// Team members controller, this script handles the module logic
    /// </summary>
    public class TeamMembersController : Controller
    {
        [Header("Json Data")]
        [SerializeField] private TeamMembersJsonData _teamMembersJsonData;

        private ITeamMembersHandler _iTeamMembersHandler;
        private List<TeamMemberData> _elements;

        protected override void __OnAwake()
        {
        }
        private void Start()
        {
            _iTeamMembersHandler.__EnableView(true);

            __LoadFileAndRefreshView();


            _iTeamMembersHandler._OnPanelEvent += __OnModuleEvent;
        }
        protected override void __FindReferences()
        {
            // Finding modules references
            _iTeamMembersHandler = __FindComponent<ITeamMembersHandler>(HandlerTag.TEAM_MEMBERS_HANDLER);
        }

        private void __EnableView(bool open) => _iTeamMembersHandler.__EnableView(open);
        private void __OnModuleEvent(TeamMembersEventName eventName)
        {
            switch (eventName)
            {
                case TeamMembersEventName.Refresh:
                    LoadingImageSystem._Instance.__ShowSpinner(true);
                    __LoadFileAndRefreshView();
                    LoadingImageSystem._Instance.__ShowSpinner(false);
                    break;

                case TeamMembersEventName.Exit:
                    AppSystem._Instance.__Quit();
                    break;
                default:
                    break;
            }
        }
        private void __LoadFileAndRefreshView()
        {
            string json = JsonLoaderHelper.__LoadJsonFile("JsonChallenge.json");

            if (string.IsNullOrEmpty(json))
                FeedbackSystem._Instance.__SendFeedback("Json file not found or is empty", FeedbackType.Error);
            else
            {
                _teamMembersJsonData = JsonDeserializerHelper.__FromJson<TeamMembersJsonData>(json);
                __ValidateKeys();

                _iTeamMembersHandler.__ClearTableData();

                if (_teamMembersJsonData == null)
                {
                    FeedbackSystem._Instance.__SendFeedback("The json file is null", FeedbackType.Error);
                    return;
                }

                _iTeamMembersHandler.__SetTitle(_teamMembersJsonData.Title);

                if (_teamMembersJsonData.ColumnHeaders == null)
                    FeedbackSystem._Instance.__SendFeedback("The team member headers are null", FeedbackType.Error);
                else
                    _iTeamMembersHandler.__SetHeaders(_teamMembersJsonData.ColumnHeaders);

                if (_teamMembersJsonData.Data == null)
                    FeedbackSystem._Instance.__SendFeedback("The team member data is null", FeedbackType.Error);
                else
                    _iTeamMembersHandler.__SetData(_teamMembersJsonData.Data);
            }

#if UNITY_EDITOR
            __DebugDictionaryForTest();
#endif
        }
        private void __ValidateKeys()
        {
            if (_teamMembersJsonData.ColumnHeaders.Count <= 0)
            {
                FeedbackSystem._Instance.__SendFeedback("Error: dont found headers", FeedbackType.Error);
                return;
            }

            if (_teamMembersJsonData.ColumnHeaders.Count > 1)
            {
                int sourceCount = _teamMembersJsonData.ColumnHeaders.Count;
                _teamMembersJsonData.ColumnHeaders = _teamMembersJsonData.ColumnHeaders.Distinct().ToList();

                if (_teamMembersJsonData.ColumnHeaders.Count != sourceCount) FeedbackSystem._Instance.__SendFeedback("Warning: some headers were repeating, duplicate records were removed", FeedbackType.Error);
            }
        }
        private void __DebugDictionaryForTest()
        {
            if (_teamMembersJsonData == null) { Debug.LogError("Null Error: _teamMembersJsonData is null"); return; }

            foreach (var dict in _teamMembersJsonData.Data)
            {
                foreach (var item in dict)
                {
                    Debug.Log("key: " + item.Key + ", val: " + item.Value);
                }
            }
        }
    }
}
