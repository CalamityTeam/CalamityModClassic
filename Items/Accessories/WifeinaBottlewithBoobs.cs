using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.IO;
using Terraria.ObjectData;
using Terraria.Utilities;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories 
{
	public class WifeinaBottlewithBoobs : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Rare Waifu in a Bottle");
			//Tooltip.SetDefault("Summons a sand elemental to fight for you\n;D");
		}
		
	    public override void SetDefaults()
	    {
	        Item.width = 20;
	        Item.height = 26;
	        Item.value = 500000;
	        Item.expert = true;
	        Item.accessory = true;
	    }
	    
	    public override void UpdateAccessory(Player player, bool hideVisual)
		{
	    	CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
			modPlayer.sandBoobWaifu = true;
			if (player.whoAmI == Main.myPlayer)
			{
				if (player.FindBuffIndex(Mod.Find<ModBuff>("DrewsSandyWaifu").Type) == -1)
				{
					player.AddBuff(Mod.Find<ModBuff>("DrewsSandyWaifu").Type, 3600, true);
				}
				if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("DrewsSandyWaifu").Type] < 1)
				{
					Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("DrewsSandyWaifu").Type, 60, 2f, Main.myPlayer, 0f, 0f);
				}
			}
		}
	}
}