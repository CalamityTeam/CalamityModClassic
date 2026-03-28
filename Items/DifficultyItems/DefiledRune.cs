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

namespace CalamityModClassicPreTrailer.Items.DifficultyItems
{
	public class DefiledRune : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Defiled Rune");
			/* Tooltip.SetDefault("Wings do nothing and enemies can critically hit you\n" +
				"Increases most rare item drop chances and enemies drop 50% more cash\n" +
				"Using this while a boss is alive will instantly kill you and despawn the boss\n" +
				"Can only be used in revengeance and death mode\n" +
				"Can be toggled on and off"); */
		}

		public override void SetDefaults()
		{
			Item.rare = 11;
			Item.width = 28;
			Item.height = 28;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = 4;
			Item.UseSound = SoundID.Item100;
			Item.consumable = false;
		}

		public override bool CanUseItem(Player player)
		{
			if (CalamityWorldPreTrailer.bossRushActive || !CalamityWorldPreTrailer.revenge)
			{
				return false;
			}
			return true;
		}

		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			for (int doom = 0; doom < 200; doom++)
			{
				if (Main.npc[doom].active && (Main.npc[doom].boss || Main.npc[doom].type == NPCID.EaterofWorldsHead || Main.npc[doom].type == NPCID.EaterofWorldsTail || Main.npc[doom].type == Mod.Find<ModNPC>("SlimeGodRun").Type ||
					Main.npc[doom].type == Mod.Find<ModNPC>("SlimeGodRunSplit").Type || Main.npc[doom].type == Mod.Find<ModNPC>("SlimeGod").Type || Main.npc[doom].type == Mod.Find<ModNPC>("SlimeGodSplit").Type))
				{
					player.KillMe(PlayerDeathReason.ByOther(12), 1000.0, 0, false);
					Main.npc[doom].active = false;
					Main.npc[doom].netUpdate = true;
				}
			}
			if (!CalamityWorldPreTrailer.defiled)
			{
				CalamityWorldPreTrailer.defiled = true;
				string key = "Your soul is mine...";
				Color messageColor = Color.DarkSeaGreen;
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
				CalamityWorldPreTrailer.defiled = false;
				string key = "Your soul is yours once more...";
				Color messageColor = Color.DarkSeaGreen;
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

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
		}
	}
}