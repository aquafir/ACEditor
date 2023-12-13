using AcClient;
//using ACE.DatLoader.FileTypes;
//using ACE.Entity.Models;
using CommandLine;
using Decal.Adapter;
using ImGuiNET;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Numerics;
using System.Reflection.Emit;
using System.Reflection;
using System.Security.Claims;
using System.Text.RegularExpressions;
using UtilityBelt.Common.Enums;
using UtilityBelt.Scripting.Interop;
using UtilityBelt.Scripting.Lib;
using UtilityBelt.Service;
using UtilityBelt.Service.Views;
using static AcClient.UIQueueManager;
using static System.Net.Mime.MediaTypeNames;
using static UtilityBelt.Common.Messages.Types.Fellowship;
using static UtilityBelt.Common.Messages.Types.PlayerModule.OptionProperty.Window;
using ACEditor.Props;
using PropertyType = ACEditor.Props.PropertyType;
using UtilityBelt.Scripting;

namespace ACEditor;
internal class ExampleUI : IDisposable
{
    /// <summary>
    /// The UBService Hud
    /// </summary>
    private readonly Hud hud;

    /// <summary>
    /// The default value for TestText.
    /// </summary>
    public const string DefaultTestText = "Some Test Text";

    /// <summary>
    /// Some test text. This value is used to the text input in our UI.
    /// </summary>
    public string TestText = DefaultTestText.ToString();

    public ExampleUI()
    {
        // Create a new UBService Hud
        hud = UBService.Huds.CreateHud("ACEditor");

        hud.Visible = true;
        hud.WindowSettings = ImGuiWindowFlags.AlwaysAutoResize;

        // set to show our icon in the UBService HudBar
        hud.ShowInBar = true;

        // subscribe to the hud render event so we can draw some controls
        hud.OnRender += Hud_OnRender;
    }

    List<PropertyFilter> filters = new()
    {
        new()
        {
             Type = PropertyType.PropertyBool, 
            Label = "Bool",
        },
        new()
        {
             Type = PropertyType.PropertyString,
            Label = "String",
        },
        new()
        {
             Type = PropertyType.PropertyInt,
            Label = "Int",
        },
    };

    /// <summary>
    /// Called every time the ui is redrawing.
    /// </summary>
    private void Hud_OnRender(object sender, EventArgs e)
    {
        try
        {
            foreach (var filter in filters)
            {
                filter.Render();
                if (filter.Changed)
                    C.Chat($"Filter changed: {filter.Label}");
            }
            //foreach(var pType in Enum.GetValues(typeof(PropertyType)))
            //{
            //    //ImGui.
            //}
            //ImGui.BeginCombo()

            //ImGui.InputTextMultiline("Test Text", ref TestText, 5000, new Vector2(400, 150));

            //if (ImGui.Button("Print Test Text"))
            //{
            //    OnPrintTestTextButtonPressed();
            //}

            //ImGui.SameLine();

            //if (ImGui.Button("Reset Test Text"))
            //{
            //    TestText = DefaultTestText;
            //}
        }
        catch (Exception ex)
        {
            PluginCore.Log(ex);
        }
    }

    /// <summary>
    /// Called when our print test text button is pressed
    /// </summary>
    private void OnPrintTestTextButtonPressed()
    {
        var textToShow = $"Test Text:\n{TestText}";

        CoreManager.Current.Actions.AddChatText(textToShow, 1);
        UBService.Huds.Toaster.Add(textToShow, ToastType.Info);
    }


//--Generic EnumConst wasn't working


//-- - @param self PropFilter
//---@param filter? string    Filter text
//---@param wo? WorldObject   Filter by properties a WorldObject has
//-- ---@param update? boolean   Builds properties unless false
//---@return PropFilter
//function PropFilter:SetFilter(filter, wo)
//  if filter ~= nil then self.FilterText = filter end
//  if wo ~= nil then self.Weenie = wo end
//  --Create regex
//  if filter ~= nil and filter ~= "" then
//    self.Regex = Regex.new(self.FilterText, RegexOptions.IgnoreCase)-- + RegexOptions.Compiled)
//  else
//    self.Regex = nil
//  end

//  --Todo: do this more better
//  -- - @type TimeSpan
//  -- local lapsed = DateTime.UtcNow - self.LastKey
//  -- if lapsed.Milliseconds < 500 then return self end
//  -- --if lapsed.Milliseconds > 500 or self.Updating then return self end
//  -- -- self.Updating = true
//  -- self.LastKey = DateTime.UtcNow
//  -- print('Updating after ', lapsed.Milliseconds)
//  self:BuildPropList()


//  return self
//end


//---Uses current filter to refresh Properties for other changes
//---@param self PropFilter
//---@return PropFilter
//function PropFilter:UpdateFilter()
//  self: SetFilter(self.FilterText, self.Weenie)

//  return self
//end

//-- - @param self PropFilter
//---@return any # Value of SelectedIndex property if Weenie has it, nil if missing
//function PropFilter:Value()
//  --Todo: error check?
//  --Support for non PropKeys?
//  if self.Weenie == nil then return nil end
//  if not self.Weenie.HasValue(self.Properties[self.SelectedIndex]) then return nil end
//  return self.Weenie.Value(self.Properties[self.SelectedIndex])
//end

//-- - @param self PropFilter
//---@param value any         Enum property value
//function PropFilter:IsFiltered(value)
//  --If the filter has a WorldObject and missing props are filtered check if Weenie has the given property
//  if self.Weenie ~= nil and not self.IncludeMissingProps and not self.Weenie.HasValue(value) then return true end
//  -- then print(value, self.Weenie.HasValue(value)) end
  
//  --Regex match (todo: pattern matching)
//  if self.Regex ~= nil and self.UseRegex and not self.Regex.IsMatch(value) then return true end
  
//  return false
//end

//---@param self PropFilter
//---@returns string  # Friendly name of PropType
//function PropFilter:TypeName()
//  if self.Type == PropType.Bool then return 'Bool' end
//  if self.Type == PropType.DataId then return 'DataId' end
//  if self.Type == PropType.Float then return 'Float' end
//  if self.Type == PropType.InstanceId then return 'InstanceId' end
//  if self.Type == PropType.Int then return 'Int' end
//  if self.Type == PropType.Int64 then return 'Int64' end
//  if self.Type == PropType.String then return 'String' end
//  if self.Type == PropType.Unknown then return 'Unknown' end
    
//  return 'ERROR'
//end





    public void Dispose()
    {
        hud.Dispose();
    }
}