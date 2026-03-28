using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
	public class Omniblade : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Omniblade");
			/* Tooltip.SetDefault("An ancient blade forged by the legendary Omnir\n" +
			                   "Has a 20% chance to give enemies the whispering death debuff on hit\n" +
			                   "This debuff cuts enemy defense by 50 points"); */
		}

		public override void SetDefaults()
		{
			Item.width = 64;
			Item.damage = 63;
			Item.crit += 45;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 10;
			Item.useStyle = 1;
			Item.useTime = 10;
			Item.useTurn = true;
			Item.knockBack = 6f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 146;
            Item.value = Item.buyPrice(0, 80, 0, 0);
            Item.rare = 8;
		}
		
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
			if (Main.rand.Next(5) == 0)
			{
				target.AddBuff(Mod.Find<ModBuff>("WhisperingDeath").Type, 360);
			}
		}
	
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Katana);
            recipe.AddIngredient(null, "BarofLife", 20);
			recipe.AddIngredient(null, "CoreofCalamity", 10);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	}
}
