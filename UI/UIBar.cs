using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.UI
{
	public class UIBar : UIState
	{
		public UIElement backPanel, barPanel; //the 'panels' of each part of the UI.
		public int barWidth = 150; //the exact width of the bar texture/panel.
		public Func<int> getValue; //the func used to get the value displayed.
		public int valueMax = 1, barOffset = 0; //the maximum value of the value displayed :: the offset used to draw the bar inwards.

		public UIBar(Texture2D imageBack, Texture2D imageBar, int barOff) : this(imageBack, imageBar, barOff, 10000, GetTickedValue) //uses textures and the test ticker for drawing
		{
		}
		
		public UIBar(Texture2D imageBack, Texture2D imageBar, int barOff, int valMax, Func<int> gValue) : this(valMax, gValue) //uses textures
		{
			backPanel = new UIImage(imageBack);
			barOffset = barOff;
			barPanel = new UIImage(imageBar);
			barWidth = (int)barPanel.Width.Pixels;
		}
		
		public UIBar() : this(10000, GetTickedValue) //for testing purposes
		{
		}
		
		public UIBar(int valMax, Func<int> gValue) : base() //uses panels for drawing instead of textures
		{
			valueMax = valMax;
			getValue = gValue;
		}
		
		public static int tick; //for testing
		public static int GetTickedValue() //ditto
		{
			Mod calamity = ModLoader.GetMod("CalamityModClassic1Point2");
			tick = Main.player[Main.myPlayer].GetModPlayer<CalamityPlayer1Point2>().stress;
			return tick;
		}

		public override void OnInitialize()
		{
			float posX = 500f, posY = 30f; //CHANGE THESE TWO TO CHANGE WHERE IT STARTS ON SCREEN!
			if (backPanel == null) //if not using textures set up panels
			{
				backPanel = new UIPanel();
				((UIPanel)backPanel).SetPadding(0);
				backPanel.Left.Set(posX, 0f);
				backPanel.Top.Set(posY, 0f);
				backPanel.Width.Set(barWidth + 20f, 0f);
				backPanel.Height.Set(50f, 0f);
				((UIPanel)backPanel).BackgroundColor = new Color(73, 94, 171);

				backPanel.OnLeftMouseDown += new UIElement.MouseEvent(DragStart);
				backPanel.OnLeftMouseUp += new UIElement.MouseEvent(DragEnd);	
				
				barPanel = new UIPanel();
				((UIPanel)barPanel).SetPadding(0);
				barPanel.Left.Set(10f, 0f);
				barPanel.Top.Set(10f, 0f);
				barPanel.Width.Set(barWidth, 0f);
				barPanel.Height.Set(30f, 0f);
				((UIPanel)barPanel).BackgroundColor = new Color(200, 0, 0);
				backPanel.Append(barPanel);
			}
			else //otherwise using images so just move it into position
			{
				backPanel.Left.Set(posX, 0f);
				backPanel.Top.Set(posY, 0f);				
				backPanel.OnLeftMouseDown += new UIElement.MouseEvent(DragStart);
				backPanel.OnLeftMouseUp += new UIElement.MouseEvent(DragEnd);

				barPanel.Left.Set(barOffset, 0f);
				barPanel.Top.Set(0f, 0f);		
	
				backPanel.Append(barPanel);
			}
			
			base.Append(backPanel);
		}
		
		public float GetPercentile()
		{
			return ((float)getValue() / Math.Max(1, ((float)valueMax - 1)));
		}
		
		Vector2 offset;
		public bool dragging = false;
		private void DragStart(UIMouseEvent evt, UIElement listeningElement)
		{
			offset = new Vector2(evt.MousePosition.X - backPanel.Left.Pixels, evt.MousePosition.Y - backPanel.Top.Pixels);
			dragging = true;
		}

		private void DragEnd(UIMouseEvent evt, UIElement listeningElement)
		{
			Vector2 end = evt.MousePosition;
			dragging = false;

			backPanel.Left.Set(end.X - offset.X, 0f);
			backPanel.Top.Set(end.Y - offset.Y, 0f);

			Recalculate();
		}

		public override void Update(GameTime gameTime)
		{
			Mod calamity = ModLoader.GetMod("CalamityModClassic1Point2");
			base.Update(gameTime);
			Recalculate(); //THIS IS IMPORTANT! IDK why but when this is included it updates the drawing every tick.
			tick = Main.player[Main.myPlayer].GetModPlayer<CalamityPlayer1Point2>().stress; //updates the testing tick
			if (tick >= 10000)
			{
				tick = 10000;
			}
			barPanel.Width.Set((GetPercentile() * barWidth), 0f); //set the bar's width to the given percentile.	
		}
		
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
		}
	}
}