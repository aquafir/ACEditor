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
using PropType = ACEditor.Props.PropType;
using UtilityBelt.Scripting;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Linq.Expressions;
using ACEditor.Table;

namespace ACEditor;
internal class PropertyEditor : IDisposable
{
    /// <summary>
    /// The UBService Hud
    /// </summary>
    readonly Hud hud;
    readonly Game game = new();
    readonly List<PropertyTable> propTables = new()
    {
        new (PropType.PropertyInt),
        new (PropType.PropertyFloat),
        new (PropType.PropertyString),
    };

    /// <summary>
    /// Original clone of the WorldObject
    /// </summary>
    PropertyData Original = new();
    /// <summary>
    /// Current version of property data
    /// </summary>
    PropertyData Current = new();

    public PropertyEditor()
    {
        // Create a new UBService Hud
        hud = UBService.Huds.CreateHud("ACEditor");

        hud.Visible = true;
        hud.WindowSettings = ImGuiWindowFlags.AlwaysAutoResize;

        // set to show our icon in the UBService HudBar
        hud.ShowInBar = true;

        // subscribe to the hud render event so we can draw some controls
        hud.OnRender += Hud_OnRender;

        game.World.OnObjectSelected += OnSelected;
    }


    private Task OnSelected(object sender, UtilityBelt.Scripting.Events.ObjectSelectedEventArgs e)
    {
        var wo = game.World.Get(e.ObjectId);

        if (wo is null)
            return Task.CompletedTask;

        
        C.Chat($"Selected {e.ObjectId} - {wo.Name}");

        //Clone WO
        Original = new PropertyData(wo);
        Current = new PropertyData(wo);

        foreach (var table in propTables)
        {
            table.SetTarget(Current);
        }

        return Task.CompletedTask;
    }


    /// <summary>
    /// Called every time the ui is redrawing.
    /// </summary>
    private void Hud_OnRender(object sender, EventArgs e)
    {
        try
        {
            DrawTabBar();
        }
        catch (Exception ex)
        {
            PluginCore.Log(ex);
        }
    }

    private void DrawTabBar()
    {
        //Draw each table as a tab
        if (ImGui.BeginTabBar("PropertyTab"))
        {
            ImGui.Text($"Tabs: {propTables.Count}");
            foreach (var table in propTables)
            {
                if (ImGui.BeginTabItem($"{table.Type}"))
                {
                    ImGui.Text($"Testing {table.Type}");

                    table.Render();

                    ImGui.EndTabItem();
                }
            }
            ImGui.EndTabBar();
        }
    }

    public void Dispose()
    {
        hud.Dispose();
    }
}