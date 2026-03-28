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
	public class BossRush : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Terminus");
            /* Tooltip.SetDefault("A ritualistic artifact, thought to have brought upon The End many millennia ago\n" +
                                "Sealed away in the abyss, far from those that would seek to misuse it\n" +
								"Activates Boss Rush Mode, using it again will deactivate Boss Rush Mode"); */
        }
		
		public override void SetDefaults()
		{
            Item.rare = 1;
			Item.width = 28;
			Item.height = 28;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = 4;
			Item.UseSound = SoundID.Item123;
			Item.consumable = false;
		}

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
            for (int doom = 0; doom < 200; doom++)
            {
                if (Main.npc[doom].active && Main.npc[doom].boss)
                {
                    Main.npc[doom].active = false;
                    Main.npc[doom].netUpdate = true;
                }
            }
            if (!CalamityWorldPreTrailer.bossRushActive)
            {
                CalamityWorldPreTrailer.bossRushStage = 0;
                CalamityWorldPreTrailer.bossRushActive = true;
                string key = "Hmm? Ah, another contender. Very well, may the ritual commence!";
                Color messageColor = Color.LightCoral;
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
                CalamityWorldPreTrailer.bossRushStage = 0;
                CalamityWorldPreTrailer.bossRushActive = false;
            }
            if (Main.netMode == 2)
            {
                NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
                var netMessage = Mod.GetPacket();
                netMessage.Write((byte)CalamityModClassicPreTrailerMessageType.BossRushStage);
                netMessage.Write(CalamityWorldPreTrailer.bossRushStage);
                netMessage.Send();
            }
            return true;
		}
    }
}