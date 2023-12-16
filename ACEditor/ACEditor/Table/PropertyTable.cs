using ACE.Server.WorldObjects;
using ACEditor.Props;
using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UtilityBelt.Scripting.Interop;
using WorldObject = UtilityBelt.Scripting.Interop.WorldObject;

namespace ACEditor.Table;

public class PropertyTable
{
    #region Constants
    const ImGuiTableFlags TABLE_FLAGS = ImGuiTableFlags.Sortable |
            ImGuiTableFlags.RowBg |
            ImGuiTableFlags.ScrollY |
            ImGuiTableFlags.BordersOuter |
            ImGuiTableFlags.BordersV |
            ImGuiTableFlags.ContextMenuInBody;

    #endregion

    #region Members
    PropertyData Target;
    public PropertyFilter filter;

    public PropType Type { get; set; }

    public bool UseFilter { get; set; } = true;

    //List<PropertyFilter> filters = new();

    //Data for the table
    public TableRow[] tableData = new TableRow[]
    {

    };

    #endregion

    public PropertyTable(PropType type)
    {
        Type = type;
        filter = new(type);
        filter?.UpdateFilter();
    }

    public void SetTarget(PropertyData target)
    {
        //Todo: clone?
        this.Target = target;
        filter?.SetTarget(target);
    }

    //Sort table based on column/direction
    private uint sortColumn = 0; // Currently sorted column index
    private ImGuiSortDirection sortDirection = ImGuiSortDirection.Ascending;
    private int CompareTableRows(TableRow a, TableRow b) => sortColumn switch
    {
        0 => a.Property.CompareTo(b.Property) * (sortDirection == ImGuiSortDirection.Descending ? -1 : 1),
        1 => a.OriginalValue.CompareTo(b.OriginalValue) * (sortDirection == ImGuiSortDirection.Descending ? -1 : 1),
    };
    //Sort if needed
    private void Sort()
    {
        var tableSortSpecs = ImGui.TableGetSortSpecs();

        //Check if a sort is needed
        if (tableSortSpecs.SpecsDirty)
        {
            //Set column/direction
            sortDirection = tableSortSpecs.Specs.SortDirection;
            sortColumn = tableSortSpecs.Specs.ColumnUserID;
            //Console.WriteLine($"Dirty: {sortDirection} - {tableSortSpecs.Specs.ColumnUserID}");

            //tableSortSpecs.SpecsDirty = false;
            Array.Sort(tableData, CompareTableRows);
        }
    }

    public void Render()
    {
        filter.Render();
        if(filter.Changed)
        {

        }

        if (ImGui.BeginTable($"{Type}", 2, TABLE_FLAGS))
        {
            // Set up columns
            int columnIndex = 0;
            ImGui.TableSetupColumn($"Column {columnIndex}", ImGuiTableColumnFlags.DefaultSort, 50, (uint)columnIndex++);

            //ImGui::PushItemWidth(-ImGui::GetContentRegionAvail().x * 0.5f);

            // Headers row
            ImGui.TableSetupScrollFreeze(0, 1);
            ImGui.TableHeadersRow();

            //Sort if needed
            Sort();

            for (int i = 0; i < tableData.Length; i++)
            {
                ImGui.TableNextRow();

                ImGui.TableNextColumn();
                //ImGui.PushItemWidth(ImGui.GetContentRegionAvail().X * .33f);
                ImGui.Text($"{tableData[i].Property}");
                ImGui.PopItemWidth();

                if (ImGui.BeginPopupContextItem())
                {
                    if (ImGui.MenuItem("Test123"))
                        Console.WriteLine("Clicked");
                    ImGui.EndPopup();
                }

                ImGui.TableNextColumn();
                ImGui.Text($"{tableData[i].OriginalValue}");

                //ImGui.TableNextColumn();
                //ImGui.Text($"{tableData[i].Value}");
            }

            //for (int i = 0; i < tableData.Length; i++)
            //{
            //    ImGui.TableNextRow();

            //    ImGui.TableNextColumn();
            //    //ImGui.PushItemWidth(ImGui.GetContentRegionAvail().X * .33f);
            //    ImGui.Text($"{tableData[i].Property}");
            //    ImGui.PopItemWidth();

            //    if (ImGui.BeginPopupContextItem())
            //    {
            //        if (ImGui.MenuItem("Test123"))
            //            Console.WriteLine("Clicked");
            //        ImGui.EndPopup();
            //    }

            //    ImGui.TableNextColumn();
            //    ImGui.Text($"{tableData[i].OriginalValue}");

            //    //ImGui.TableNextColumn();
            //    //ImGui.Text($"{tableData[i].Value}");
            //}

            ImGui.EndTable();
        }
    }
}