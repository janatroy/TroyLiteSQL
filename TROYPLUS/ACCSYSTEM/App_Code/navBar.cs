using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Text;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.Web.UI.Design;
using System.IO;
using System.Reflection;
using System.Web;

namespace ACCSYSTEM
{
	[DefaultProperty("TestProperty"),ToolboxData("<{0}:NavBar runat=server></{0}:NavBar>"), PersistChildren(false), Designer(typeof(NavBarDesigner))]
	public class NavBar : System.Web.UI.WebControls.WebControl
	{

		/// <exclude/>
		private string _classnavbar = "navbar";
		/// <exclude/>
		private bool _useroles = true;
		/// <exclude/>
		private bool _usereplace = false;
		/// <exclude/>
		private NavBarBlocks _blocks = new NavBarBlocks();
		
		/// <summary>
		/// The height of the control in Units
		/// </summary>
		public override Unit Height
		{
			get	{return base.Height;}
			set {base.Height = value;}
		}

		/// <summary>
		/// The width of the control in Units
		/// </summary>
		public override Unit Width
		{
			get	{return base.Width;}
			set {base.Width = value;}
		}

		/// <summary>
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void Render(HtmlTextWriter output)
		{
			StringBuilder sb = new StringBuilder();

			System.Reflection.Assembly thisExe;
			thisExe = System.Reflection.Assembly.GetExecutingAssembly();
			string [] resources = thisExe.GetManifestResourceNames();
			
			for(int i=0; i<resources.Length; i++)
			{
				if(resources[i].EndsWith("navbar.js")) 
				{
					TextReader sr = new StreamReader(thisExe.GetManifestResourceStream(resources[i]), System.Text.Encoding.UTF8);
					output.Write(sr.ReadToEnd());
				}
			}
			
			if(_classnavbar != null && _classnavbar != string.Empty) 
				base.Attributes.Add("class", _classnavbar);

			base.AddAttributesToRender(output);
			
			base.RenderBeginTag(output);

			bool expando = false;

			foreach(NavBarBlock bl in this.Blocks) 
			{
				if(bl.Expanded) 
				{
					expando = true;
					break;
				}
			}

			if(!expando && this.Blocks.Count>0) 
			{
				this.Blocks[0].Expanded = true;
			}

			expando = false;
			int blockid = 0;

			foreach(NavBarBlock bl in this.Blocks) 
			{
				bool allowed = !_useroles;

				foreach(NavBarItem itm in bl.Items) 
				{
					if(!this.DesignMode) 
					{
						if(itm.Roles == null || itm.Roles.Equals(string.Empty)) 
						{
							allowed = !_useroles;
						}
						else if(itm.Roles!=null) 
						{
							switch (itm.Roles) 
							{
								case "*":
									allowed = true;
									break;
								case "?":
									allowed = this.Page.User.Identity.IsAuthenticated;
									break;
								default :
									string[] roles = Convert.ToString(itm.Roles).Split(',');

									for(int i=0; i<roles.Length; i++) 
									{
										if(this.Page.User.IsInRole(roles[i])) 
										{
											allowed = true;
											break;
										}
									}
									break;
							}
						}
					}
					else
					{
						allowed = true;
					}
				}

				if(bl.Items.Count>0 && allowed) 
				{
					HttpCookie cookie = this.Parent.Page.Request.Cookies["Expanded"];
					string prevexpanded = string.Empty;
					string blockname = string.Format("nbBlock_{0}", (++blockid).ToString());

					if(cookie!=null) 
						prevexpanded = cookie.Value;

					cookie = this.Parent.Page.Request.Cookies["Selected"];
					string selected = string.Empty;

					if(cookie!=null) 
						selected = cookie.Value;

					output.WriteBeginTag("div");
					output.Write(" class='block'");
					output.Write(string.Format(" id='{0}' ", blockname ));
					output.Write(">");

					string expanded = string.Empty;
					string collapsed = string.Empty;

					if(prevexpanded != string.Empty) 
					{
						if(prevexpanded == blockname) 
							bl.Expanded = true;
						else
							bl.Expanded = false;
					}

					if(bl.BlockStyle == null || bl.BlockStyle.Closed == null) 
					{
						if(!(this.DefaultBlockStyle == null || this.DefaultBlockStyle.Closed == null)) 
							collapsed = this.DefaultBlockStyle.Closed;	
					}
					else
						collapsed = this.DefaultBlockStyle.Closed;

					if(bl.BlockStyle == null || bl.BlockStyle.Expanded == null) 
					{
						if(!(this.DefaultBlockStyle == null || this.DefaultBlockStyle.Expanded == null)) 
							expanded = this.DefaultBlockStyle.Expanded;	
					}
					else
						expanded = this.DefaultBlockStyle.Expanded;

					//output.WriteAttribute("expanded", "false");

					//Block Header
					if(!bl.Expanded) 
					{
						output.WriteBeginTag("div");
						output.WriteAttribute("class" , collapsed);	
						output.WriteAttribute("expanded", "false");
					}
					else
					{
						output.WriteBeginTag("div");
						output.WriteAttribute("class" , expanded);	
						output.WriteAttribute("expanded", "true");
					}

					output.WriteAttribute("onclick", string.Format("nbToggleBlock(this, '{0}', '{1}')", collapsed, expanded));
					output.Write(">");
                    if(bl.Image != null && bl.Image.ToString() != "")
                        output.Write("<img src='" + bl.Image + "'/>&nbsp;");

					output.Write(bl.Text);
					output.WriteEndTag("div");
					//End Block Header

					//Item Area
					output.WriteBeginTag("div");
					if(bl.BlockStyle == null || bl.BlockStyle.ItemArea == null)
					{
						if(this.DefaultBlockStyle == null || this.DefaultBlockStyle.ItemArea == null) 
						{
							output.Write(" class=''");
						}
						else
						{
							output.WriteAttribute("class", this.DefaultBlockStyle.ItemArea);
						}

					}
					else
					{
						output.WriteAttribute("class", bl.BlockStyle.ItemArea);
					}

					if(!bl.Expanded)
                        output.WriteAttribute("style", "display:none;overflow-x:hidden; overflow-y: auto");
					else
                        output.WriteAttribute("style", "display:block;overflow-x:hidden; overflow-y: auto");

					output.Write(">");

					System.Web.UI.HtmlControls.HtmlTable tbl = GetChildrenAsTable(bl.Items, blockname, selected);

					tbl.RenderControl(output);

					output.WriteEndTag("div");
					//End Item Area
					output.WriteEndTag("div");
				}
			}

			base.RenderEndTag(output);

		}

