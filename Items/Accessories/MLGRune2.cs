using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using CalamityModClassic1Point2.NPCs;

namespace CalamityModClassic1Point2.Items.Accessories
{
	public class MLGRune2 : ModItem
	{
		
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.expert = true;
			Item.maxStack = 99;
			Item.useAnimation = 30;
			Item.useTime = 30;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item4;
			Item.consumable = true;
		}
		
		public override bool CanUseItem(Player player)
		{
			CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
			if (!Main.expertMode || modPlayer.extraAccessoryML)
			{
				return false;
			}
			return true;
		}

		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
			if (player.itemAnimation > 0 && !modPlayer.extraAccessoryML && player.itemTime == 0)
			{
				player.itemTime = Item.useTime;
				modPlayer.extraAccessoryML = true;
				if (!CalamityWorld.onionMode)
				{
					CalamityWorld.onionMode = true;
				}
				NetMessage.SendData(MessageID.SyncPlayer, -1, -1, null, 0, player.whoAmI, 0f, 0f, 0, 0, 0);
			}
			return true;
		}
	}
}