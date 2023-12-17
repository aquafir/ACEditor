`PropertyEditor`

* Renders a tab for each `PropType` it supports
* Adds menu / events for selecting, updating, exporting, etc
* Snapshots a `PropertyData` as a copy of a WorldObject



`PropType` are the standard types and maybe additional types (e.g., Vitals) for the future

* Used to convert data to the right format
* Used to get arrays of keys
  * From corresponding Enum
  * Or a `PropertyData` target
* Eventually want the values to be loaded from a file
  * Min/max ranges
  * FakeProp support





`PropertyTable`

* Renders a table of properties
* Store an `<int, string>` dictionary 
* 



`PropertyFilter` 

* Renders a combo box of a `PropertyType`
  * Has `FilterText` used to narrow available properties
    * Make Regex optional?
  * Has an optional `WorldObject` target
    * ~~Has checkbox to include missing properties on its target~~  Do it if needed in parent
  * `Props` keeps a `string[]` of the enum names, used for the combo
* Has `Display` used if a name for the component is needed
* Has a `Label` used to name/find its ImGui items
* Has a `Changed` bool if it updated this render





Todo

* Look up range of values for property type/key

