﻿using Dalamud.Interface;
using ImGuiNET;
using Newtonsoft.Json;
using RotationSolver.Helpers;
using RotationSolver.Localization;
using RotationSolver.Rotations.CustomRotation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RotationSolver.Timeline;

internal class ConditionSet : ICondition
{
    public bool IsTrue(ICustomRotation combo) => Conditions.Count == 0 ? false :
                          IsAnd ? Conditions.All(c => c.IsTrue(combo))
                                : Conditions.Any(c => c.IsTrue(combo));
    public List<ICondition> Conditions { get; set; } = new List<ICondition>();
    public bool IsAnd { get; set; }

    [JsonIgnore]
    public float Height => Conditions.Sum(c => c is ConditionSet ? c.Height + 10 : c.Height) + ICondition.DefaultHeight + 12;
    public void Draw(ICustomRotation combo)
    {
        if (ImGui.BeginChild("ConditionSet" + GetHashCode().ToString(), new System.Numerics.Vector2(-1f, Height), true))
        {
            AddButton();

            ImGui.SameLine();

            ImGuiHelper.DrawCondition(IsTrue(combo));

            ImGui.SameLine();

            int isAnd = IsAnd ? 1 : 0;
            if (ImGui.Combo("##Rule" + GetHashCode().ToString(), ref isAnd, new string[]
            {
                "OR", "AND",
            }, 2))
            {
                IsAnd = isAnd != 0;
            }

            ImGui.Separator();

            var relay = Conditions;
            if (ImGuiHelper.DrawEditorList(relay, i => i.Draw(combo)))
            {
                Conditions = relay;
            }

            ImGui.EndChild();
        }
    }

    private void AddButton()
    {
        if (ImGuiHelper.IconButton(FontAwesomeIcon.Plus, "AddButton" + GetHashCode().ToString()))
        {
            ImGui.OpenPopup("Popup" + GetHashCode().ToString());
        }

        if (ImGui.BeginPopup("Popup" + GetHashCode().ToString()))
        {
            AddOneCondition<ConditionSet>(LocalizationManager.RightLang.Timeline_ConditionSet);
            AddOneCondition<ActionCondition>(LocalizationManager.RightLang.Timeline_ActionCondition);
            AddOneCondition<TargetCondition>(LocalizationManager.RightLang.Timeline_TargetCondition);
            AddOneCondition<RotationCondition>(LocalizationManager.RightLang.Timeline_RotationCondition);

            ImGui.EndPopup();
        }
    }

    private void AddOneCondition<T>(string name) where T : ICondition
    {
        if (ImGui.Selectable(name))
        {
            Conditions.Add(Activator.CreateInstance<T>());
            ImGui.CloseCurrentPopup();
        }
    }
}
