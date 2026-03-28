using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.NPCs;

namespace CalamityModClassicPreTrailer.Items.TheDevourerofGods
{
	public class CosmicWorm : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cosmic Worm");
			/* Tooltip.SetDefault("Summons the Devourer of Gods\n" +
                "Not consumable"); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 18;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = 4;
			Item.consumable = false;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 13;
		}
		
		public override bool CanUseItem(Player player)
		{
			return !NPC.AnyNPCs(Mod.Find<ModNPC>("DevourerofGodsHead").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("DevourerofGodsHeadS").Type) && CalamityWorldPreTrailer.DoGSecondStageCountdown <= 0 && CalamityWorldPreTrailer.downedBossAny;
		}
		
		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
            string key = "You are no god...but I shall feast upon your essence regardless!";
            Color messageColor = Color.Cyan;
            if (Main.netMode == 0)
            {
                Main.NewText(Language.GetTextValue(key), messageColor);
            }
            else if (Main.netMode == 2)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
            }
            NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("DevourerofGodsHead").Type);
			SoundEngine.PlaySound(SoundID.Roar, player.position);
			return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "ArmoredShell", 3);
			recipe.AddIngredient(null, "TwistingNether");
			recipe.AddIngredient(null, "DarkPlasma");
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}
}