		/// <summary>
		/// Detects whether the Control is used on the design surface or in run mode.
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool DesignMode 
		{ 
			get
			{
				return (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv");
			}
		}

		/// <summary>
		/// Returns the Child items wrapped in an HTML table, with correctly formatted
		/// TR and TD elements
		/// </summary>
		/// <param name="items"><see cref="NavBarItem"/> Collection to render</param>
		/// <param name="blockname">The heading that appaears at the top of the block</param>
		/// <param name="selectedcell">The ID of the currently selected cell (so that it can be rendered
		/// with the correct css class)</param>
		/// <returns>An HTMLTable containing al of the items</returns>
		public System.Web.UI.HtmlControls.HtmlTable GetChildrenAsTable(NavBarItems items, string blockname, string selectedcell) 
		{
			System.Web.UI.HtmlControls.HtmlTable tbl = new System.Web.UI.HtmlControls.HtmlTable();
			tbl.Border= 0;
			tbl.CellPadding = 2;
			tbl.CellSpacing= 0;
			tbl.Width = "100%";

			bool allowed = !_useroles;
			int itmcount = 0;

			foreach(NavBarItem itm in items) 
			{
				allowed = false;

				if(!this.DesignMode) 
				{
					if(itm.Roles == null || itm.Roles.Equals(string.Empty)) 
					{
						allowed = !_useroles;
					}
					else if(itm.Roles!=null) 
					{
						switch (itm.Roles) 
						{
							case "*":
								allowed = true;
								break;
							case "?":
								allowed = this.Page.User.Identity.IsAuthenticated;
								break;
							default :
								string[] roles = Convert.ToString(itm.Roles).Split(',');

								for(int i=0; i<roles.Length; i++) 
								{
									if(this.Page.User.IsInRole(roles[i])) 
									{
										allowed = true;
										break;
									}
								}
								break;
						}
					}
				}
				else
				{
					allowed = true;
				}

				if(allowed) 
				{
					string cellid = blockname + "_nbNavItem_" + (++itmcount).ToString();

					string selectedstyle = string.Empty;
					string basestyle = string.Empty;
					string hoverstyle = string.Empty;

					if(itm.ItemStyle == null || itm.ItemStyle.Selected == null) 
					{
						if(this.DefaultItemStyle == null || this.DefaultItemStyle.Selected == null) 
							selectedstyle = string.Empty;
						else
							selectedstyle = this.DefaultItemStyle.Selected;
					}
					else
					{
						selectedstyle = itm.ItemStyle.Selected;
					}

					if(itm.ItemStyle == null || itm.ItemStyle.Base == null) 
					{
						if(this.DefaultItemStyle == null || this.DefaultItemStyle.Base == null) 
							basestyle = string.Empty;
						else
							basestyle = this.DefaultItemStyle.Base;
					}
					else
					{
						basestyle = itm.ItemStyle.Base;
					}

					if(itm.ItemStyle == null || itm.ItemStyle.Hover == null) 
					{
						if(this.DefaultItemStyle == null || this.DefaultItemStyle.Hover == null) 
							hoverstyle = string.Empty;
						else
							hoverstyle = this.DefaultItemStyle.Hover;
					}
					else
					{
						hoverstyle = itm.ItemStyle.Hover;
					}


					if(selectedcell != string.Empty)
						if(selectedcell != cellid) 
							itm.Selected = false;
						else 
							itm.Selected =true;

					HtmlTableRow tr = new HtmlTableRow();
			
					string style = string.Empty;

					if(itm.Selected) 
						style = selectedstyle;
					else
						style = basestyle;

					tr.Attributes.Add("class", style);
					tr.Style.Add("width", "100%");

					HtmlTableCell td = new HtmlTableCell();
					td.Style.Add("width", "100%");
					td.ID = cellid;

					td.Attributes.Add("class", style);
					td.Attributes.Add("onmouseover", string.Format("nbItemHighlight(this,'{0}')", hoverstyle) );
					td.Attributes.Add("onmouseout", string.Format("nbItemLowlight(this,'{0}')", style));
					td.Attributes.Add("selected", itm.Selected.ToString());

					if(_usereplace) 
						td.Attributes.Add("onclick", string.Format("nbReplace('{0}', '{1}', '{2}')", cellid, (itm.NavigateUrl==null || itm.NavigateUrl.Equals(string.Empty)) ? "#" : System.Web.HttpUtility.UrlEncode(itm.NavigateUrl), selectedstyle));
					else
						td.Attributes.Add("onclick", string.Format("nbNavigate('{0}', '{1}', '{2}')", cellid, (itm.NavigateUrl==null || itm.NavigateUrl.Equals(string.Empty)) ? "#" : System.Web.HttpUtility.UrlEncode(itm.NavigateUrl), selectedstyle));

					if(itm.Description!=null && itm.Description.Length>0) 
						td.Attributes.Add("title", System.Web.HttpUtility.HtmlEncode(itm.Description));
					
					HtmlAnchor a = new HtmlAnchor();
					LiteralControl lc = new LiteralControl();
					
					a.HRef = string.Format("javascript: {1}('{0}', '{2}', '{3}');", cellid, _usereplace ? "nbReplace" : "nbNavigate", (itm.NavigateUrl==null || itm.NavigateUrl.Equals(string.Empty)) ? "#" : System.Web.HttpUtility.UrlEncode(itm.NavigateUrl), selectedstyle);

					StringBuilder inner = new StringBuilder();

					if(itm.Image != null && itm.Image.Length>0) 
						inner.AppendFormat("<img src='{0}'/>", itm.Image);

					inner.Append("<span class='nbItem'>" + itm.Text + "</span>" );

					lc.Text = inner.ToString();
					a.InnerHtml = inner.ToString();

					td.Controls.Add(lc);


					tr.Cells.Add(td);
					tbl.Rows.Add(tr);
				}

			}
			
			return tbl;
		}
		
		/// <summary>
		/// Overrides the default TagKey so that the control renders within it's own DIV
		/// </summary>
		protected override HtmlTextWriterTag TagKey
		{
			get {return HtmlTextWriterTag.Div;}
		}

		/// <exclude/>
		private NavBarItemStyles _itmstyle = new NavBarItemStyles(); 
		/// <exclude/>
		private NavBarBlockStyles _blkstyle = new NavBarBlockStyles();

		/// <summary>
		/// The Default CSS Class of a block.  Default values are used in the absence of
		/// specific settings.
		/// </summary>
		[DefaultValue(""), Category("Style"),
		PersistenceMode(PersistenceMode.InnerProperty),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),		
		NotifyParentProperty(true),
		RefreshProperties(RefreshProperties.Repaint)]
		public NavBarBlockStyles DefaultBlockStyle 
		{
			get {return _blkstyle;}
			set {_blkstyle = value;}
		}

