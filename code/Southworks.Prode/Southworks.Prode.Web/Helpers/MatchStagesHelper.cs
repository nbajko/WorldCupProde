using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Southworks.Prode.Data.Models;

namespace Southworks.Prode.Web.Models
{
    public static class MatchStagesHelper
    {
        public static IEnumerable<SelectListItem> MatchStages = new List<SelectListItem>
        {
            new SelectListItem { Text = "Grupo A", Value = MatchStage.GroupA.ToString() },
            new SelectListItem { Text = "Grupo B", Value = MatchStage.GroupB.ToString() },
            new SelectListItem { Text = "Grupo C", Value = MatchStage.GroupC.ToString() },
            new SelectListItem { Text = "Grupo D", Value = MatchStage.GroupD.ToString() },
            new SelectListItem { Text = "Grupo E", Value = MatchStage.GroupE.ToString() },
            new SelectListItem { Text = "Grupo F", Value = MatchStage.GroupF.ToString() },
            new SelectListItem { Text = "Grupo G", Value = MatchStage.GroupG.ToString() },
            new SelectListItem { Text = "Grupo H", Value = MatchStage.GroupH.ToString() },
            new SelectListItem { Text = "Octavos", Value = MatchStage.Round16.ToString() },
            new SelectListItem { Text = "Cuartos", Value = MatchStage.QuarterFinals.ToString() },
            new SelectListItem { Text = "Semis", Value = MatchStage.SemiFinals.ToString() },
            new SelectListItem { Text = "3er puesto", Value = MatchStage.ThirdPlace.ToString() },
            new SelectListItem { Text = "Final", Value = MatchStage.Final.ToString() }
        };

        public static bool SupportPenalties(this MatchStage matchStage)
        {
            return matchStage >= MatchStage.Round16;
        }

        public static bool SupportPenalties(string matchStage)
        {
            try
            {
                var stage = (MatchStage)Enum.Parse(typeof(MatchStage), matchStage);
                return stage >= MatchStage.Round16;
            }
            catch
            {
                return false;
            }
        }
    }
}