using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.NPCs;

namespace CalamityModClassicPreTrailer.Items
{
	public class IronHeart : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Iron Heart");
			/* Tooltip.SetDefault("Makes dying while a boss is alive permanently kill you.\n" +
                "Can be toggled on and off.\n" +
                "Using this while a boss is alive will permanently kill you.\n" +
                "Cannot be activated if any boss has been killed."); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.expert = true;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = 4;
			Item.UseSound = SoundID.Item119;
			Item.consumable = false;
		}

        public override bool CanUseItem(Player player)
        {
            if (CalamityWorldPreTrailer.downedBossAny)
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
					player.KillMeForGood();
					Main.npc[doom].active = false;
                    Main.npc[doom].netUpdate = true;
                }
			}
			if (!CalamityWorldPreTrailer.ironHeart)
			{
				CalamityWorldPreTrailer.ironHeart = true;
				string key = "Iron Heart is active, don't die.";
				Color messageColor = Color.LightSkyBlue;
				if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue(key), messageColor);
				}
				else if (Main.netMode == 2)
				{
					ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
				}
			}
			else
			{
				CalamityWorldPreTrailer.ironHeart = false;
				string key = "Iron Heart is not active, you can die again.";
				Color messageColor = Color.LightSkyBlue;
				if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue(key), messageColor);
				}
				else if (Main.netMode == 2)
				{
					ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
				}
			}
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
			return true;
		}
	}
}