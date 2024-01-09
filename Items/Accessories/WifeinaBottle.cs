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
	public class WifeinaBottle : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Waifu in a Bottle");
			//Tooltip.SetDefault("Summons a sand elemental to fight for you");
		}
		
	    public override void SetDefaults()
	    {
	        Item.width = 20;
	        Item.height = 26;
	        Item.value = 500000;
	        Item.rare = ItemRarityID.Pink;
	        Item.accessory = true;
	    }
	    
	    public override void UpdateAccessory(Player player, bool hideVisual)
		{
	    	CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
			modPlayer.sandWaifu = true;
			if (player.whoAmI == Main.myPlayer)
			{
				if (player.FindBuffIndex(Mod.Find<ModBuff>("SandyWaifu").Type) == -1)
				{
					player.AddBuff(Mod.Find<ModBuff>("SandyWaifu").Type, 3600, true);
				}
				if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("SandyWaifu").Type] < 1)
				{
					Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("SandyWaifu").Type, 60, 2f, Main.myPlayer, 0f, 0f);
				}
			}
		}
	}
}