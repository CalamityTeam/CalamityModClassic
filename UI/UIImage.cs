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
using Terraria.GameContent;
using SteelSeries.GameSense.DeviceZone;

namespace CalamityModClassic1Point2.UI
{
	public class UIImage : UIElement
	{
		public Texture2D texture = null;
		
		public UIImage(Texture2D tex, int width = -1, int height = -1) : base()
		{
			texture = tex;
			this.Width.Set(width == -1 ? tex.Width : width, 0f);
			this.Height.Set(height == -1 ? tex.Height : height, 0f);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (Main.gameMenu)
				return;
			if (Main.dedServ)
				return;
			if (!Main.LocalPlayer.TryGetModPlayer<CalamityPlayer>(out CalamityPlayer p))
			{
				return;
			}
			if (!CalamityWorld.revenge)
				return;
			CalculatedStyle dimensions = base.GetDimensions();
			Color color = Color.White; //base.IsMouseHovering ? Color.White : Color.Silver; //uncomment this to produce it getting brighter if hovered over.
			int width = (int)dimensions.Width, height = (int)dimensions.Height;
			texture = ModContent.Request<Texture2D>("CalamityModClassic1Point2/ExtraTextures/UI/BarStressBorder").Value;
            Texture2D texture2 = ModContent.Request<Texture2D>("CalamityModClassic1Point2/ExtraTextures/UI/BarStress").Value;
			Rectangle hitBox = new Rectangle((int)(Main.screenWidth * 0.35f), (int)(Main.screenHeight * 0.03f), texture.Width, texture.Height);
            spriteBatch.Draw(texture, hitBox, new Rectangle(0, 0, texture.Width, texture.Height), color);
            spriteBatch.Draw(texture2, new Rectangle((int)(Main.screenWidth * 0.35f) + 4, (int)(Main.screenHeight * 0.03f), (int)((texture2.Width * ((float)Math.Clamp(Main.LocalPlayer.GetModPlayer<CalamityPlayer>().stress, 1, 10000) / 10000))), texture2.Height), new Rectangle(0, 0, (int)(texture2.Width * ((float)Math.Clamp(Main.LocalPlayer.GetModPlayer<CalamityPlayer>().stress, 1, 10000) / 10000)), texture2.Height), color);
			Rectangle maus = new Rectangle((int)Main.MouseWorld.X - (int)Main.screenPosition.X, (int)Main.MouseWorld.Y- (int)Main.screenPosition.Y, 20, 20);
			if (maus.Intersects(hitBox))
			{
                Main.LocalPlayer.mouseInterface = true;
                Main.instance.MouseText("Stress: " + Main.LocalPlayer.GetModPlayer<CalamityPlayer>().stress + "/" + 10000 + "", 0, 0, -1, -1, -1, -1);
            }
        }		
	}
}