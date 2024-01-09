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

namespace CalamityModClassic1Point2.Items.Crabulon 
{
	public class FungalClump : ModItem
	{
	    public override void SetDefaults()
	    {
	        Item.width = 20;
	        Item.height = 26;
	        Item.value = 40000;
	        Item.expert = true;
	        Item.accessory = true;
	    }
	    
	    public override void UpdateAccessory(Player player, bool hideVisual)
		{
	    	CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
			modPlayer.fungalClump = true;
			if (player.whoAmI == Main.myPlayer)
			{
				if (player.FindBuffIndex(Mod.Find<ModBuff>("FungalClump").Type) == -1)
				{
					player.AddBuff(Mod.Find<ModBuff>("FungalClump").Type, 3600, true);
				}
				if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("FungalClump").Type] < 1)
				{
					Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("FungalClump").Type, 10, 1f, Main.myPlayer, 0f, 0f);
				}
			}
			for (int num569 = 0; num569 < 200; num569++)
			{
				if (Main.npc[num569].active && Main.npc[num569].type == (Mod.Find<ModNPC>("CrabulonIdle").Type))
				{
					Main.npc[num569].friendly = true;
					Main.npc[num569].dontTakeDamage = true;
					Main.npc[num569].chaseable = false;
				}
			}
		}
	}
}