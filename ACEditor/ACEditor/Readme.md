`PropertyEditor`

* Renders a tab for each type of property it supports

* Adds menu / events for selecting, updating, exporting, etc

  

`PropertyTable`

* Renders a table of properties
* `UseFilter` for optional `PropertyFilter` to narrow the list?  



`PropertyFilter` 

* Renders a combo box of a `PropertyType`
* Has `Display` used if a name for the component is needed
* Has `FilterText` used to narrow available properties
  * Make Regex optional?
* Has an optional `WorldObject` target
  * ~~Has checkbox to include missing properties on its target~~  Do it if needed in parent
* Has a `Label` used to name/find its ImGui items
* Has a `Changed` bool if it updated this render



