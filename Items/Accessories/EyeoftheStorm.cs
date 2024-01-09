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
	public class EyeoftheStorm : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Eye of the Storm");
			//Tooltip.SetDefault("Summons a cloud elemental to fight for you");
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
			modPlayer.cloudWaifu = true;
			if (player.whoAmI == Main.myPlayer)
			{
				if (player.FindBuffIndex(Mod.Find<ModBuff>("CloudyWaifu").Type) == -1)
				{
					player.AddBuff(Mod.Find<ModBuff>("CloudyWaifu").Type, 3600, true);
				}
				if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("CloudyWaifu").Type] < 1)
				{
					Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("CloudyWaifu").Type, 60, 2f, Main.myPlayer, 0f, 0f);
				}
			}
		}
	}
}