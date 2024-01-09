using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.UI.Chat;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.UI;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using Terraria.Localization;
using Terraria.GameContent.UI;

using CalamityModClassic1Point2.UI;

namespace CalamityModClassic1Point2
{
	public class UIHandler
	{
		public static UserInterface userBar;
		public static UIBar uiBar;
		
		//call this in mod.Load()
		public static void OnLoad(Mod mod)
		{
			Texture2D borderTex = ModContent.Request<Texture2D>("CalamityModClassic1Point2/ExtraTextures/UI/BarStressBorder").Value, barTex = ModContent.Request<Texture2D>("CalamityModClassic1Point2/ExtraTextures/UI/BarStress").Value; //replace 'null' with your textures.
			uiBar = new UIBar(borderTex, barTex, 4); //the 4 is how many pixels inwards to stick the bar, which in this case is 4.
			userBar = new UserInterface();
			userBar.SetState(uiBar);
		}
		
		//call this in mod.ModifyInterfaceLayers()
		public static void ModifyInterfaceLayers(Mod mod, List<GameInterfaceLayer> layers)
		{
			AddInterfaceLayer(mod, layers, userBar, uiBar, "CalamityModClassic1Point2: Stress Bar", "Vanilla: Mouse Text", true);		
		}
		
		public static void AddInterfaceLayer(Mod mod, List<GameInterfaceLayer> list, UserInterface uInterface, UIElement uElement, string layerName, string parent, bool first)
		{
			GameInterfaceLayer item = new LegacyGameInterfaceLayer(mod.Name + ":" + layerName, delegate
			{
				uInterface.Update(Main._drawInterfaceGameTime);
				uElement.Draw(Main.spriteBatch);
				return true;
			}, InterfaceScaleType.UI);

			int insertAt = -1;
            for (int m = 0; m < list.Count; m++)
            {
                GameInterfaceLayer dl = list[m];
                if (dl.Name.Contains(parent)) { insertAt = m; break; }
            }
            if (insertAt == -1) list.Add(item); else list.Insert(first ? insertAt : insertAt + 1, item);		
		}		
	}
}