		/// <summary>
		/// The default CSS Class of each Item  Default values are used in the absence of
		/// specific settings.
		/// </summary>
		[DefaultValue(""), Category("Style"),
		PersistenceMode(PersistenceMode.InnerProperty),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),		
		NotifyParentProperty(true),
		RefreshProperties(RefreshProperties.Repaint)]
		public NavBarItemStyles DefaultItemStyle 
		{
			get {return _itmstyle;}
			set {_itmstyle = value;}
		}

		/// <summary>
		/// Collection of blocks within the control.  Each block has a header and many items.
		/// </summary>
		[DefaultValue(""), Category("Custom"),
		PersistenceMode(PersistenceMode.InnerProperty),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public NavBarBlocks Blocks 
		{
			get {return _blocks;}
		}

		/// <summary>
		/// Security Setting.  Setting this value causes the control to inspect a user's 
		/// role during the render process.  Each Item can have many roles associated with
		/// it, and if the user is not in one of these roles, then the item will not be
		/// rendered.
		/// </summary>
		[DefaultValue(true), Category("Custom"),
		PersistenceMode(PersistenceMode.Attribute),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public bool UseRoles 
		{
			get {return _useroles;}
			set {_useroles = value;}
		}

		/// <summary>
		/// By default, a click on an item causes a <code>location.href = <i>URL</i>;</code> Javascript
		/// function to execute, which means that a user may user the Back button on their browser
		/// to return to the previously selected page.  However, if this property is True, then the javascript
		/// run is changed to <code>location.replace(<i>URL</i>);</code> which prevents the user
		/// from using their Back button.
		/// </summary>
		[DefaultValue(false), Category("Custom"),
		PersistenceMode(PersistenceMode.Attribute),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public bool ReplaceOnNavigate 
		{
			get {return _usereplace;}
			set {_usereplace = value;}
		}

	}

	#region Style Classes

	/// <summary>
	/// Each Item in the NavBarBlock can have it's own style attributes.  This class
	/// provides the possible types of style that can be applied to an Item.
	/// </summary>
	[TypeConverter(typeof(ExpandableObjectConverter)), PersistChildren(false)]
	public class NavBarItemStyles 
	{
		/// <summary>
		/// Create a new instance of a NavBarItemStyles class
		/// </summary>
		public NavBarItemStyles()
		{
		}

		/// <exclude/>
		private string _base;
		/// <exclude/>
		private string _hover;
		/// <exclude/>
		private string _selected;

		/// <summary>
		/// The Base style is the one that is used when the item is neither hovered or selected.
		/// </summary>
		[NotifyParentProperty(true),
		RefreshProperties(RefreshProperties.Repaint)]
		public string Base 
		{
			get {return _base;}
			set {_base = value;}
		}

		/// <summary>
		/// The Hover style is applied when the mouse hovers over the Item
		/// </summary>
		[NotifyParentProperty(true),
		RefreshProperties(RefreshProperties.Repaint)]
		public string Hover 
		{
			get {return _hover;}
			set {_hover = value;}
		}

		/// <summary>
		/// The Selected Style is applied when the user has clicked on the item, thereby selecting it.
		/// </summary>
		[NotifyParentProperty(true),
		RefreshProperties(RefreshProperties.Repaint)]
		public string Selected
		{
			get {return _selected;}
			set {_selected = value;}
		}

	}

	/// <summary>
	/// Each NavBarBlock has a header associated with it, the styles here represent the 
	/// styles applied to this header, with the exception of ItemArea, which is the style
	/// of the background where the Items are rendered.  Each style is a CSS Class Name.
	/// </summary>
	[TypeConverter(typeof(ExpandableObjectConverter)), PersistChildren(false)]
	public class NavBarBlockStyles 
	{
		/// <summary>
		/// Creates a new instance of a <see cref="NavBarBlockStyles"/> class.
		/// </summary>
		public NavBarBlockStyles()
		{
		}

		/// <exclude/>
		private string _hover;
		/// <exclude/>
		private string _selected;
		/// <exclude/>
		private string _expanded;
		/// <exclude/>
		private string _closed;
		/// <exclude/>
		private string _itemarea;

		/// <summary>
		/// The Hover style is applied when the mouse hovers over the Header
		/// </summary>
		[NotifyParentProperty(true),
		RefreshProperties(RefreshProperties.Repaint)]
		public string Hover 
		{
			get {return _hover;}
			set {_hover = value;}
		}

		/// <summary>
		/// The Selected Style is applied when the user has clicked on the Header, thereby selecting it.
		/// </summary>
		[NotifyParentProperty(true),
		RefreshProperties(RefreshProperties.Repaint)]
		public string Selected
		{
			get {return _selected;}
			set {_selected = value;}
		}

		/// <summary>
		/// The Expanded Style is applied to the header when the user has expanded the group.
		/// </summary>
		[NotifyParentProperty(true),
		RefreshProperties(RefreshProperties.Repaint)]
		public string Expanded 
		{
			get {return _expanded;}
			set {_expanded = value;}
		}

		/// <summary>
		/// The Closed style is applied to the header when the user has expanded the group.
		/// </summary>
		[NotifyParentProperty(true),
		RefreshProperties(RefreshProperties.Repaint)]
		public string Closed 
		{
			get {return _closed;}
			set {_closed = value;}
		}

		/// <summary>
		/// The ItemArea Style is applied to the background of the expanded group and appears behind 
		/// each of the items.
		/// </summary>
		[NotifyParentProperty(true),
		RefreshProperties(RefreshProperties.Repaint)]
		public string ItemArea 
		{
			get {return _itemarea;}
			set {_itemarea = value;}
		}

	}

	#endregion

	#region NavBar Blocks

	/// <summary>
	/// A collection of NavBarBlocks that make up the control
	/// </summary>
	[PersistChildren(false)]
	public class NavBarBlocks : CollectionBase
	{
		/// <summary>
		/// Create a new instance of a <see cref="NavBarBlocks"/> class
		/// </summary>
		public NavBarBlocks() {}

		/// <summary>
		/// Add a new <see cref="NavBarBlock"/> object to the collection.
		/// </summary>
		/// <param name="NewBlock">The <see cref="NavBarBlock"/> to add</param>
		/// <returns>The index of the newly added NavBarBlock</returns>
		public int Add(NavBarBlock NewBlock) 
		{
			return base.List.Add(NewBlock);
		}

		/// <summary>
		/// Add a new NavBarBlock object to the collection at the position given
		/// </summary>
		/// <param name="index">Position in the collection to add the object</param>
		/// <param name="Block">The new NavBarBlock object to include</param>
		public void Insert(int index, NavBarBlock Block) 
		{
			List.Insert(index, Block);
		}

		/// <summary>
		/// Removes the specified NavBarBlock from the collection
		/// </summary>
		/// <param name="Block">The NavBarBlock to remove</param>
		public void Remove(NavBarBlock Block)
		{
			List.Remove(Block);
		}

		/// <summary>
		/// Examines the collection to see if the supplied NavBarBlock is contained in the collection
		/// </summary>
		/// <param name="Block">The NavBarBlock object to find in the collection</param>
		/// <returns>True if the collection contains the object, false otherwise.</returns>
		public bool Contains(NavBarBlock Block)
		{
			return List.Contains(Block);
		}

		/// <summary>
		/// Examines the collection for the referenced NavBarBlock object and returns the index of the referenced 
		/// object (if found)
		/// </summary>
		/// <param name="Block">The NavBarBlock object to find</param>
		/// <returns>The index of referenced object.</returns>
		public int IndexOf(NavBarBlock Block)
		{
			return List.IndexOf(Block);
		}

		/// <summary>
		/// Creates and array of NavBarBlock objects from the collection
		/// </summary>
		/// <param name="array">The array of NavBarBlocks</param>
		/// <param name="index">The starting point in the array to begin the copy</param>
		public void CopyTo(NavBarBlock[] array, int index)
		{
			List.CopyTo(array, index);
		}
		
		/// <summary>
		/// Default accessor for the collection.  Provides the implementation of the NavBarBlocks[i] method.
		/// </summary>
		[DefaultValue(""), Category("Custom"),
		PersistenceMode(PersistenceMode.InnerProperty),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content), 
		NotifyParentProperty(true),
		RefreshProperties(RefreshProperties.Repaint)]
		public NavBarBlock this[int index]
		{
			get 
			{ 
				return (NavBarBlock)List[index]; 
			}
			set 
			{ 
				List[index] = value; 
			}
		}

	}

	#endregion

	#region NavBar Block

	/// <summary>
	/// A Block that has a header and a collection of NavBarItems
	/// </summary>
	[PersistChildren(false)]
	public class NavBarBlock
	{
		/// <exclude/>
		private NavBarItems _itemcoll = new NavBarItems();
		/// <exclude/>
		private string _text;
		/// <exclude/>
		private bool _expanded = false;
        /// <summary>
        private string _image;
        /// <exclude/>
		private NavBarBlockStyles _style = new NavBarBlockStyles();
		
		/// <summary>
		/// Create a new instance of a <see cref="NavBarBlock"/> class
		/// </summary>
		public NavBarBlock() {}


		/// <summary>
		/// The test that appears in the header of the block.
		/// </summary>
		[DefaultValue(""), NotifyParentProperty(true),
		RefreshProperties(RefreshProperties.Repaint)]
		public string Text 
		{
			get {return _text;}
			set {_text = value;}
		}

        /// <summary>
        /// The test that appears in the header of the block.
        /// </summary>
        [DefaultValue(""), NotifyParentProperty(true),
        RefreshProperties(RefreshProperties.Repaint)]
        public string Image
        {
            get { return _image; }
            set { _image = value; }
        }

		/// <summary>
		/// Whether the block is to be rendered as expanded
		/// </summary>
		[DefaultValue(false), NotifyParentProperty(true),
		RefreshProperties(RefreshProperties.Repaint)]
		public bool Expanded 
		{
			get {return _expanded;}
			set {_expanded = value;}
		}

		/// <summary>
		/// The collection of NavBarItem that will be rendered with this block.
		/// </summary>
		[DefaultValue(""), Category("Custom"),
		PersistenceMode(PersistenceMode.InnerProperty),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content), 
		NotifyParentProperty(true),
		RefreshProperties(RefreshProperties.Repaint)]
		public NavBarItems Items 
		{
			get {return _itemcoll;}
		}

		/// <summary>
		/// The CSS Class that will be used when rendering the block.
		/// </summary>
		[DefaultValue(""), Category("Style"),
		PersistenceMode(PersistenceMode.InnerProperty),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public NavBarBlockStyles BlockStyle 
		{
			get {return _style;}
			set {_style = value;}
		}
	}

	#endregion

	#region NavBar Items

	/// <summary>
	/// Collection of NavBarItems contained within a group
	/// </summary>
	[PersistChildren(false)]
	public class NavBarItems : CollectionBase 
	{
		/// <summary>
		/// Create a new instance of a <see cref="NavBarItems"/> class
		/// </summary>
		public NavBarItems() {}

		/// <summary>
		/// Add a new NavBarItem to the collection
		/// </summary>
		/// <param name="NewItem">NavBarItem to add</param>
		/// <returns>The index position of the newly created item</returns>
		public int Add(NavBarItem NewItem) 
		{
			return base.List.Add(NewItem);
		}

		/// <summary>
		/// Add a new NavBarItem object to the collection at the position given
		/// </summary>
		/// <param name="index">Position in the collection to add the object</param>
		/// <param name="Item">The new NavBarItem object to include</param>
		public void Insert(int index, NavBarItem Item) 
		{
			List.Insert(index, Item);
		}

		/// <summary>
		/// Removes the specified NavBarItem from the collection
		/// </summary>
		/// <param name="Item">The NavBarItem to remove</param>
		public void Remove(NavBarItem Item)
		{
			List.Remove(Item);
		}

		/// <summary>
		/// Examines the collection to see if the supplied NavBarItem is contained in the collection
		/// </summary>
		/// <param name="Item">The NavBarItem object to find in the collection</param>
		/// <returns>True if the collection contains the object, false otherwise.</returns>
		public bool Contains(NavBarItem Item)
		{
			return List.Contains(Item);
		}

		/// <summary>
		/// Examines the collection for the referenced NavBarItem object and returns the index of the referenced 
		/// object (if found)
		/// </summary>
		/// <param name="Item">The NavBarItem object to find</param>
		/// <returns>The index of referenced object.</returns>
		public int IndexOf(NavBarItem Item)
		{
			return List.IndexOf(Item);
		}

		/// <summary>
		/// Creates and array of change objects from the collection
		/// </summary>
		/// <param name="array">The array of NavBarItems</param>
		/// <param name="index">The starting point in the array to begin the copy</param>
		public void CopyTo(NavBarItem[] array, int index)
		{
			List.CopyTo(array, index);
		}
		
		/// <summary>
		/// Default accessor for the collection.  Provides implementation of the NavBarItems[i] method.
		/// </summary>
		public NavBarItem this[int index]
		{
			get 
			{ 
				return (NavBarItem)List[index]; 
			}
			set 
			{ 
				List[index] = value; 
			}
		}

	}

	#endregion

	#region NavBar Item

	/// <summary>
	/// Each item displayed in the list items is represented here.
	/// </summary>
	/// 
	[TypeConverter(typeof(ExpandableObjectConverter)),PersistChildren(false)]
	public class NavBarItem 
	{
		/// <exclude/>
		private string _text;
		/// <exclude/>
		private string _href;
		/// <exclude/>
		private string _image;
		/// <exclude/>
		private string _roles;
		/// <exclude/>
		private string _description = string.Empty;
		/// <exclude/>
		private NavBarItemStyles _style = new NavBarItemStyles();
		/// <exclude/>
		private bool _selected = false;

		/// <summary>
		/// Create a new instance of a <see cref="NavBarItem"/> class.
		/// </summary>
		public NavBarItem() {}

		/// <summary>
		/// Create a new instance of a <see cref="NavBarItem"/> class and sets the Text and Href properties.
		/// </summary>
		/// <param name="Text">The text that appears under the item</param>
		/// <param name="href">The navigation path that will run when the user clicks on this item.  
		/// Javascript functions are not allowed here.</param>
		public NavBarItem(string Text, string href) 
		{
			_text = Text;
			_href = href;
		}
		
		/// <summary>
		/// Create a new instance of a <see cref="NavBarItem"/> class and sets the Text, Image and Href properties.
		/// </summary>
		/// <param name="Text">The text that appears under the item</param>
		/// <param name="href">The navigation path that will run when the user clicks on this item.  
		/// Javascript functions are not allowed here.</param>
		/// <param name="image">Path to the image to display above the text</param>
		public NavBarItem(string Text, string href, string image) 		
		{
			_text = Text;
			_href = href;
			_image = image;
		}

		/// <summary>
		/// Create a new instance of a <see cref="NavBarItem"/> class and sets the Text, Image and Href properties.
		/// </summary>
		/// <param name="Text">The text that appears under the item</param>
		/// <param name="href">The navigation path that will run when the user clicks on this item.  
		/// Javascript functions are not allowed here.</param>
		/// <param name="image">Path to the image to display above the text</param>
		/// <param name="description">When the user hovers over the control, the tooltip containing this 
		/// value will be displayed.</param>
		public NavBarItem(string Text, string href, string image, string description)
		{
			_text = Text;
			_href = href;
			_image = image;
			_description = description;
		}

		/// <summary>
		/// When the user hovers over the control, the tooltip containing this 
		/// value will be displayed.
		/// </summary>
		[DefaultValue(""), NotifyParentProperty(true),
		RefreshProperties(RefreshProperties.Repaint)]
		public string Description 
		{
			get {return _description;}
			set {_description = value;}
		}

		/// <summary>
		/// The text that appears under the item
		/// </summary>
		[DefaultValue(""), NotifyParentProperty(true),
		RefreshProperties(RefreshProperties.Repaint)]
		public string Text 
		{
			get {return _text;}
			set {_text = value;}
		}

		/// <summary>
		/// The navigation path that will run when the user clicks on this item.  
		/// Javascript functions are not allowed here.
		/// </summary>
		[DefaultValue(""), NotifyParentProperty(true),
		RefreshProperties(RefreshProperties.Repaint)]
		public string NavigateUrl 
		{
			get {return _href;}
			set {_href = value;}
		}

		/// <summary>
		/// Path to the image to display above the text
		/// </summary>
		[DefaultValue(""), NotifyParentProperty(true),
		RefreshProperties(RefreshProperties.Repaint)]
		public string Image 
		{
			get {return _image;}
			set {_image = value;}
		}

		/// <summary>
		/// A Comma separated list of Roles to which the user must belong before the item will be 
		/// rendered.  <seealso cref="NavBar.UseRoles"/>
		/// </summary>
		[DefaultValue(""), NotifyParentProperty(true),
		RefreshProperties(RefreshProperties.Repaint)]
		public string Roles 
		{
			get {return _roles;}
			set {_roles = value;}
		}

		/// <summary>
		/// Boolen to indicate if this Item is currently selected.
		/// </summary>
		[DefaultValue(""), NotifyParentProperty(true),
		RefreshProperties(RefreshProperties.Repaint)]
		public bool Selected 
		{
			get {return _selected;}
			set {_selected = value;}
		}

		/// <summary>
		/// The CSS Class that will be applied by default to this item
		/// </summary>
		[DefaultValue(""), Category("Style"),
		PersistenceMode(PersistenceMode.InnerProperty),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public NavBarItemStyles ItemStyle 
		{
			get {return _style;}
			set {_style = value;}
		}

	}

	#endregion

	#region Designer

	/// <summary>
	/// NavBarDesigner that will be displayed on the design surface while editing the control
	/// in Visual Studio.
	/// </summary>
    public class NavBarDesigner : System.Web.UI.Design.ControlDesigner
	{
		/// <summary>
		/// The only method required for the designer.  Returns the HTML to render onto the
		/// design surface.
		/// </summary>
		/// <returns>HTML of the dummy table.</returns>
		public override string GetDesignTimeHtml()
		{
			NavBar nb = ((NavBar)Component);
			StringBuilder sb = new StringBuilder();

			sb.Append("<div ");

			if(nb.CssClass != null && nb.CssClass != string.Empty) 
			{
				sb.AppendFormat(" class = '{0}' ", nb.CssClass);
			}

			sb.Append(" style='");

			if(!nb.Width.IsEmpty) 
			{
				sb.AppendFormat(" Width: {0}; ", nb.Width.ToString());
			}

			if(!nb.Height.IsEmpty) 
			{
				sb.AppendFormat(" Height: {0}; ", nb.Height.ToString());
			}

			sb.Append("'>");
			bool expando = false;

			foreach(NavBarBlock bl in nb.Blocks) 
			{
				if(bl.Expanded) 
				{
					expando = true;
					break;
				}
			}

			if(!expando && nb.Blocks.Count>0) 
			{
				nb.Blocks[0].Expanded = true;
			}

			expando = false;

			foreach(NavBarBlock bl in nb.Blocks) 
			{
				if(bl.Items.Count>0) 
				{
					sb.Append("<div");
					sb.Append(" class='block'");
					sb.Append(">");

					//Block Header
					if(!bl.Expanded) 
					{
						sb.Append("<div");
						if(bl.BlockStyle == null || bl.BlockStyle.Closed == null) 
						{
							if(nb.DefaultBlockStyle == null || nb.DefaultBlockStyle.Closed == null) 
							{
								sb.Append(" class='' ");
							}
							else
							{
								sb.AppendFormat(" class = '{0}' " , nb.DefaultBlockStyle.Closed);	
							}
						}
						else
						{
							sb.AppendFormat(" class = '{0}' " , bl.BlockStyle.Closed);	
						}
						sb.AppendFormat(" expanded = '{0}' ", "false");
					}
					else
					{
						sb.Append("<div");
						if(bl.BlockStyle == null || bl.BlockStyle.Expanded  == null) 
						{
							if(nb.DefaultBlockStyle == null || nb.DefaultBlockStyle.Expanded == null) 
							{
								sb.Append(" class='' ");
							}
							else
							{
								sb.AppendFormat(" class = '{0}' " , nb.DefaultBlockStyle.Expanded);	
							}
						}
						else
						{
							sb.AppendFormat(" class='{0}' " , bl.BlockStyle.Expanded);	
						}

						sb.AppendFormat(" expanded = '{0}' ", "true");

					}

					sb.Append(">");
					sb.Append(bl.Text);
					sb.Append("</div>");
					//End Block Header

					//Item Area
					sb.Append("<div");
					if(bl.BlockStyle == null || bl.BlockStyle.ItemArea == null)
					{
						if(nb.DefaultBlockStyle == null || nb.DefaultBlockStyle.ItemArea == null) 
						{
							sb.Append(" class='' ");
						}
						else
						{
							sb.AppendFormat(" class='{0}' ", nb.DefaultBlockStyle.ItemArea);
						}

					}
					else
					{
						sb.AppendFormat(" class='{0}' ", bl.BlockStyle.ItemArea);
					}

					if(!bl.Expanded) 
					{
						sb.Append(" style='display:none' ");
					}

					sb.Append(">");

					sb.Append(GetChildrenAsTable(nb, bl.Items));

					sb.Append("</div>");
					//End Item Area
					sb.Append("</div>");
				}
			}
			sb.Append("</div>");

			return sb.ToString();

		}
	
		/// <summary>
		/// Gets the items wrapped in a table
		/// </summary>
		/// <param name="nb">The Navbar in which rendering is taking place</param>
		/// <param name="items">The items to render into the group</param>
		/// <returns></returns>
		private string GetChildrenAsTable(NavBar nb, NavBarItems items) 
		{
			System.Web.UI.HtmlControls.HtmlTable tbl = new System.Web.UI.HtmlControls.HtmlTable();
			tbl.Border= 0;
			tbl.CellPadding = 2;
			tbl.CellSpacing= 0;
			tbl.Width = "100%";

			foreach(NavBarItem itm in items) 
			{
				HtmlTableRow tr = new HtmlTableRow();
			
				string style = string.Empty;
				if(itm.ItemStyle == null || itm.ItemStyle.Base == null) 
				{
					if(nb.DefaultItemStyle == null || nb.DefaultItemStyle.Base == null) 
						style = string.Empty;
					else
						style = nb.DefaultItemStyle.Base;
				}
				else
				{
					style = itm.ItemStyle.Base;
				}

				string hoverstyle = string.Empty;
				if(itm.ItemStyle == null || itm.ItemStyle.Hover == null) 
				{
					if(nb.DefaultItemStyle == null || nb.DefaultItemStyle.Hover == null) 
						hoverstyle = string.Empty;
					else
						hoverstyle = nb.DefaultItemStyle.Hover;
				}
				else
				{
					hoverstyle = itm.ItemStyle.Hover;
				}

				tr.Attributes.Add("class", style);
				tr.Style.Add("width", "100%");

				HtmlTableCell td = new HtmlTableCell();
				td.Style.Add("width", "100%");
				td.Attributes.Add("class", style);
				td.Attributes.Add("onmouseover", string.Format("nbItemHighlight(this,'{0}')", hoverstyle) );
				td.Attributes.Add("onmouseout", string.Format("nbItemLowlight(this,'{0}')", style));
				
				HtmlAnchor a = new HtmlAnchor();
				
				a.HRef = itm.NavigateUrl;
				StringBuilder inner = new StringBuilder();

				if(itm.Image != null && itm.Image.Length>0) 
				{
					inner.AppendFormat("<img src='{0}'/><br>", itm.Image);
				}

				inner.Append(itm.Text);
				a.InnerHtml = inner.ToString();

				td.Controls.Add(a);

				tr.Cells.Add(td);
				tbl.Rows.Add(tr);

			}
			
			StringBuilder sb = new StringBuilder();
			TextWriter tw = new StringWriter(sb);
			HtmlTextWriter htw = new HtmlTextWriter(tw);

			tbl.RenderControl(htw);

			return sb.ToString();
		}

	}

	
	#endregion
}