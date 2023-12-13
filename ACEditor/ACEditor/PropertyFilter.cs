using ACEditor.Props;
using Decal.Adapter;

//using Decal.Adapter;
//using Decal.Adapter.Wrappers;
using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using UtilityBelt.Scripting.Interop;


namespace ACEditor;

public class PropertyFilter
{
    public string Display { get; set; } = "";
    public string Label { get; set; }

    public PropertyType Type { get; set; } = PropertyType.Unknown;
    //public bool IncludeMissing = false;
    public bool UseFilter { get; set; } = true;
    //public bool UseRegex { get; set; } = true;

    public int SelectedIndex = 0;
    public string Selection => SelectedIndex < Props.Length ? Props[SelectedIndex] : null;

    public string[] Props { get; set; } = new string[0];

    public WorldObject Target { get; set; }
    public string FilterText = "";

    public bool Changed { get; set; } = false;


    public PropertyFilter()
    {
        Label = Type.ToString();
    }   

    public void Render()
    {
        //Todo: when to reset?
        Changed = false;

        if(ImGui.Combo($"###{Label}Combo", ref SelectedIndex, Props, Props.Length))
        {

            //C.Chat("Selected " + SelectedIndex);
            C.Chat(Selection ?? "");
        }

        if (UseFilter)
        {
            ImGui.SetNextItemWidth(200);
            ImGui.SameLine();

            if (ImGui.InputText($"{Display}###{Label}Filter", ref FilterText, 256))
            {
                SetFilter();
            }
        }

        //ImGui.SameLine();
        //if(ImGui.Checkbox($"Include Missing?###{Label}IncMiss", ref IncludeMissing)) {
        //    SetFilter();
        //}
    }

    public void SetFilter()
    {
        Changed = true;

        //Get Target props
        Props = Target is null ? Type.GetProps() : null;

        //Apply filter
        if (!String.IsNullOrWhiteSpace(FilterText)) {
            var regex = new Regex(FilterText ?? "", RegexOptions.IgnoreCase);

            Props = Props.Where(x => regex.IsMatch(x)).ToArray();
        }

        C.Chat("Filter changed");
    }
}
//---@class PropFilter
//---@field private Weenie WorldObject   Optional WorldObject used for value checks
//---@field private FilterText string Optional filter
//---@field private LastKey DateTime      Todo, think about how to update so it doesn't lag
//---@field Regex               Regex Regex compiled when filter text updated
//---@field Properties          string[] Populated list of Enum values
//-- ---@field DrawFilter fun(s: PropFilter, boolean)
//PropFilter         = {
//  --Defaults
//  SelectedIndex = 1,
//  Type = PropType.Unknown,
//  FilterText = "",
//  UseRegex = true,
//  IncludeMissingProps = false,
//  LastKey = DateTime.MinValue,
//  Updating = false,
//  Properties = { },
//}
//PropFilter.__index = PropFilter

