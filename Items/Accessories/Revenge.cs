using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using CalamityModClassic1Point2.NPCs;

namespace CalamityModClassic1Point2.Items.Accessories
{
	public class Revenge : ModItem
	{		
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.expert = true;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item119;
			Item.consumable = true;
		}
		
		public override bool CanUseItem(Player player)
		{
			if (!Main.expertMode)
			{
				return false;
			}
			return true;
		}

		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			for (int doom = 0; doom < 200; doom++)
			{
				if (Main.npc[doom].active && Main.npc[doom].boss)
				{
					player.KillMe(PlayerDeathReason.ByOther(12), 1000.0, 0, false);
					Main.npc[doom].active = false;
				}
			}
			if (!CalamityWorld.revenge)
			{
				CalamityWorld.revenge = true;
			}
			return true;
		}
	}
}