using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items
{
	public class EidolonTablet : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Eidolon Tablet");
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 20;
			Item.rare = 9;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = 4;
			Item.consumable = true;
		}
		
		public override bool CanUseItem(Player player)
		{
			return !NPC.AnyNPCs(NPCID.CultistBoss) && !NPC.LunarApocalypseIsUp && !NPC.AnyNPCs(NPCID.CultistTablet);
		}
		
		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
            if (Main.netMode != 1)
            {
                int num1302 = NPC.NewNPC(new EntitySource_BossSpawn(player),(int)player.Center.X + 30, (int)player.Center.Y - 90, NPCID.CultistBoss, 0, 0f, 0f, 0f, 0f, 255);
                Main.npc[num1302].direction = (Main.npc[num1302].spriteDirection = Math.Sign(player.Center.X - (float)player.Center.X - 30f));
            }
			return true;
		}
	}
}